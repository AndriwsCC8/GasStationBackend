using System.Collections.Generic;
using System.Threading.Tasks;
using GasStation.Application.Interfaces;
using GasStation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly GasStationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(GasStationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
