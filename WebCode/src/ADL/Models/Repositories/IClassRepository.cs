using System.Collections.Generic;

namespace ADL.Models.Repositories
{
    public interface IClassRepository
    {
        IEnumerable<Class> Classes { get; }
        void SaveClass(Class theClass);
        Class DeleteClass(int classId);
        void AddPersonToClass(int classId, Person newPerson);

    }
}
