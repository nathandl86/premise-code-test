
namespace DutyHours.Models
{
    public class InstitutionResidentModel
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }

    }
}
