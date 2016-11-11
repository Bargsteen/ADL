using System.Collections.Generic;

namespace ADL.Models {

    public interface ILocationRepository {
        IEnumerable<Location> Location { get; }
        void Save(Location location);
        void Delete(Location location);
    }
}
