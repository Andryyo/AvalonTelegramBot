using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalon.Core.Interfaces;
using Avalon.Web.Interfaces;
using Avalon.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Avalon.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddTransient<ITelegramBotClient, TelegramBotClient>((p) => new TelegramBotClient(Configuration["botToken"]));
            services.AddTransient<IUserInteractionService, UserInteractionService>();
            services.AddSingleton<IGamesManager, GamesManager>();

            services.AddSingleton<ICallbackQueryHandler, CallbackQueryHandler>();

            services.AddTransient<ICommandHandler, StartCommandHandler>();
            services.AddTransient<ICommandHandler, JoinCommandHandler>();
            services.AddTransient<ICommandHandler, PlayCommandHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ITelegramBotClient client)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (!env.IsDevelopment())
            {
                client.DeleteWebhookAsync().Wait();
                client.SetWebhookAsync(
                    url: "https://" + Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME") + "/api",
                    allowedUpdates: new UpdateType[] { UpdateType.Message, UpdateType.CallbackQuery }).Wait();
            }
        }
    }
}
