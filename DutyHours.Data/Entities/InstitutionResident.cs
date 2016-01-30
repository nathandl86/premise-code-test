namespace DutyHours.Data.Entities
{
    using System.Collections.Generic;

    public partial class InstitutionResident : BaseIdentityEntity
    {        
        public InstitutionResident()
        {
            ResidentShifts = new HashSet<ResidentShift>();
        }        

        public int InstitutionId { get; set; }

        public int UserId { get; set; }

        public virtual Institution Institution { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<ResidentShift> ResidentShifts { get; set; }
    }
}
