﻿namespace RecipeSharingApi.DataLayer.Models.DTOs
{
    public class UserCreateDTO
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        public string Roles { get; set; }
        public string PhoneNumber { get; set; }

        public string SaltedHashPassword { get; set; }
        public string Salt { get; set; }
        
    }
}
