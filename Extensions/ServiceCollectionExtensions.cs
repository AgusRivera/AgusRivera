using AMVTravels.Abstractions.Services;
using AMVTravels.Contexts;
using AMVTravels.Models.Dtos;
using AMVTravels.Models.Entities;
using AMVTravels.Models.Entities.Identity;
using AMVTravels.Services;
using AMVTravels.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace AMVTravels.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            //! Todos los servicios deben ser agregados desde aqui.

            builder.Services.AddScoped<IManagerServices, ManagerServices>();

            ////! Identity
            builder.Services.AddIdentity<User, Role>(opt =>
            {
                //Password
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 10;

                //Email Confirmed
                opt.SignIn.RequireConfirmedEmail = false;

                //Lockout
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 5;
            })
                            .AddDefaultTokenProviders()
                            .AddEntityFrameworkStores<IdentityDBContext>();

            //Todos los servicios de validación deben ser agregados desde aqui.
            builder.Services.AddScoped<IValidator<Tour>, TourValidator>();
            builder.Services.AddScoped<IValidator<Reservation>, ReservationValidator>();





        }
    }
}
