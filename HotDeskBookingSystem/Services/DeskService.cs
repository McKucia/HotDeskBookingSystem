using System;
using HotDeskBookingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotDeskBookingSystem
{
    public interface IDeskService
    {
        IEnumerable<Desk> GetAll(string orderBy, bool isAvailable);
    }

    public class DeskService : IDeskService
    {
        private readonly DeskDbContext _dbContext;

        public DeskService(DeskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Desk> GetAll(string orderBy, bool isAvailable)
        {
            var desks = _dbContext
                .Desks
                .Include(d => d.Location)
                .ToList();

            if(isAvailable)
            {
                desks = desks.Where(d => d.Status == Status.Available).ToList();
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
    }
}

