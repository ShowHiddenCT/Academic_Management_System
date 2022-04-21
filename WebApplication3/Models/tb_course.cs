namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_course()
        {
            tb_stucourse = new HashSet<tb_stucourse>();
        }

        [Key]
        [StringLength(8)]
        [Display(Name ="�γ̺�")]
        public string CourseNo { get; set; }

        [StringLength(20)]
        [Display(Name = "�γ�����")]
        public string CourseName { get; set; }

        [StringLength(10)]
        [Display(Name = "�ڿ���ʦ")]
        public string CourseTeacher { get; set; }

        [Display(Name = "ѧ��")]
        public int? CourseCredit { get; set; }

        [StringLength(50)]
        [Display(Name = "�Ͽ�ʱ��")]
        public string CourseTime { get; set; }

        [StringLength(50)]
        [Display(Name = "�Ͽεص�")]
        public string CoursePlace { get; set; }

        [StringLength(20)]
        [Display(Name = "�γ�����")]
        public string CourseType { get; set; }

        [Display(Name = "��ѧʱ")]
        public int? CourseHour { get; set; }

        public virtual tb_teacher tb_teacher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_stucourse> tb_stucourse { get; set; }
    }
}
