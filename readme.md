# Testes NHibernate

![Badge em Desenvolvimento](https://img.shields.io/static/v1?label=STATUS&message=FINALIZADO&color=GREEN&style=for-the-badge)

## Introdução
Este projeto é uma aplicação de console que demonstra a implementação de operações CRUD (Create, Read, Update, Delete) em uma base de dados SQL utilizando o Framework NHibernate. As principais tecnologias utilizadas são:

* C#
* NHibernate
* SQL Server
* Console Application

## Observações
O build action do arquivo `hbm.xml` deve ser configurado como **Embedded Resource** para que o NHibernate consiga localizar e utilizar corretamente os mapeamentos.

## NHibernate
NHibernate é a implementação para .NET do framework Hibernate do Java. Ele é um dos mais antigos e respeitados ORMs (Object Relational Mapping), facilitando a interação entre a aplicação .NET e o banco de dados SQL Server, gerenciando mapeamentos entre objetos .NET e tabelas do banco de dados.

## Estrutura do Projeto
O projeto está organizado da seguinte forma:
* **Models**: Contém as classes de modelo que representam as tabelas do banco de dados.
* **Mappings**: Contém os arquivos de mapeamento (`.hbm.xml`) usados pelo NHibernate para mapear os modelos às tabelas do banco de dados.
* **Data**: Contém a configuração do NHibernate e as classes de repositório para acessar os dados.
* **Program**: Contém a lógica principal da aplicação de console.

## Funcionalidades
* **Create**: Adicionar novos registros ao banco de dados.
* **Read**: Visualizar registros existentes.
* **Update**: Editar registros existentes.
* **Delete**: Remover registros do banco de dados.

## Configuração e Execução
Para executar este projeto localmente, siga os passos abaixo:

1. **Clone o repositório**:
   ```bash
   git clone <URL-do-repositorio>
   ```

2. **Navegue até o diretório do projeto**:
   ```bash
   cd nome-do-projeto
   ```

3. **Instale as dependências**:
   Certifique-se de que você tenha o .NET SDK instalado em sua máquina. Instale as dependências com:
   ```bash
   dotnet restore
   ```

4. **Atualize as configurações do banco de dados**:
   No arquivo `appsettings.json`, configure a string de conexão para o seu servidor SQL Server.

5. **Configure os arquivos de mapeamento**:
   Certifique-se de que o build action dos arquivos `.hbm.xml` está definido como **Embedded Resource**.

6. **Execute o projeto**:
   ```bash
   dotnet run
   ```
