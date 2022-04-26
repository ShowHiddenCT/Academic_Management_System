namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_dept
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_dept()
        {
            tb_major = new HashSet<tb_major>();
            tb_teacher = new HashSet<tb_teacher>();
        }

        [Key]
        [StringLength(6)]
        [Display(Name = "院系号")]
        public string DeptNo { get; set; }

        [StringLength(20)]
        [Display(Name = "院系名称")]
        public string DeptName { get; set; }

        [StringLength(20)]
        [Display(Name = "学院负责人")]
        public string DeptChairman { get; set; }

        [StringLength(11)]
        [Display(Name = "负责人联系方式")]
        public string DeptTel { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "学院介绍")]
        public string DeptDesc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_major> tb_major { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_teacher> tb_teacher { get; set; }
    }
}
