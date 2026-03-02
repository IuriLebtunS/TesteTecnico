using Financeiro.Business.Interfaces;
using Financeiro.Business.Entities;
using System;

namespace Financeiro.Business.Services
{

    public class LancamentoFinanceiroService
    {
        private readonly ILancamentoFinanceiroRepository _repository;

        public LancamentoFinanceiroService(ILancamentoFinanceiroRepository repository)
        {
            _repository = repository;
        }

        public void Validar(LancamentoFinanceiro lancamento)
        {
            if (string.IsNullOrWhiteSpace(lancamento.Descricao))
                throw new Exception("Descrição obrigatória");

            if (lancamento.ValorOriginal <= 0)
                throw new Exception("Valor deve ser maior que zero");

            if (string.IsNullOrWhiteSpace(lancamento.Competencia))
                throw new Exception("Competência obrigatória");

            if (lancamento.Tipo == TipoLancamento.Debito)
            {
                if (lancamento.PercentualTaxa <= 0)
                    throw new Exception("Débito exige taxa");

                if (lancamento.PercentualDesconto > 0)
                    throw new Exception("Débito não pode ter desconto");
            }

            if (lancamento.Tipo == TipoLancamento.Credito)
            {
                if (lancamento.PercentualDesconto <= 0)
                    throw new Exception("Crédito exige desconto");

                if (lancamento.PercentualTaxa > 0)
                    throw new Exception("Crédito não pode ter taxa");
            }
        }

        public decimal CalcularValor(LancamentoFinanceiro lancamento)
        {
            if (lancamento.Tipo == TipoLancamento.Debito)
            {
                return lancamento.ValorOriginal + (lancamento.ValorOriginal * (lancamento.PercentualTaxa / 100));
            }

            if (lancamento.Tipo == TipoLancamento.Credito)
            {
                return lancamento.ValorOriginal - (lancamento.ValorOriginal * (lancamento.PercentualDesconto / 100));
            }

            return lancamento.ValorOriginal;
        }

        public void Criar(LancamentoFinanceiro lancamento)
        {
            Validar(lancamento);

            bool existe = _repository.ExisteDuplicado(lancamento.Competencia, lancamento.Descricao, lancamento.Tipo);
            if (existe)
                throw new Exception("Lançamento duplicado");

            lancamento.ValorCalculado = CalcularValor(lancamento);
            lancamento.DataCriacao = DateTime.Now;
            lancamento.Status = StatusLancamento.Aberto;

            _repository.Inserir(lancamento);
        }

        public void Editar(LancamentoFinanceiro lancamento)
        {
            if (lancamento.Status != StatusLancamento.Aberto)
                throw new Exception("Somente lançamentos em aberto podem ser editados");

            Validar(lancamento);

            lancamento.ValorCalculado = CalcularValor(lancamento);

            _repository.Atualizar(lancamento);
        }

        public void Pagar(int id)
        {
            _repository.AtualizarStatus(id, StatusLancamento.Pago, DateTime.Now);
        }

        public void Cancelar(int id)
        {
            _repository.AtualizarStatus(id, StatusLancamento.Cancelado, DateTime.Now);
        }

        public decimal CalcularSaldo()
        {
            var pagos = _repository.ListarPagos();
            decimal saldo = 0;

            foreach (var lancamento in pagos)
            {
                if (lancamento.Tipo == TipoLancamento.Credito)
                    saldo += lancamento.ValorCalculado;
                else
                    saldo -= lancamento.ValorCalculado;
            }

            return saldo;
        }
    }
}

