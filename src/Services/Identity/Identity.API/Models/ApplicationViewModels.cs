// Author: 	Liang Zhinian
// On:		2020/9/19
//
using System;
namespace Identity.API.Models
{
    public class ApplicationViewModel
    {
        public int Id { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string ApplicationName { get; set; }

        public string ApplicationDescription { get; set; }

        public string HomepageURL { get; set; }

        public string AuthorizationCallbackURL { get; set; }
    }

    public class CreateApplicationViewModel
    {
        public string ApplicationName { get; set; }

        public string ApplicationDescription { get; set; }

        public string HomepageURL { get; set; }

        public string AuthorizationCallbackURL { get; set; }
    }
}
