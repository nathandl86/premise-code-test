namespace DutyHours.EntityData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InstitutionAdmin
    {
        public int Id { get; set; }

        public int InstitutionId { get; set; }

        public int UserId { get; set; }

        public virtual Institution Institution { get; set; }

        public virtual User User { get; set; }
    }
}
