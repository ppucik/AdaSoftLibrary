using Microsoft.EntityFrameworkCore;

namespace AdaSoftLibrary.Application.Common.Interfaces;

public interface IAppDataContext
{
    /// <summary>
    /// Generický DbSet (tabuľka)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public DbSet<TEntity> Set<TEntity>() where TEntity : class;

    /// <summary>
    /// Uloží všetky zmeny v danom kontexte do databázy
    /// </summary>
    /// <returns></returns>
    public int SaveChanges();

    /// <summary>
    /// Uloží asynchrónne všetky zmeny v danom kontexte do databázy
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
