using Dropbox.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Project.Source.Infrastructure;
using Project.Source.Domain;
using Project.Source.AntiCorruption;

namespace Project
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
            services.AddMvc();

            DropboxClient dropboxClient =
                new DropboxClient("eChWowI7hUYAAAAAAAAgtGRtv1XnZTldjnQSRVU7ZDBm8IFPOfwvtAe1B4xeNuJT");
            
            services.AddTransient<DropboxDataProvider>(s => new DropboxDataProvider(dropboxClient));
            services.AddTransient<DropboxFileUploader>(s => new DropboxFileUploader(dropboxClient));
            services.AddTransient<IDataProvider, DataProvider>();
            services.AddTransient<IFileUploader, FileUploader>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
