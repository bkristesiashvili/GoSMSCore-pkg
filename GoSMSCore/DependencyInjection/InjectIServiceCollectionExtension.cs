using GoSMSCore.Config;

using System;
using Microsoft.Extensions.DependencyInjection;


namespace GoSMSCore.DependencyInjection
{
    public static class InjectIServiceCollectionExtension
    {
        /// <summary>
        /// Add to services collection Go SMS all dependencies
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddGoSmsService(this IServiceCollection services, Action<SmsSettingsOption> option)
        {
            try
            {
                if (services == null) throw new ArgumentNullException($"{ nameof(services) }");
                if (option == null) throw new ArgumentNullException($"{ nameof(option) }");

                var smsOption = new SmsSettingsOption();

                option.Invoke(smsOption);

                services.AddSingleton<ISmsSettings>(new SmsSettings(smsOption));

                services.AddTransient<ISmsService, GoSmsService>();

                return services;
            }
            catch{ throw; }
        }
    }
}
