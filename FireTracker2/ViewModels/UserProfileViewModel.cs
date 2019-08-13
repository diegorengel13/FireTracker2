using FireTracker2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FireTracker2.ViewModels
{
    public class UserProfileViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{LastName},{FirstName}";
            }
        }
        public string Email { get; set; }
        public string DisplayName { get; set; }

        public string AvatarUrl { get; set; }
        public ICollection<Project> Projects { get; set; }

        public UserProfileViewModel()
        {
            Projects = new HashSet<Project>();

        }

    }
}