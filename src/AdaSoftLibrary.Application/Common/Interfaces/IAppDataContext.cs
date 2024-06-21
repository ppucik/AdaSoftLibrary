namespace AdaSoftLibrary.Application.Common.Interfaces;

public interface IAppDataContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
