using AdaSoftLibrary.Domain.Entities;

namespace AdaSoftLibrary.Application.Common.Interfaces;

public interface IAppDataContext
{
    /// <summary>
    /// Data knihovna
    /// </summary>
    public Library Library { get; init; }

    /// <summary>
    /// Uloží všetky zmeny v danom kontexte do súboru
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
