using System;
using System.Collections.Generic;
using System.Linq;

namespace ADL.Models
{

    public class EFLocationRepository : ILocationRepository
    {
        private ApplicationDbContext context;

        public EFLocationRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Location> Locations => context.Locations;

        public void SaveLocation(Location location)
        {
            if (location.LocationId == 0)
            {
                // This is a new location
                context.Locations.Add(location);
            }
            else
            {
                // Editing an existing location
                Location dbEntry = context.Locations.FirstOrDefault(l => l.LocationId == location.LocationId);
                if (dbEntry != null)
                {
                    dbEntry.Title = location.Title;
                    dbEntry.Description = location.Description;
                }
            }
            context.SaveChanges();
        }

        public Location DeleteLocation(int locationId)
        {
            Location dbEntry = context.Locations
                .FirstOrDefault(l => l.LocationId == locationId);
            if (dbEntry != null)
            {
                context.Locations.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public bool SaveAttachedAssignmentId(int locationId, int assignmentId)
        {
            Location dbEntry = context.Locations
                .FirstOrDefault(l => l.LocationId == locationId);
            if (dbEntry != null)
            {
                dbEntry.AttachedAssignmentId = assignmentId;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}



