using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wordless.Models;

namespace Wordless.ViewModel
{
    public class UserPageViewModel
    {
        public string username { get; set; }

        public List<User> UserResult { get; set; }

        public UserPageViewModel()
        {
            username = "";

            UserResult = new List<User>();
        }
    }
}