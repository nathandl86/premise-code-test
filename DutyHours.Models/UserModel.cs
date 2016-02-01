
using System.Collections.Generic;

namespace DutyHours.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public bool IsActive { get; set;}
        public List<int> InstitutionsWhereAdmin { get; set; }
    }
}
