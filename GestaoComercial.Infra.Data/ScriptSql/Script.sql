CREATE DATABASE DB_GESTAOCOMERCIAL
GO
USE DB_GESTAOCOMERCIAL;
GO

/*---------------------------------------------------------------------------------------
				TABELA DE CLIENTE - CLIENTE
---------------------------------------------------------------------------------------*/
CREATE TABLE CLIENTE (
    CLIENTEID INT IDENTITY (1, 1) NOT NULL,
    NOME VARCHAR(100),
    CONSTRAINT PK_CLIENTE PRIMARY KEY (CLIENTEID)
);
GO
/*----------------------------------FIM------------------------------------------------*/

/*---------------------------------------------------------------------------------------
				TABELA DE PEDIDO - PEDIDO
---------------------------------------------------------------------------------------*/
CREATE TABLE PEDIDO (
    PEDIDOID INT IDENTITY (1, 1) NOT NULL,
    DATACRIACAO DATETIME NOT NULL,
    CLIENTEID INT NOT NULL,
    CONSTRAINT PK_PEDIDO PRIMARY KEY (PEDIDOID),
    CONSTRAINT FK_PEDIDO_CLIENTE FOREIGN KEY(CLIENTEID) REFERENCES CLIENTE(CLIENTEID)
);
GO
/*----------------------------------FIM------------------------------------------------*/

/*---------------------------------------------------------------------------------------
				TABELA DE PRODUTO - PRODUTO
---------------------------------------------------------------------------------------*/
CREATE TABLE PRODUTO (
    PRODUTOID INT IDENTITY (1, 1) NOT NULL,
    NOME VARCHAR(100),
    PRECO DECIMAL(10, 2),
    CONSTRAINT PK_PRODUTO PRIMARY KEY (PRODUTOID)
);
GO
/*----------------------------------FIM------------------------------------------------*/

/*---------------------------------------------------------------------------------------
				TABELA DE PEDIDO_PRODUTO - PEDIDO_PRODUTO
---------------------------------------------------------------------------------------*/
CREATE TABLE PEDIDO_PRODUTO(
     PEDIDOPRODUTOID INT IDENTITY (1, 1) NOT NULL,
     PEDIDOID INT NOT NULL,
     PRODUTOID INT NOT NULL,
     QUANTIDADE INT NOT NULL,
    CONSTRAINT PK_PEDIDOPRODUTO PRIMARY KEY (PEDIDOPRODUTOID),
    CONSTRAINT FK_PEDIDOPRODUTO_PEDIDO FOREIGN KEY(PEDIDOID) REFERENCES PEDIDO(PEDIDOID),
    CONSTRAINT FK_PEDIDOPRODUTO_PRODUTO FOREIGN KEY(PRODUTOID) REFERENCES PRODUTO(PRODUTOID)

);

GO
/*----------------------------------FIM------------------------------------------------*/


