namespace DutyHours.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User : DataModelBase
    {
        public User()
        {
            InstitutionAdmins = new HashSet<InstitutionAdmin>();
            InstitutionResidents = new HashSet<InstitutionResident>();
        }
        
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        public bool Active { get; set; }

        public virtual ICollection<InstitutionAdmin> InstitutionAdmins { get; set; }

        public virtual ICollection<InstitutionResident> InstitutionResidents { get; set; }
    }
}
