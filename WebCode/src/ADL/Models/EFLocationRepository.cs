using System;
using System.Collections.Generic;
using System.Linq;

namespace ADL.Models {

    public class EFLocationRepository : ILocationRepository 
    {
        private ApplicationDbContext context;

        public EFLocationRepository(ApplicationDbContext ctx) 
        {
            context = ctx;
        }

        public IEnumerable<Location> Location => context.Locations;

        public void Save(Location location)
        {
            bool exists = false;
            foreach (Location DBLocation in context.Locations)
            {
                if (DBLocation.LocationID == location.LocationID)
                {
                    exists = true;
                }
            }
            if (exists == false)
            {
                context.Add(location);
            }
            else
            {
                context.Update(location);
            }
            context.SaveChanges();
        }

        public void Delete(Location location)
        {
            context.Remove(location);
            context.SaveChanges();
        }
    }
}
