namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_student()
        {
            tb_payment = new HashSet<tb_payment>();
            tb_stucourse = new HashSet<tb_stucourse>();
        }

        [Key]
        [StringLength(10)]
        [Display(Name = "学号")]
        public string StudentNo { get; set; }

        [StringLength(20)]
        [Display(Name = "密码")]
        public string StudentPasswd { get; set; }

        [StringLength(4)]
        [Display(Name = "专业")]
        public string MajorNo { get; set; }

        [StringLength(20)]
        [Display(Name = "名称")]
        public string StudentName { get; set; }

        [StringLength(2)]
        [Display(Name = "性别")]
        public string StudentSex { get; set; }

        [Display(Name = "出生日期")]
        public DateTime? StudentBirthday { get; set; }

        [StringLength(50)]
        [Display(Name = "照片")]
        public string Studentphoto { get; set; }

        [StringLength(18)]
        [Display(Name = "身份证号")]
        public string StudentId { get; set; }

        [StringLength(11)]
        [Display(Name = "联系方式")]
        public string StudentTel { get; set; }

        public virtual tb_major tb_major { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_payment> tb_payment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_stucourse> tb_stucourse { get; set; }
    }
}
