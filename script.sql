USE [Testes];

CREATE TABLE Produto
(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(50),
	preco DECIMAL(18,2),
	quantidade INT	
)

CREATE TABLE Usuario
(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(50),
	senha VARCHAR(32),
	dataCadastro DATETIME
)

--SELECT * FROM Produto;
--DELETE FROM Produto;
--DBCC CHECKIDENT ('Produto', RESEED, 0);

--SELECT * FROM Usuario;
--DELETE FROM Usuario;
--DBCC CHECKIDENT ('Usuario', RESEED, 0);