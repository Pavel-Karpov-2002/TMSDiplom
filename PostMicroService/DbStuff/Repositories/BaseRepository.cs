using PostMicroService.DbStuff.Interfaces;
using PostMicroService.DbStuff.Models;
using Microsoft.EntityFrameworkCore;

namespace PostMicroService.DbStuff.Repositories
{
    public class BaseRepository<DbModel> : IGetEntities<DbModel>, IGetEntitiesAsync<DbModel>, IAddEntity<DbModel>, IAddEntityAsync<DbModel>,
        IDeleteEntity<DbModel>, IDeleteEntityAsync<DbModel>, IUpdate<DbModel> where DbModel : BaseModel
    {
        protected readonly PostNetworkWebDbContext _context;
        protected readonly DbSet<DbModel> _entyties;

        public BaseRepository(PostNetworkWebDbContext context)
        {
            _context = context;
            _entyties = _context.Set<DbModel>();
        }

        public virtual DbModel? GetById(int id)
        {
            return _entyties.SingleOrDefault(ent => ent.Id == id);
        }

        public virtual int Add(DbModel entity)
        {
            _entyties.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public virtual void DeleteById(int id)
        {
            var entity = _entyties.First(x => x.Id == id);
            _entyties.Remove(entity);
            _context.SaveChanges();
        }

        public virtual IEnumerable<DbModel> GetAll()
        {
            return _entyties.ToList();
        }

        public virtual Task<DbModel?>? GetByIdAsync(int id)
        {
            return _entyties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<List<DbModel>> GetAllAsync()
        {
            return await _entyties.ToListAsync();
        }

        public virtual async Task AddAsync(DbModel entity)
        {
            await _context.AddAsync(entity);
            _context.SaveChangesAsync();
        }

        public virtual async Task DeleteByIdAsync(int id)
        {
            var entity = await _entyties.FirstAsync(x => x.Id == id);
            _entyties.Remove(entity);
            _context.SaveChangesAsync();
        }

        public virtual DbModel Update(DbModel entity)
        {
            var updatedEntity = _entyties.Update(entity);
            _context.SaveChanges();
            return updatedEntity.Entity;
        }

        public virtual async Task DeleteByEntityAsync(DbModel entity)
        {
            var findedEntity = await _entyties.FirstAsync(x => x.Equals(entity));
            _entyties.Remove(entity);
            _context.SaveChangesAsync();
        }

        public virtual void DeleteByEntity(DbModel entity)
        {
            var findedEntity = _entyties.First(x => x.Equals(entity));
            _entyties.Remove(findedEntity);
            _context.SaveChanges();
        }

        public virtual bool Any() => _entyties.Any();
    }
}
