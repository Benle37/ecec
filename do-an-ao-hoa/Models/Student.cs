using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace do_an_ao_hoa.Models
{
    public class Student
    {
        public Object _id { get; set; }
        [Display(Name ="Mã sinh viên")]
        public string code { get; set; }
        [Display(Name = "Họ tên")]
        public string name { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Số điện thoại")]
        public string phone { get; set; }
        [Display(Name = "Địa chỉ")]
        public string address { get; set; }
        [Display(Name = "Hình ảnh")]
        public string image { get; set; }
    }
}