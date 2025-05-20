using Business.Models;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract partial class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();


    public virtual async Task<RepositoryResult<bool>> AddAsync(TEntity entity)
    {
        if (entity == null)
            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 400, Error = "Entity can´t be null" };

        try
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Succeeded = true, StatusCode = 201 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 500, Error = ex.Message };
        }
    }

    public virtual async Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync()
    {
        var entities = await _dbSet.ToListAsync();
        return new RepositoryResult<IEnumerable<TEntity>> { Succeeded = true, StatusCode = 200, Result = entities };
    }

    public virtual async Task<RepositoryResult<TEntity>> GetAsync(Expression<Func<TEntity, bool>> findBy)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(findBy);
        return entity == null
            ? new RepositoryResult<TEntity> { Succeeded = false, StatusCode = 404, Error = "Entity not found" }
            : new RepositoryResult<TEntity> { Succeeded = true, StatusCode = 200, Result = entity };
    }

    public virtual async Task<RepositoryResult<bool>> ExistsAsync(Expression<Func<TEntity, bool>> findBy)
    {
        var exists = await _dbSet.AnyAsync(findBy);
        return !exists
            ? new RepositoryResult<bool> { Succeeded = false, StatusCode = 404, Error = "Entity not found" }
            : new RepositoryResult<bool> { Succeeded = true, StatusCode = 200 };
    }

    public virtual async Task<RepositoryResult<bool>> UpdateAsync(TEntity entity)
    {
        if (entity == null)
            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 400, Error = "Entity can´t be null" };

        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Succeeded = true, StatusCode = 200 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 500, Error = ex.Message };
        }
    }

    public virtual async Task<RepositoryResult<bool>> DeleteAsync(TEntity entity)
    {
        if (entity == null)
            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 400, Error = "Entity can´t be null" };

        try
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Succeeded = true, StatusCode = 200 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new RepositoryResult<bool> { Succeeded = false, StatusCode = 500, Error = ex.Message };
        }
    }



}