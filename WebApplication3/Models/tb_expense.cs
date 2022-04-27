namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_expense
    {
        [Key]
        [StringLength(10)]
        [Display(Name = "费用号")]
        public string ExpenseNo { get; set; }

        [StringLength(20)]
        [Display(Name = "费用名称")]
        public string ExpenseName { get; set; }

        public int? ExpenseNum { get; set; }
    }
}
