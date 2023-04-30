using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.DataLayer.Models.Entities
{
    public class Role :BaseEntity
    {
        
        public string Name { get; set; }
        public ICollection<Policy> Policies { get; set; }
    }
}
