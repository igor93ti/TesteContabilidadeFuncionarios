using System.Threading.Tasks;

namespace ContabilidadeFuncionarios.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
