namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_stucourse
    {
        public int Id { get; set; }

        [StringLength(10)]
        [Display(Name = "ѧ��")]
        public string StudentNo { get; set; }

        [StringLength(8)]
        [Display(Name = "�γ̺�")]
        public string CourseNo { get; set; }

        [StringLength(10)]
        [Display(Name = "�ڿ���ʦ")]
        public string TeacherNo { get; set; }

        [Display(Name = "����")]
        public int? Grade { get; set; }

        public virtual tb_course tb_course { get; set; }

        public virtual tb_student tb_student { get; set; }

        public virtual tb_teacher tb_teacher { get; set; }
    }
}
