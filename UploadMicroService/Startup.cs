using System;
using BlobStorage;
using BlobStorage.Interface;
using DataStore;
using DataStore.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UploadMicroService.Factory;
using UploadMicroService.Interface;
using UploadMicroService.Service;


namespace UploadMicroService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
           
            services.AddScoped<iDocumentStorageService, DocumentStorageService>();
            services.AddScoped<iMemberStorageFactory, MemberStorageItemFactory>();
            var datastoreconnection =   Environment.GetEnvironmentVariable("dataconnectionstring");
            var blobstoreconnection =  Environment.GetEnvironmentVariable("storageconnectionstring");
            var blobcontainer = Environment.GetEnvironmentVariable("containername");
            services.AddScoped<iDataStoreRepository, DataStoreRepository>(_ => new DataStoreRepository(datastoreconnection));
            services.AddScoped<iBlobClient>(_ => new BlobClient(blobstoreconnection, blobcontainer));
            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Storage API V1");
            });

            //make swagger the default page for convenience 
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger/index.html");
            app.UseRewriter(option);

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
            });
        }
    }
}
