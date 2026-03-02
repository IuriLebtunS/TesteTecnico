using Financeiro.Business.Entities;
using Financeiro.Business.Interfaces;
using Financeiro.Data.Infra;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Financeiro.Data.Repositories
{
    public class LancamentoFinanceiroRepository : ILancamentoFinanceiroRepository
    {
        public void Inserir(LancamentoFinanceiro lancamento)
        {
            using (var con = DbConnectionFactory.Create())
            {
                con.Open();

                var cmd = new SqlCommand(@"
                INSERT INTO LancamentoFinanceiro
                (Descricao, Tipo, ValorOriginal, PercentualTaxa, PercentualDesconto, ValorCalculado,
                 DataLancamento, DataCriacao, Competencia, Status)
                VALUES
                (@Descricao,@Tipo,@ValorOriginal,@PercentualTaxa,@PercentualDesconto,@ValorCalculado,
                 @DataLancamento,@DataCriacao,@Competencia,@Status)", con);

                cmd.Parameters.AddWithValue("@Descricao", lancamento.Descricao);
                cmd.Parameters.AddWithValue("@Tipo", (int)lancamento.Tipo);
                cmd.Parameters.AddWithValue("@ValorOriginal", lancamento.ValorOriginal);
                cmd.Parameters.AddWithValue("@PercentualTaxa", lancamento.PercentualTaxa);
                cmd.Parameters.AddWithValue("@PercentualDesconto", lancamento.PercentualDesconto);
                cmd.Parameters.AddWithValue("@ValorCalculado", lancamento.ValorCalculado);
                cmd.Parameters.AddWithValue("@DataLancamento", lancamento.DataLancamento);
                cmd.Parameters.AddWithValue("@DataCriacao", lancamento.DataCriacao);
                cmd.Parameters.AddWithValue("@Competencia", lancamento.Competencia);
                cmd.Parameters.AddWithValue("@Status", (int)lancamento.Status);

                cmd.ExecuteNonQuery();
            }
        }

        public void Atualizar(LancamentoFinanceiro lancamento)
        {
            using (var con = DbConnectionFactory.Create())
            {
                con.Open();

                var cmd = new SqlCommand(@"
                UPDATE LancamentoFinanceiro SET
                Descricao=@Descricao,
                Tipo=@Tipo,
                ValorOriginal=@ValorOriginal,
                PercentualTaxa=@PercentualTaxa,
                PercentualDesconto=@PercentualDesconto,
                ValorCalculado=@ValorCalculado,
                DataLancamento=@DataLancamento,
                Competencia=@Competencia
                WHERE Id=@Id AND Status=@Status", con);

                cmd.Parameters.AddWithValue("@Descricao", lancamento.Descricao);
                cmd.Parameters.AddWithValue("@Tipo", (int)lancamento.Tipo);
                cmd.Parameters.AddWithValue("@ValorOriginal", lancamento.ValorOriginal);
                cmd.Parameters.AddWithValue("@PercentualTaxa", lancamento.PercentualTaxa);
                cmd.Parameters.AddWithValue("@PercentualDesconto", lancamento.PercentualDesconto);
                cmd.Parameters.AddWithValue("@ValorCalculado", lancamento.ValorCalculado);
                cmd.Parameters.AddWithValue("@DataLancamento", lancamento.DataLancamento);
                cmd.Parameters.AddWithValue("@Competencia", lancamento.Competencia);
                cmd.Parameters.AddWithValue("@Id", lancamento.Id);
                cmd.Parameters.AddWithValue("@Status", (int)StatusLancamento.Aberto);

                cmd.ExecuteNonQuery();
            }
        }

        public void AtualizarStatus(int id, StatusLancamento status, DateTime data)
        {
            using (var con = DbConnectionFactory.Create())
            {
                con.Open();

                string campoData = status == StatusLancamento.Pago ? "DataPagamento" : "DataCancelamento";

                var cmd = new SqlCommand($@"
                UPDATE LancamentoFinanceiro 
                SET Status=@Status, {campoData}=@Data
                WHERE Id=@Id", con);

                cmd.Parameters.AddWithValue("@Status", (int)status);
                cmd.Parameters.AddWithValue("@Data", data);
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }

        public List<LancamentoFinanceiro> Listar()
        {
            var lista = new List<LancamentoFinanceiro>();

            using (var con = DbConnectionFactory.Create())
            {
                con.Open();

                var cmd = new SqlCommand("SELECT * FROM LancamentoFinanceiro", con);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(Map(dr));
                }
            }

            return lista;
        }

        public List<LancamentoFinanceiro> ListarPagos()
        {
            var lista = new List<LancamentoFinanceiro>();

            using (var con = DbConnectionFactory.Create())
            {
                con.Open();

                var cmd = new SqlCommand("SELECT * FROM LancamentoFinanceiro WHERE Status = @Status", con);
                cmd.Parameters.AddWithValue("@Status", (int)StatusLancamento.Pago);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(Map(dr));
                }
            }

            return lista;
        }

        public bool ExisteDuplicado(string competencia, string descricao, TipoLancamento tipo)
        {
            using (var con = DbConnectionFactory.Create())
            {
                con.Open();

                var cmd = new SqlCommand(@"
                SELECT COUNT(1) 
                FROM LancamentoFinanceiro
                WHERE Competencia=@Competencia AND Descricao=@Descricao AND Tipo=@Tipo", con);

                cmd.Parameters.AddWithValue("@Competencia", competencia);
                cmd.Parameters.AddWithValue("@Descricao", descricao);
                cmd.Parameters.AddWithValue("@Tipo", (int)tipo);

                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        private LancamentoFinanceiro Map(SqlDataReader dr)
        {
            return new LancamentoFinanceiro
            {
                Id = Convert.ToInt32(dr["Id"]),
                Descricao = dr["Descricao"].ToString(),
                Tipo = (TipoLancamento)Convert.ToInt32(dr["Tipo"]),
                ValorOriginal = Convert.ToDecimal(dr["ValorOriginal"]),
                PercentualTaxa = Convert.ToDecimal(dr["PercentualTaxa"]),
                PercentualDesconto = Convert.ToDecimal(dr["PercentualDesconto"]),
                ValorCalculado = Convert.ToDecimal(dr["ValorCalculado"]),
                DataLancamento = Convert.ToDateTime(dr["DataLancamento"]),
                DataCriacao = Convert.ToDateTime(dr["DataCriacao"]),
                DataPagamento = dr["DataPagamento"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["DataPagamento"]),
                DataCancelamento = dr["DataCancelamento"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dr["DataCancelamento"]),
                Competencia = dr["Competencia"].ToString(),
                Status = (StatusLancamento)Convert.ToInt32(dr["Status"])
            };
        }
    }
}