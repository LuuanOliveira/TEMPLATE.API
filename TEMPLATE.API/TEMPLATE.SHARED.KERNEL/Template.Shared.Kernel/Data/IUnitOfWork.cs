using System.Threading.Tasks;

namespace Template.Shared.Kernel.Data
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}