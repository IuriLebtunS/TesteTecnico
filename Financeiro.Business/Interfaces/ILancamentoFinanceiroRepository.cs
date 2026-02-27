using Financeiro.Business.Entities;
using System.Collections.Generic;
using System;

namespace Financeiro.Business.Interfaces
{
    public interface ILancamentoFinanceiroRepository
    {
        void Inserir(LancamentoFinanceiro l);
        void Atualizar(LancamentoFinanceiro l);
        void AtualizarStatus(int id, StatusLancamento status, DateTime data);
        bool ExisteDuplicado(string competencia, string descricao, TipoLancamento tipo);
        List<LancamentoFinanceiro> Listar();
        List<LancamentoFinanceiro> ListarPagos();
    }
}