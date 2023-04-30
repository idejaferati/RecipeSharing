using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.DataLayer.Models.DTOs.User
{
    public class UserLoginDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        public string Roles { get; set; }
        public string PhoneNumber { get; set; }

        public string password { get; set; }


    }
}
