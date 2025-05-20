using Business.Models;
using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

    public class EventRepository(DataContext context) : BaseRepository<EventEntity>(context), IEventRepository
    {
        public override async Task<RepositoryResult<IEnumerable<EventEntity>>> GetAllAsync()
        {
            var entities = await _dbSet.Include(x => x.Packages).ToListAsync();
            return new RepositoryResult<IEnumerable<EventEntity>> { Succeeded = true, StatusCode = 200, Result = entities };
        }

        public override async Task<RepositoryResult<EventEntity>> GetAsync(Expression<Func<EventEntity, bool>> findBy)
        {
            var entity = await _dbSet.Include(x => x.Packages).FirstOrDefaultAsync(findBy);
            return entity == null
                ? new RepositoryResult<EventEntity> { Succeeded = false, StatusCode = 404, Error = "Entity not found" }
                : new RepositoryResult<EventEntity> { Succeeded = true, StatusCode = 200, Result = entity };
        }
    }
