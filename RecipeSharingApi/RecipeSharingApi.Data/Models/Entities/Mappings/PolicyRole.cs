using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.DataLayer.Models.Entities.Mappings
{
    public class PolicyRole :BaseEntity
    {
        
        public Guid PolicyId { get; set; }
        public Guid RoleId { get; set; }
        public Policy Policies { get; set; }
        public Role Roles { get; set; }
    }
}
