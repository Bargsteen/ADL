using System.Collections.Generic;

namespace ADL.Models {

    public interface ILocationRepository {
        IEnumerable<Location> Locations { get; }
        void SaveLocation(Location location);
        Location DeleteLocation(int locationId);
        bool SaveAttachedAssignmentId(int locationId, int personId, int assignmentId);
    }
}
