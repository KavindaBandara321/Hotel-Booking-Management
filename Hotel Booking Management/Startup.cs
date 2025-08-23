using Hotel_Booking_Management.IRepository;
using Hotel_Booking_Management.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Booking_Management
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
            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel_Booking_Management", Version = "v1" });
            });
            services.AddDbContext<HotelDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Register repository
            //services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddHostedService<BackgroundWorker>();
            services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();
            services.AddScoped<ICalendarRepository, CalendarRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<ISpecialRequestRepository, SpecialRequestRepository>();
            bool useXmlStorage = Configuration.GetValue<bool>("StorageOptions:UseXmlStorage");
            services.AddScoped<IBookingRepository, XmlBookingRepository>();

            //if (useXmlStorage)
            //{
            //    services.AddScoped<IBookingRepository, XmlBookingRepository>();
            //}
            //else
            //{
            //    services.AddScoped<IBookingRepository, BookingRepository>();
            //}


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel_Booking_Management v1"));
            }
            app.UseCors(policy =>
                policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
);
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
