using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Wordless.Models;

namespace Wordless.ViewModel
{
    public class AdminViewModel
    {
       
        public string username { get; set; }

        public List<User> UserResult { get; set; }

        public AdminViewModel()
        {
            username = "";

            UserResult = new List<User>();
        }
    }
}