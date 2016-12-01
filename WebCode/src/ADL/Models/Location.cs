using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ADL.Models
{
    public class Location
    {
        public int LocationId { get; set; }

        [Required(ErrorMessage = "Venligst indtast en titel")]
        [MinLengthAttribute(2)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Venligst vælg HVOR lokationen er!")]
        [MinLengthAttribute(2)]
        public string Description { get; set; }

        //public int AttachedAssignmentId { get; set; }

        private Dictionary<string, int> Attachments { get; set; }

        /*returns true if the attachment was valid, and false if not*/
        public bool AddAttachmentToLocation(string personId, int AssignmentId)
        {
            if (Attachments[personId] != 0)
            {
                Attachments.Add(personId, AssignmentId);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveAttachmentFromLocation(string personId)
        {
            Attachments.Remove(personId);
        }

        public int GetAssignmentIdFromPersonId(string personId)
        {
            return Attachments[personId];
        }

        public bool GetPersonConnectedToLocation(string personId)
        {
            if (Attachments.ContainsKey(personId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
