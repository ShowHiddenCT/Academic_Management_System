namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_major
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_major()
        {
            tb_student = new HashSet<tb_student>();
        }

        [Key]
        [StringLength(4)]
        [Display(Name = "רҵ��")]
        public string MajorNo { get; set; }

        [StringLength(6)]
        [Display(Name = "����ѧԺ")]
        public string DeptNo { get; set; }

        [StringLength(20)]
        [Display(Name = "רҵ����")]
        public string MajorName { get; set; }

        [StringLength(10)]
        [Display(Name = "��Ա")]
        public string MajorAssistant { get; set; }

        [StringLength(11)]
        [Display(Name = "��Ա��ϵ��ʽ")]
        public string MajorTel { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "רҵ����")]
        public string MjorDetail { get; set; }

        public virtual tb_dept tb_dept { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_student> tb_student { get; set; }
    }
}
