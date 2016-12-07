using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ADL.Models.Repositories
{

    public class EFLocationRepository : ILocationRepository
    {
        private ApplicationDbContext context;

        public EFLocationRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Location> Locations => context.Locations.Include(l => l.PersonAssignmentCouplings);

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

        public bool AddCouplingsToLocation(int locationId, List<PersonAssignmentCoupling> personAssignmentCouplings)
        {
            Location dbEntry = Locations.FirstOrDefault(l => l.LocationId == locationId);
            if (dbEntry != null)
            {
                if(dbEntry.PersonAssignmentCouplings == null)
                {
                    dbEntry.PersonAssignmentCouplings = new List<PersonAssignmentCoupling>();
                }
                dbEntry.PersonAssignmentCouplings.AddRange(personAssignmentCouplings);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveAllCouplingsForSpecificPersonOnLocation(int locationId, string personId)
        {
            Location dbEntry = Locations.FirstOrDefault(l => l.LocationId == locationId);
            if (dbEntry != null)
            {
                if (dbEntry.PersonAssignmentCouplings != null)
                {
                    List<PersonAssignmentCoupling> couplingsToBeDeleted = new List<PersonAssignmentCoupling>();
                    foreach(PersonAssignmentCoupling coupling in dbEntry.PersonAssignmentCouplings)
                    {
                        if(coupling.PersonId == personId) // Selects all couplings with this person.
                        {
                            couplingsToBeDeleted.Add(coupling);
                        }
                    }

                    if (couplingsToBeDeleted.Count() != 0) // This person is coupled with the location at least once.
                    {
                        foreach (var coupling in couplingsToBeDeleted)
                        {
                            dbEntry.PersonAssignmentCouplings.Remove(coupling);
                            context.Remove(coupling);
                        }
                        context.SaveChanges();
                    }
                    
                }

                return true;
            }
            return false;
        }
    }
}



