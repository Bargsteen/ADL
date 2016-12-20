using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ADL.Models.Repositories
{

    public class EfLocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _context;

        public EfLocationRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public IEnumerable<Location> Locations => _context.Locations.Include(l => l.PersonAssignmentCouplings);

        public void SaveLocation(Location location)
        {
            if (location.LocationId == 0)
            {
                // This is a new location
                _context.Locations.Add(location);
            }
            else
            {
                // Editing an existing location
                Location dbEntry = Locations.FirstOrDefault(l => l.LocationId == location.LocationId);
                if (dbEntry != null)
                {
                    dbEntry.Title = location.Title;
                    dbEntry.Description = location.Description;
                }
            }
            _context.SaveChanges();
        }

        public Location DeleteLocation(int locationId)
        {
            Location dbEntry = Locations.FirstOrDefault(l => l.LocationId == locationId);
            if (dbEntry != null)
            {
                _context.Locations.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public bool AddCouplingsToLocation(int locationId, List<PersonAssignmentCoupling> personAssignmentCouplings)
        {
            Location dbEntry = Locations.FirstOrDefault(l => l.LocationId == locationId);
            if (dbEntry != null)
            {
                if (dbEntry.PersonAssignmentCouplings == null)
                {
                    dbEntry.PersonAssignmentCouplings = new List<PersonAssignmentCoupling>();
                }
                dbEntry.PersonAssignmentCouplings.AddRange(personAssignmentCouplings);
                _context.SaveChanges();
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
                    foreach (PersonAssignmentCoupling coupling in dbEntry.PersonAssignmentCouplings)
                    {
                        if (coupling.PersonId == personId) // Selects all couplings with this person.
                        {
                            couplingsToBeDeleted.Add(coupling);
                        }
                    }

                    if (couplingsToBeDeleted.Count() != 0) // This person is coupled with the location at least once.
                    {
                        foreach (var coupling in couplingsToBeDeleted)
                        {
                            dbEntry.PersonAssignmentCouplings.Remove(coupling);
                            _context.Remove(coupling);
                        }
                        _context.SaveChanges();
                    }

                }

                return true;
            }
            return false;
        }

        public void RemoveSpecificCouplingOnLocation(int locationId, PersonAssignmentCoupling coupling)
        {
            var dbEntry = Locations.FirstOrDefault(l => l.LocationId == locationId);
            var couplingToBeDeleted = dbEntry?.PersonAssignmentCouplings?.FirstOrDefault(pac => pac.PersonAssignmentCouplingId == coupling.PersonAssignmentCouplingId);
            if (couplingToBeDeleted != null)
            {
                dbEntry.PersonAssignmentCouplings.Remove(couplingToBeDeleted);
                _context.Remove(couplingToBeDeleted);
                _context.SaveChanges();
            }
        }
    }
}



