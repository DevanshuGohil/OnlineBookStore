using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Book_Store.Models
{
    public class UserInfo
    {
        [Key]
        [Required]
        public int UserInfoId { get; set; }
        [Required]
        public string Username { set; get; }
        [Required]
        [EmailAddress]
        public string emailId { set; get; }
        [Required]
        [DataType(DataType.Password)]
        public string password { set; get; }
        [Required]
        public int MobileNo { set; get; }
        [Required]
        public string city { set; get; }
        public string role { set; get; }

    }
}