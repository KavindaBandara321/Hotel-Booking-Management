using Hotel_Booking_Management.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access
{
    public class HotelDbContext : DbContext
    {
        public DbSet<Booking> bookings { get; set; }

        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    string connectionString = @"Server=.\SQLEXPRESS;Database=TestDb;userid=sa;password=778569119119kS";
        //    optionsBuilder.UseSqlServer(connectionString);
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server =.\SQLEXPRESS; Database = TestDb; userid = sa; password = 778569119119kS");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>().HasKey(b => b.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
} 
