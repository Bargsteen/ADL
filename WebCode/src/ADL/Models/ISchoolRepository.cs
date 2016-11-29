﻿using System.Collections.Generic;

namespace ADL.Models
{
    public interface ISchoolRepository
    {
        IEnumerable<School> Schools { get; }
        void SaveSchool(School school);
        School DeleteSchool(int schoolId);

    }
}
