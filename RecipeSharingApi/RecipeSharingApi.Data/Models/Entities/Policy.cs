using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.DataLayer.Models.Entities
{
    public class Policy:BaseEntity
    {
        
        public string Name { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
