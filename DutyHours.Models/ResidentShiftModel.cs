

using System;

namespace DutyHours.Models
{
    public class ResidentShiftModel
    {
        public int Id{ get; set; }
        public int InstitutionResidentId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }

        public bool OverrideAcknowleded { get; set; }
    }
}
