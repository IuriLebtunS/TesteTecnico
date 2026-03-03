using Financeiro.Business.Interfaces;
using Financeiro.Business.Entities;
using System;

namespace Financeiro.Business.Services
{

    public class LancamentoFinanceiroService
    {
        private readonly ILancamentoFinanceiroRepository _repositorio;

        public LancamentoFinanceiroService(ILancamentoFinanceiroRepository repository)
        {
            _repositorio = repository;
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

            bool existe = _repositorio.ExisteDuplicado(lancamento.Competencia, lancamento.Descricao, lancamento.Tipo);
            if (existe)
                throw new Exception("Lançamento duplicado");

            lancamento.ValorCalculado = CalcularValor(lancamento);
            lancamento.DataCriacao = DateTime.Now;
            lancamento.Status = StatusLancamento.Aberto;

            _repositorio.Inserir(lancamento);
        }

        public void Editar(LancamentoFinanceiro lancamentoEditado)
        {
            var lancamentoAtual = _repositorio.ObterPorId(lancamentoEditado.Id);

            if (lancamentoAtual == null)
                throw new Exception("Lançamento não encontrado.");

            if (lancamentoAtual.Status != StatusLancamento.Aberto)
                throw new Exception("Somente lançamentos em aberto podem ser editados.");

            lancamentoAtual.Descricao = lancamentoEditado.Descricao;
            lancamentoAtual.ValorOriginal = lancamentoEditado.ValorOriginal;
            lancamentoAtual.PercentualTaxa = lancamentoEditado.PercentualTaxa;
            lancamentoAtual.PercentualDesconto = lancamentoEditado.PercentualDesconto;
            lancamentoAtual.Tipo = lancamentoEditado.Tipo;
            lancamentoAtual.Competencia = lancamentoEditado.Competencia;

            lancamentoAtual.ValorCalculado = CalcularValor(lancamentoAtual);

            Validar(lancamentoAtual);

            _repositorio.Atualizar(lancamentoAtual);
        }

        public void Pagar(int id)
        {
            var lancamento = _repositorio.ObterPorId(id);

            if (lancamento == null)
                throw new Exception("Lançamento não encontrado.");

            if (lancamento.Status != StatusLancamento.Aberto)
                throw new Exception("Somente lançamentos em aberto podem ser pagos.");

            _repositorio.AtualizarStatus(id, StatusLancamento.Pago, DateTime.Now);
        }

        public void Cancelar(int id)
        {
            var lancamento = _repositorio.ObterPorId(id);

            if (lancamento == null)
                throw new Exception("Lançamento não encontrado.");

            if (lancamento.Status != StatusLancamento.Aberto)
                throw new Exception("Somente lançamentos em aberto podem ser cancelados.");

            _repositorio.AtualizarStatus(id, StatusLancamento.Cancelado, DateTime.Now);
        }

        public decimal CalcularSaldo()
        {
            var lancamentos = _repositorio.Listar();

            decimal saldo = 0;

            foreach (var lancamento in lancamentos)
            {
                if (lancamento.Status == StatusLancamento.Pago)
                {
                    if (lancamento.Tipo == TipoLancamento.Credito)
                        saldo += lancamento.ValorCalculado;
                    else
                        saldo -= lancamento.ValorCalculado;
                }
            }

            return saldo;
        }
    }
}

