using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContabilidadeFuncionarios.Application.Dtos
{
    public class LancamentoDto(string Tipo, decimal Valor, string Descricao)
    {
        public string TipoLancamento { get; } = Tipo;
        public decimal Valor { get; } = Valor;
        public string Descricao { get; } = Descricao;
    }
}
