using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.Models;

namespace WebApplication3.Areas.teacher.Model
{
    public class LayuiModel
    {
        public int code { get; set; }
        public int count { get; set; }

        public List<tb_teacher> data = new List<tb_teacher>();
    }
}