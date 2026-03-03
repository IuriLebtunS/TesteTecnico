USE FinanceiroDB;
GO

CREATE TABLE LancamentoFinanceiro (
    Id INT IDENTITY(1,1) PRIMARY KEY,

    Descricao NVARCHAR(200) NOT NULL,

    Tipo INT NOT NULL,
 
    ValorOriginal DECIMAL(18,2) NOT NULL,

    PercentualTaxa DECIMAL(5,2) NOT NULL DEFAULT 0,
    PercentualDesconto DECIMAL(5,2) NOT NULL DEFAULT 0,

    ValorCalculado DECIMAL(18,2) NOT NULL,

    DataLancamento DATETIME NOT NULL,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
    DataPagamento DATETIME NULL,
    DataCancelamento DATETIME NULL,

    Competencia CHAR(7) NOT NULL,

    Status INT NOT NULL
    
);

CREATE UNIQUE INDEX UX_Lancamento_Unico
ON LancamentoFinanceiro (Competencia, Descricao, Tipo);