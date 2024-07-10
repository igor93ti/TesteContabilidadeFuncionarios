using System.Threading.Tasks;
using ContabilidadeFuncionarios.Domain.Interfaces;
using ContabilidadeFuncionarios.Infrastructure.Data;

namespace ContabilidadeFuncionarios.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
