namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_teacher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_teacher()
        {
            tb_course = new HashSet<tb_course>();
            tb_stucourse = new HashSet<tb_stucourse>();
        }

        [Key]
        [StringLength(10)]
        [Display(Name = "教师工号")]
        public string TeacherNo { get; set; }

        [StringLength(20)]
        [Display(Name = "密码")]
        public string TeacherPasswd { get; set; }

        [StringLength(6)]
        [Display(Name = "院系")]
        public string DeptNo { get; set; }

        [StringLength(20)]
        [Display(Name = "名称")]
        public string TeacherName { get; set; }

        [StringLength(2)]
        [Display(Name = "性别")]
        public string TeacherSex { get; set; }

        [Display(Name = "出生日期")]
        public DateTime? TeacherBirthday { get; set; }

        [StringLength(20)]
        [Display(Name = "职称")]
        public string TeacherTitle { get; set; }

        [StringLength(50)]
        [Display(Name = "照片")]
        public string Teacherphoto { get; set; }

        [StringLength(18)]
        [Display(Name = "身份证号")]
        public string TeacherId { get; set; }

        [StringLength(11)]
        [Display(Name = "联系方式")]
        public string TeacherTel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_course> tb_course { get; set; }

        public virtual tb_dept tb_dept { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_stucourse> tb_stucourse { get; set; }
    }
}
