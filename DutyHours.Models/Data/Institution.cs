namespace DutyHours.Models.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Institution : DataModelBase
    {
        public Institution()
        {
            InstitutionAdmins = new HashSet<InstitutionAdmin>();
            InstitutionResidents = new HashSet<InstitutionResident>();
        }
        
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public virtual ICollection<InstitutionAdmin> InstitutionAdmins { get; set; }

        public virtual ICollection<InstitutionResident> InstitutionResidents { get; set; }
    }
}
