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
                if (!_dbContext.Desks.Any() && !_dbContext.Reservations.Any())
                {
                    var reservations = GetReservations();
                    var desks = GetDesks();

                    _dbContext.Reservations.AddRange(reservations);
                    _dbContext.SaveChanges();

                    _dbContext.Desks.AddRange(desks);
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
                    SecondName = "Kucia",
                    Email = "macikucia@onet.pl",
                    Role = new Role()
                    {
                        Name = "Employee"
                    }
                }
            };
            reservation_1.DaysDuration =
                ((int)reservation_1.FinishAt.Subtract(reservation_1.StartAt).TotalDays);

            Reservation reservation_2 = new Reservation()
            {
                CreatedAt = DateTime.Now,
                StartAt = new DateTime(2022, 7, 15, 8, 0, 0),
                FinishAt = new DateTime(2022, 7, 20, 8, 0, 0),
                Desk = new Desk()
                {
                    Status = Status.Unavailable,
                    Location = new Location()
                    {
                        FloorNumber = 0,
                        RoomNumber = 2
                    }
                },
                Employee = new Employee()
                {
                    FirstName = "Jakub",
                    SecondName = "Jezierczak",
                    Email = "jakoobas@interia.pl",
                    Role = new Role()
                    {
                        Name = "Admin"
                    }
                }
            };
            reservation_2.DaysDuration =
                ((int)reservation_2.FinishAt.Subtract(reservation_2.StartAt).TotalDays);

            var reservations = new List<Reservation>()
            {
                reservation_1, reservation_2
            };

            return reservations;
        }

        private IEnumerable<Desk> GetDesks()
        {
            var desks = new List<Desk>()
            {
                new Desk()
                {
                    Location = new Location()
                    {
                        FloorNumber = 0,
                        RoomNumber = 1
                    }
                },
                new Desk()
                {
                    Location = new Location()
                    {
                        FloorNumber = 0,
                        RoomNumber = 2
                    }
                },
                new Desk()
                {
                    Location = new Location()
                    {
                        FloorNumber = 1,
                        RoomNumber = 1
                    }
                },
                new Desk()
                {
                    Location = new Location()
                    {
                        FloorNumber = 0,
                        RoomNumber = 2
                    }
                },
                new Desk()
                {
                    Location = new Location()
                    {
                        FloorNumber = 3,
                        RoomNumber = 1
                    }
                }
            };

            return desks;
        }
    }
}