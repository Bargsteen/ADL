using System.Collections.Generic;

namespace ADL.Models.Repositories
 {

    public interface ILocationRepository {
        IEnumerable<Location> Locations { get; }
        void SaveLocation(Location location);
        Location DeleteLocation(int locationId);
        bool SavePersonAssignmentCoupling(int locationId, List<PersonAssignmentCoupling> personAssignmentCouplings);
        bool RemovePersonAssignmentCoupling(int locationId);
    }
}
