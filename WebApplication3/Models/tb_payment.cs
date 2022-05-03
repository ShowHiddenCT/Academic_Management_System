namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_payment
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string StudentNo { get; set; }

        public virtual tb_student tb_student { get; set; }
    }
}
