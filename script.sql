USE [Testes];

CREATE TABLE Produto
(
	id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(50),
	preco decimal(18,2),
	quantidade int
)

--SELECT * FROM Produto;
--DELETE FROM Produto;
--DBCC CHECKIDENT ('Produto', RESEED, 0);