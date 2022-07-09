using System;
using System.Collections.Generic;
using System.Linq;
using HotDeskBookingSystem.Entities;

namespace HotDeskBookingSystem
{
    public class DeskSeeder
    {
        private readonly DeskDbContext _dbContext;

        public DeskSeeder(DeskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Desks.Any())
                {
                    var reservations = GetReservations();
                    _dbContext.Reservations.AddRange(reservations);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Reservation> GetReservations()
        {
            Reservation reservation_1 = new Reservation()
            {
                CreatedAt = DateTime.Now,
                StartAt = new DateTime(2022, 7, 14, 8, 0, 0),
                FinishAt = new DateTime(2022, 7, 16, 8, 0, 0),
                Desk = new Desk()
                {
                    Status = Status.Unavailable,
                    Location = new Location()
                    {
                        FloorNumber = 0,
                        RoomNumber = 3
                    }
                },
                Employee = new Employee()
                {
                    FirstName = "Maciej",
                    SecondName = "Kucia"
                }
            };
            reservation_1.DaysDuration =
                ((int)reservation_1.FinishAt.Subtract(reservation_1.StartAt).TotalDays);

            var reservations = new List<Reservation>()
            {
                reservation_1
            };

            return reservations;
        }
    }
}