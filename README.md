# Bem-vindo ao Projeto Driver

O Projeto Driver é uma aplicação desenvolvida para facilitar o processo de aluguel de motos. Combinando lógicas avançadas implementadas em PostgreSQL e a eficiência do framework .NET 6 com o Dapper, o Driver oferece uma solução robusta e escalável para gerenciar operações de aluguel de motos de forma eficiente e confiável.

## Recursos Principais

- **Lógicas Avançadas no PostgreSQL:** Utilizamos PostgreSQL como nosso sistema de gerenciamento de banco de dados, onde implementamos lógicas específicas para o processo de aluguel de motos. Isso inclui validações, cálculos de preços e outras operações essenciais para garantir a integridade e o bom funcionamento do sistema.

- **Desenvolvido em .NET 6:** O Projeto Driver foi construído utilizando o framework .NET 6, oferecendo o mais alto padrão de desempenho, segurança e compatibilidade. .NET 6 fornece uma base sólida para o desenvolvimento de aplicativos modernos e escaláveis.

- **Utilização do Dapper:** Para interagir com o banco de dados de forma eficiente e simplificada, o Driver utiliza o Dapper, um micro-ORM (Object-Relational Mapping) para .NET. O Dapper permite executar consultas SQL de forma eficaz, fornecendo um desempenho superior e uma experiência de desenvolvimento ágil.

## Como Começar

Para começar a utilizar o Projeto Driver, siga estas etapas simples:

1. **Configuração do Ambiente:**
   - Certifique-se de ter o PostgreSQL instalado em sua máquina.
   - Clone este repositório em seu ambiente de desenvolvimento.

2. **Configuração do Banco de Dados:**
   - Siga as instruções fornecidas na seção "Configuração do Banco de Dados" no arquivo README.md para configurar o banco de dados e as tabelas necessárias.

3. **Execução do Projeto:**
   - Utilize o Visual Studio ou a linha de comando para iniciar o projeto. Certifique-se de ter o .NET 6 SDK instalado em sua máquina.

# Configuração do Banco de Dados

Siga os passos abaixo para configurar o ambiente do banco de dados:

1. **Criação do Banco de Dados:**
   - Certifique-se de ter o PostgreSQL instalado em sua máquina.
   - Crie um banco de dados com o nome `driver-db`.
   - Adicione sua string de conexão ao código dotnet com exemplo: 
   ```sql
    string connectionString = "Host=localhost;Port=<PORTA_POSTGRES>;Database=driver;User Id=<USER_NAME>;Password=<PASSWORD>;";

2. **Criação das Tabelas:**
   - Execute o script inicial para criar todas as tabelas necessárias. Você pode encontrá-lo [aqui](https://github.com/gabriel-ao/driver/blob/main/Driver/database/script.sql).

3. **Implementação de Lógicas e Validações:**
   - Utilize os scripts de funções para adicionar lógica e validações ao banco de dados. Eles estão disponíveis [neste diretório](https://github.com/gabriel-ao/driver/tree/main/Driver/database/functions).

Certifique-se de executar esses passos na ordem apresentada para garantir que o ambiente do banco de dados seja configurado corretamente e todas as funcionalidades estejam disponíveis para a aplicação.

