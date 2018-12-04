using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot.Models
{
    public class LoginModel
    {
        public string SelectedPath { get; set; } = "www.999dice.com";
        public string Login { get; set; } = "login";
        public string Password { get; set; } = "";
        public int GoogleCode { get; set; } = 0;
    }
}
