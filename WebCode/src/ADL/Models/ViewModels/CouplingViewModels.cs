using System;
using System.Collections.Generic;

namespace ADL.Models.ViewModels
{

    public class ChooseClassViewModel
    {
        public int ChosenAssignmentSetId { get; set; }
        public int ChosenClassId { get; set; }
        public IEnumerable<Class> AvailableClasses { get; set; }
    }

    public class ChooseLocationsViewModel
    {
        public List<PersonAssignmentCoupling> PersonAssignmentCouplings { get; set; }
        public List<Location> AvailableLocations { get; set; }
        public List<ChosenLocation> ChosenLocations { get; set; }

    }

    public class ChosenLocation 
    {
        public int LocationId { get; set; }
        public bool IsChosen { get; set; } = true;

    }

    public class DifferentiateViewModel
    {
        public AssignmentSet ChosenAssignmentSet { get; set; }
        public Class ChosenClass { get; set; }
        public int CurrentSchoolId { get; set; }
        public List<PersonIdAssignmentIdCoupling> PersonAssignmentCouplings { get; set; } = new List<PersonIdAssignmentIdCoupling>();
    }

    public class PersonIdAssignmentIdCoupling
    {
        public int AssignmentId { get; set; }
        public string PersonId { get; set; }
        public bool IsChosen { get; set; } = true;
    }


}
