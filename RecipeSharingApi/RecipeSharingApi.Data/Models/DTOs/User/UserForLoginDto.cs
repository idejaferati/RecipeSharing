using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.DataLayer.Models.DTOs.User
{
    public class UserForLoginDto
    {

        //name should be changed
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
