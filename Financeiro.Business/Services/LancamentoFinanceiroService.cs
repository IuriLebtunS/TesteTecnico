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

        public void Validar(LancamentoFinanceiro l)
        {
            if (string.IsNullOrWhiteSpace(l.Descricao))
                throw new Exception("Descrição obrigatória");

            if (l.ValorOriginal <= 0)
                throw new Exception("Valor deve ser maior que zero");

            if (string.IsNullOrWhiteSpace(l.Competencia))
                throw new Exception("Competência obrigatória");

            if (l.Tipo == TipoLancamento.Debito)
            {
                if (l.PercentualTaxa <= 0)
                    throw new Exception("Débito exige taxa");

                if (l.PercentualDesconto > 0)
                    throw new Exception("Débito não pode ter desconto");
            }

            if (l.Tipo == TipoLancamento.Credito)
            {
                if (l.PercentualDesconto <= 0)
                    throw new Exception("Crédito exige desconto");

                if (l.PercentualTaxa > 0)
                    throw new Exception("Crédito não pode ter taxa");
            }
        }

        public decimal CalcularValor(LancamentoFinanceiro l)
        {
            if (l.Tipo == TipoLancamento.Debito)
            {
                return l.ValorOriginal + (l.ValorOriginal * (l.PercentualTaxa / 100));
            }

            if (l.Tipo == TipoLancamento.Credito)
            {
                return l.ValorOriginal - (l.ValorOriginal * (l.PercentualDesconto / 100));
            }

            return l.ValorOriginal;
        }

        public void Criar(LancamentoFinanceiro l)
        {
            Validar(l);

            bool existe = _repository.ExisteDuplicado(l.Competencia, l.Descricao, l.Tipo);
            if (existe)
                throw new Exception("Lançamento duplicado");

            l.ValorCalculado = CalcularValor(l);
            l.DataCriacao = DateTime.Now;
            l.Status = StatusLancamento.Aberto;

            _repository.Inserir(l);
        }

        public void Editar(LancamentoFinanceiro l)
        {
            if (l.Status != StatusLancamento.Aberto)
                throw new Exception("Somente lançamentos em aberto podem ser editados");

            Validar(l);

            l.ValorCalculado = CalcularValor(l);

            _repository.Atualizar(l);
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

            foreach (var l in pagos)
            {
                if (l.Tipo == TipoLancamento.Credito)
                    saldo += l.ValorCalculado;
                else
                    saldo -= l.ValorCalculado;
            }

            return saldo;
        }
    }
}

