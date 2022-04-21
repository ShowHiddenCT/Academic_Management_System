namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_Admin
    {
        [Key]
        [StringLength(5)]
        public string AdminNo { get; set; }

        [StringLength(20)]
        public string AdminPasswd { get; set; }
    }
}
