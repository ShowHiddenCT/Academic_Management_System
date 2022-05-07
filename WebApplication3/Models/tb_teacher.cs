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
        [Display(Name = "��ʦ����")]
        public string TeacherNo { get; set; }

        [StringLength(20)]
        [Display(Name = "����")]
        public string TeacherPasswd { get; set; }

        [StringLength(6)]
        [Display(Name = "Ժϵ")]
        public string DeptNo { get; set; }

        [StringLength(20)]
        [Display(Name = "����")]
        public string TeacherName { get; set; }

        [StringLength(2)]
        [Display(Name = "�Ա�")]
        public string TeacherSex { get; set; }

        [Display(Name = "��������")]
        public DateTime? TeacherBirthday { get; set; }

        [StringLength(20)]
        [Display(Name = "ְ��")]
        public string TeacherTitle { get; set; }

        [StringLength(50)]
        [Display(Name = "��Ƭ")]
        public string Teacherphoto { get; set; }

        [StringLength(18)]
        [Display(Name = "���֤��")]
        public string TeacherId { get; set; }

        [StringLength(11)]
        [Display(Name = "��ϵ��ʽ")]
        public string TeacherTel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_course> tb_course { get; set; }

        public virtual tb_dept tb_dept { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_stucourse> tb_stucourse { get; set; }
    }
}
