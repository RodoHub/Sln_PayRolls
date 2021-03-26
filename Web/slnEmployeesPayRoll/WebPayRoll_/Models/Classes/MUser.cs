using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPayRoll_.Controllers;
namespace WebPayRoll_.Models.Classes
{
    public class MUser
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string Token { get; set; }
    }
}