using Messaging.Users.Domain.Repositories;
using Messaging.Common.Commands;
using Messaging.Common.Mongo;
using Messaging.Common.RabbitMq;
using Messaging.Services.Identity.Domain.Services;
using Messaging.User.Services.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Messaging.User.Services;
using Messaging.Shared.Common.Commands;

namespace Messaging.Services.Users
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
            services.AddLogging();
            services.AddMongoDB(Configuration);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddRabbitMq(Configuration);
            services.AddScoped<ICommandHandler<CreateUser>, CreateUserHandler>();
            services.AddScoped<ICommandHandler<BlockUser>, BlockUserHandler>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IEncrypter, Encrypter>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Messaging.Services.Users", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Messaging.Services.Users v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
