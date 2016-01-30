namespace DutyHours.Data.Entities
{
    public partial class InstitutionAdmin : BaseIdentityEntity
    {

        public int InstitutionId { get; set; }

        public int UserId { get; set; }

        public virtual Institution Institution { get; set; }

        public virtual User User { get; set; }
    }
}
