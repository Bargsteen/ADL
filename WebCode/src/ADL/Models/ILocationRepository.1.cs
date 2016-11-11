using System.Collections.Generic;

namespace ADL.Models {

    public interface ILocationRepository {
        IEnumerable<Location> Location { get; }
        void Add(Location location);
        void Delete(Location location);
        void Edit(Location location);
    }
}
