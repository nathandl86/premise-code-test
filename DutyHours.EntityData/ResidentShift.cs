namespace DutyHours.EntityData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ResidentShift
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int InstitutionResidentId { get; set; }

        public DateTime EntryDateTimeUtc { get; set; }

        public DateTime StartDateTimeUtc { get; set; }

        public DateTime? EndDateTimeUtc { get; set; }

        public virtual InstitutionResident InstitutionResident { get; set; }
    }
}
