using Hotel_Booking_Management.IRepository;
using Hotel_Booking_Management.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hotel_Booking_Management.Repository
{
    public class XmlBookingRepository : IBookingRepository
    {
        private readonly string _xmlFilePath = @"C:\Msc\CW1_W2151426\Backend\Hotel Booking Management\bookings.xml";


        private async Task<List<Booking>> LoadBookingsAsync()
        {
            if (!File.Exists(_xmlFilePath))
                return new List<Booking>();

            using var stream = File.OpenRead(_xmlFilePath);
            var serializer = new XmlSerializer(typeof(List<Booking>));
            return (List<Booking>)serializer.Deserialize(stream);
        }

        private async Task SaveBookingsAsync(List<Booking> bookings)
        {
            var directory = Path.GetDirectoryName(_xmlFilePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            using var stream = File.Create(_xmlFilePath);
            var serializer = new XmlSerializer(typeof(List<Booking>));
            serializer.Serialize(stream, bookings);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await LoadBookingsAsync();
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            var bookings = await LoadBookingsAsync();
            return bookings.Find(b => b.Id == id);
        }

        public async Task<Booking> AddAsync(Booking booking)
        {
            var bookings = await LoadBookingsAsync();
            booking.Id = bookings.Count > 0 ? bookings.Max(b => b.Id) + 1 : 1;
            bookings.Add(booking);
            await SaveBookingsAsync(bookings);
            return booking;
        }

        public async Task<Booking> UpdateAsync(Booking booking)
        {
            var bookings = await LoadBookingsAsync();
            var index = bookings.FindIndex(b => b.Id == booking.Id);
            if (index == -1) return null;

            bookings[index] = booking;
            await SaveBookingsAsync(bookings);
            return booking;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var bookings = await LoadBookingsAsync();
            var booking = bookings.Find(b => b.Id == id);
            if (booking == null) return false;

            bookings.Remove(booking);
            await SaveBookingsAsync(bookings);
            return true;
        }
    }
}
