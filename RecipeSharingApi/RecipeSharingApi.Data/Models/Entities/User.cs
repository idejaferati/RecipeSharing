namespace RecipeSharingApi.DataLayer.Models.Entities;
public class User:BaseEntity
{
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }

    public Guid RoleId { get; set; }
    public Role Role { get; set; }
    public string PhoneNumber { get; set; }

    public string SaltedHashPassword { get; set; }
    public string Salt { get; set; }

    public List<Recipe> Recipes { get; set; }



}
