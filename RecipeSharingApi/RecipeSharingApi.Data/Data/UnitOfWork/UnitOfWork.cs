using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeSharingApi.DataLayer.Data;
using RecipeSharingApi.DataLayer.Data.Repository;
using RecipeSharingApi.DataLayer.Data.Repository.IRepository;
using RecipeSharingApi.DataLayer.Data.UnitOfWork;
using RecipeSharingApi.DataLayer.Models.Entities;

namespace RecipeSharingApi.Data.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RecipeSharingDbContext _context;

        private Hashtable _repositories;

        public UnitOfWork(RecipeSharingDbContext context)
        {
            _context = context;
        }
        public bool Complete()
        {
            var numberOfAffectedRows = _context.SaveChanges();
            return numberOfAffectedRows > 0;
        }

        public IRecipeSharingRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RecipeSharingRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return _repositories[type] as IRecipeSharingRepository<TEntity>;
        }
    }
}
