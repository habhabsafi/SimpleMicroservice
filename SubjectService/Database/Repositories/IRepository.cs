using SubjectService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectService.Database.Repositories
{
    public interface IRepository<TEntity,CEntity> where TEntity : IEntity
        where CEntity : IEntity
    {
        IEnumerable<CEntity> GetAll();
        CEntity GetById(int id);
        CEntity GetById(int id, string[] include = null);
        void Add(CEntity entity);
        CEntity Update(CEntity entity);
        IEnumerable<CEntity> GetAll(int page, int counterPerPage, out int totalCount);
        void Delete(int id);
    }
}
