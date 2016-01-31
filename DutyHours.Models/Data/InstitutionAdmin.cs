namespace DutyHours.Models.Data
{
    public partial class InstitutionAdmin : DataModelBase
    {

        public int InstitutionId { get; set; }

        public int UserId { get; set; }

        public virtual Institution Institution { get; set; }

        public virtual User User { get; set; }
    }
}
