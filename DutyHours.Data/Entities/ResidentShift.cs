namespace DutyHours.Data.Entities
{
    using System;

    public partial class ResidentShift : BaseIdentityEntity
    {
        public int InstitutionResidentId { get; set; }

        public DateTime EntryDateTimeUtc { get; set; }

        public DateTime StartDateTimeUtc { get; set; }

        public DateTime? EndDateTimeUtc { get; set; }

        public virtual InstitutionResident InstitutionResident { get; set; }
    }
}
