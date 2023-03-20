using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeSharingApi.DataLayer.Data.Repository.IRepository;
using RecipeSharingApi.DataLayer.Models.Entities;

namespace RecipeSharingApi.DataLayer.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IRecipeSharingRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        bool Complete();
    }
}
