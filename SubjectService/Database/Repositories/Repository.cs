using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SubjectService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectService.Database.Repositories
{
    public class Repository<TEntity,CEntity> : IRepository<TEntity,CEntity>
         where TEntity :class, IEntity
         where CEntity :class, IEntity
    {
        protected readonly DatabaseContext context;
        protected readonly IMapper _mapper;
        private DbSet<TEntity> entities;
        string errorMessage = string.Empty;
        public Repository( DatabaseContext context, IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;
            entities = context.Set<TEntity>();
        }
        public virtual void Delete(int id)
        {
            if (id==0) 
            throw new ArgumentNullException("on delete entity");
            TEntity entity = entities.FirstOrDefault(c => c.Id == id);
            if (entity==null)
                throw new Exception("Entity Not found");
            entities.Remove(entity);
            context.SaveChanges();
        }

        public IEnumerable<CEntity> GetAll()
        {
            return entities.Select(c=>_mapper.Map<CEntity>(c))
                           .AsEnumerable();
        }
        public virtual IEnumerable<TEntity> Filter()
        {
           return  entities.ToList();
        }
        public IEnumerable<CEntity> GetAll(int page,int counterPerPage, out int totalCount)
        {
            totalCount = Filter().Count();
            return Filter().OrderByDescending(c => c.Id)
                .Skip(page * counterPerPage)
                .Take(counterPerPage)
                .ToList()
                .Select(_mapper.Map<CEntity>)
                .ToList();
            //return entities.AsEnumerable();
        }

        public CEntity GetById(int id)
        {
            return _mapper.Map<CEntity> (entities.FirstOrDefault(c => c.Id == id));
        }
        public CEntity GetById (int id , string[] include = null)
        {
            TEntity dbEntity = default;
                var query = context.Set<TEntity>().AsNoTracking();
            if (include != null)
            {
                for(int i=0; i < include.Length; i++)
                {
                    query = query.Include(include[i]);
                }
            }
                dbEntity = query.FirstOrDefault(e => e.Id == id);
            return dbEntity!=null? _mapper.Map<CEntity>(dbEntity):null;
            
        }
        public virtual CEntity Add(CEntity entity)
        {
            if(entity==null)
            throw new ArgumentNullException("null entity when adding");
            TEntity dbEntity = _mapper.Map<TEntity>(entity);
          entities.Add(dbEntity);
            if (context.SaveChanges() > 0)
            {
                return GetById(dbEntity.Id);
            }
            else return default;
        }

        public virtual CEntity Update(CEntity entity)
        {
            if(entity==null)
            throw new ArgumentNullException("null entity when updating");
            entities.Update(_mapper.Map<TEntity>(entity));
            context.SaveChanges();
            return GetById(entity.Id);
        }
    }
}
