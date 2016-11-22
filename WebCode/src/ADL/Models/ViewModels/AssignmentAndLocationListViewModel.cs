﻿using System.Collections.Generic;

namespace ADL.Models.ViewModels
{
    public class AssignmentAndLocationListViewModel
    {
        public IEnumerable<Location> Locations { get; set; }
        public IEnumerable<Assignment> Assignments { get; set; }
    }
}