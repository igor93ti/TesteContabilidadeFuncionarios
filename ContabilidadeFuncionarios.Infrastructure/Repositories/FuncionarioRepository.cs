using ContabilidadeFuncionarios.Domain.Entities;
using ContabilidadeFuncionarios.Domain.Interfaces.Repositories;
using ContabilidadeFuncionarios.Infrastructure.Data;

namespace ContabilidadeFuncionarios.Infrastructure.Repositories
{
    public class FuncionarioRepository : Repository<Funcionario>, IFuncionarioRepository
    {
        public FuncionarioRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
