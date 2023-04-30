
using RecipeSharingApi.DataLayer.Models.Entities;

namespace RecipeSharingApi.BusinessLogic.Helpers.Extensions; 
public class TagNameComparer : IEqualityComparer<Tag>
{
    public bool Equals(Tag left, Tag right)
    {
        if(string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return false;
    }

    public int GetHashCode(Tag tag)
    {
        return tag.Name.GetHashCode();
    }
}
