using System;
using HotDeskBookingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotDeskBookingSystem.Services
{
    public interface IDeskService
    {
        IEnumerable<Desk> GetAll(string orderBy, bool isAvailable, DateTime start, DateTime finish);
        bool CreateReservation(int deskId, int employeeId, DateTime start, DateTime finish);
        bool ChangeDesk(int reservationId, int deskId);
    }

    public class DeskService : IDeskService
    {
        private readonly DeskDbContext _dbContext;

        public DeskService(DeskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Desk> GetAll(string orderBy, bool isAvailable, DateTime start, DateTime finish)
        {
            var desks = _dbContext
                    .Desks
                    .Include(d => d.Location)
                    .ToList();

            if (isAvailable)
            {
                var reservedDesks = _dbContext
                    .Reservations
                    .Where(r => start <= r.FinishAt && finish >= r.StartAt)
                    .Select(r => r.Desk)
                    .ToList();

                desks = desks.Except(reservedDesks).ToList();
            }

            switch (orderBy)
            {
                case "id":
                    desks = desks.OrderBy(d => d.Id).ToList();
                    break;
                case "location":
                    desks = desks
                        .OrderBy(d => d.Location.FloorNumber)
                        .ThenBy(d => d.Location.RoomNumber)
                        .ToList();
                    break;
                default:
                    desks = desks.OrderBy(d => d.Id).ToList();
                    break;
            }

            return desks;
        }

        public bool CreateReservation(int deskId, int employeeId, DateTime start, DateTime finish)
        {
            var desk = _dbContext
                .Desks
                .FirstOrDefault(d => d.Id == deskId);

            var employee = _dbContext
                .Employees
                .FirstOrDefault(e => e.Id == employeeId);

            if (desk is null || employee is null)
                return false;

            // desk is unavailable
            if (_dbContext
                .Reservations
                .Where(r => r.Desk.Id == deskId)
                .Any(r => start <= r.FinishAt && finish >= r.StartAt))
            {
                return false;
            }

            // more than a week
            if (finish.Subtract(start).Days > 7)
            {
                return false;
            }

            desk.Status = Status.Unavailable;

            var reservation = new Reservation()
            {
                Desk = desk,
                Employee = employee,
                CreatedAt = DateTime.Now,
                StartAt = start,
                FinishAt = finish
            };

            reservation.DaysDuration = finish.Subtract(start).Days;

            _dbContext.Reservations.Add(reservation);
            _dbContext.SaveChanges();

            return true;
        }

        public bool ChangeDesk(int reservationId, int deskId)
        {
            var reservation = _dbContext
                .Reservations
                .FirstOrDefault(r => r.Id == reservationId);

            var desk = _dbContext
                .Desks
                .FirstOrDefault(d => d.Id == deskId);
            
            if (reservation is null
                || desk == null
                // Not later than the 24h before reservation.
                || reservation.StartAt.Subtract(DateTime.Now).Hours < 24)
                return false;

            // desk is unavailable
            if (_dbContext
                .Reservations
                .Where(r => r.Desk.Id == deskId)
                .Any(r => reservation.StartAt <= r.FinishAt && reservation.FinishAt >= r.StartAt))
            {
                return false;
            }

            reservation.Desk = desk;

            _dbContext.Entry(desk).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return true;
        }
    }
}

