using Application;
using Application.Abstraction.IRepository;
using Application.Abstraction.IService;
using Application.Common;
using Infrastructure.Persistence;
using Infrastructure.Repository;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureOption(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LendingConfig>(configuration.GetSection("LendingConfig"));
            services.Configure<NotificationConfig>(configuration.GetSection("NotificationConfig"));
            services.Configure<SsoConfig>(configuration.GetSection("SsoConfig"));
            return services;
        }

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CPGDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CPGDbContext")));

            services.AddScoped<ICPGDbContext, CPGDbContext>();

            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IOtpRepository, OtpRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IPurchaseDetailRepository, PurchaseDetailRepository>();
            return services;
        }

        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IOtpService, OtpService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<ILendingService, LendingService>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<IPurchaseDetailService, PurchaseDetailService>();
            return services;
        }

        public static IServiceCollection AddProviderHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            var notificationConfig = configuration.GetSection("NotificationConfig").Get<NotificationConfig>();
            services.AddHttpClient("Notification", o =>
            {
                o.BaseAddress = new Uri(notificationConfig.BaseUrl);
                o.Timeout = notificationConfig.Timeout;
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                }
            }).SetHandlerLifetime(TimeSpan.FromMinutes(10));

            var lendingConfig = configuration.GetSection("LendingConfig").Get<LendingConfig>();
            services.AddHttpClient("Lending", o =>
            {
                o.BaseAddress = new Uri(lendingConfig.BaseUrl);
                o.Timeout = lendingConfig.Timeout;
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                }
            }).SetHandlerLifetime(TimeSpan.FromMinutes(10));

            return services;
        }

        public static IServiceCollection AddSsoConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var ssoConfig = configuration.GetSection("SsoConfig").Get<SsoConfig>();

            var publicKeyBytes = Convert.FromBase64String(ssoConfig.PublicKey);
            var rsa = RSA.Create();
            rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out int _);
            var rsaSecurityKey = new RsaSecurityKey(rsa);

            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
                {
                    o.MetadataAddress = ssoConfig.MetadataAddress;
                    o.Authority = ssoConfig.Authority;
                    o.RequireHttpsMetadata = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidIssuer = ssoConfig.ValidIssuer,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        IssuerSigningKey = rsaSecurityKey,
                        ValidateLifetime = true,    
                    };
                });

            return services;
        }
    }
}
