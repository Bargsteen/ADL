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

        public IEnumerable<Location> Locations => context.Locations;


        /*public void SaveProduct(Product product) {
            if (product.ProductID == 0) {
                context.Products.Add(product);
            } else {
                Product dbEntry = context.Products
                    .FirstOrDefault(p => p.ProductID == product.ProductID);
                if (dbEntry != null) {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            context.SaveChanges();
        }*/
        public void SaveLocation(Location location)
        {
            if(location.LocationId == 0) 
            {
                // This is a new location
                context.Locations.Add(location);
            }
            else
            {
                Location dbEntry = context.Locations.FirstOrDefault(l => l.LocationId == location.LocationId);
                if(dbEntry != null)
                {
                    dbEntry = location;
                    context.Update(dbEntry);
                }
            }
            context.SaveChanges();
        }

        public Location DeleteLocation(int locationId) {
            Location dbEntry = context.Locations
                .FirstOrDefault(l => l.LocationId == locationId);
            if (dbEntry != null) {
                context.Locations.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
