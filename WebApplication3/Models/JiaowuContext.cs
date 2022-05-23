using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApplication3.Models
{
    public partial class JiaowuContext : DbContext
    {
        public JiaowuContext()
            : base("name=JiaowuContext")
        {
        }

        public virtual DbSet<tb_Admin> tb_Admin { get; set; }
        public virtual DbSet<tb_course> tb_course { get; set; }
        public virtual DbSet<tb_dept> tb_dept { get; set; }
        public virtual DbSet<tb_expense> tb_expense { get; set; }
        public virtual DbSet<tb_major> tb_major { get; set; }
        public virtual DbSet<tb_payment> tb_payment { get; set; }
        public virtual DbSet<tb_stucourse> tb_stucourse { get; set; }
        public virtual DbSet<tb_student> tb_student { get; set; }
        public virtual DbSet<tb_teacher> tb_teacher { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tb_Admin>()
                .Property(e => e.AdminNo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tb_Admin>()
                .Property(e => e.AdminPasswd)
                .IsUnicode(false);

            modelBuilder.Entity<tb_course>()
                .Property(e => e.CourseNo)
                .IsUnicode(false);

            modelBuilder.Entity<tb_course>()
                .Property(e => e.CourseTeacher)
                .IsUnicode(false);

            modelBuilder.Entity<tb_dept>()
                .Property(e => e.DeptNo)
                .IsUnicode(false);

            modelBuilder.Entity<tb_dept>()
                .Property(e => e.DeptTel)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tb_expense>()
                .Property(e => e.ExpenseNo)
                .IsUnicode(false);

            modelBuilder.Entity<tb_major>()
                .Property(e => e.MajorNo)
                .IsUnicode(false);

            modelBuilder.Entity<tb_major>()
                .Property(e => e.DeptNo)
                .IsUnicode(false);

            modelBuilder.Entity<tb_payment>()
                .Property(e => e.StudentNo)
                .IsUnicode(false);

            modelBuilder.Entity<tb_stucourse>()
                .Property(e => e.StudentNo)
                .IsUnicode(false);

            modelBuilder.Entity<tb_stucourse>()
                .Property(e => e.CourseNo)
                .IsUnicode(false);

            modelBuilder.Entity<tb_stucourse>()
                .Property(e => e.TeacherNo)
                .IsUnicode(false);

            modelBuilder.Entity<tb_student>()
                .Property(e => e.StudentNo)
                .IsUnicode(false);

            modelBuilder.Entity<tb_student>()
                .Property(e => e.StudentPasswd)
                .IsUnicode(false);

            modelBuilder.Entity<tb_student>()
                .Property(e => e.MajorNo)
                .IsUnicode(false);

            modelBuilder.Entity<tb_student>()
                .Property(e => e.StudentId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tb_student>()
                .Property(e => e.StudentTel)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tb_teacher>()
                .Property(e => e.TeacherNo)
                .IsUnicode(false);

            modelBuilder.Entity<tb_teacher>()
                .Property(e => e.TeacherPasswd)
                .IsUnicode(false);

            modelBuilder.Entity<tb_teacher>()
                .Property(e => e.DeptNo)
                .IsUnicode(false);

            modelBuilder.Entity<tb_teacher>()
                .Property(e => e.TeacherId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tb_teacher>()
                .Property(e => e.TeacherTel)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tb_teacher>()
                .HasMany(e => e.tb_course)
                .WithOptional(e => e.tb_teacher)
                .HasForeignKey(e => e.CourseTeacher);
        }
    }
}
