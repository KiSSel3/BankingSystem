﻿using Domain.Interfaces;
using Domain.Sevices;
using BankClient.Pages;
using BankClient.ViewModels;

using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using BankClient.Interfaces;
using BankClient.Services;

namespace BankClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Appetite.ttf", "Appetite");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            SetupServices(builder.Services);

            return builder.Build();
        }

        private static void SetupServices(IServiceCollection services)
        {
            //Pages
            services.AddTransient<LoginPage>();

            //ViewModels
            services.AddTransient<LoginViewModel>();

            //Services
            services.AddTransient<IEncoderService, EncoderService>();
            services.AddTransient<IRegistrationService, RegistrationService>();
        }
    }
}