# Bem-vindo ao Projeto Driver

O Projeto Driver é uma aplicação desenvolvida para facilitar o processo de aluguel de motos. Combinando lógicas avançadas implementadas em PostgreSQL e a eficiência do framework .NET 6 com o Dapper, o Driver oferece uma solução robusta e escalável para gerenciar operações de aluguel de motos de forma eficiente e confiável.

## Recursos Principais

- **Lógicas Avançadas no PostgreSQL:** Utilizamos PostgreSQL como nosso sistema de gerenciamento de banco de dados, onde implementamos lógicas específicas para o processo de aluguel de motos. Isso inclui validações, cálculos de preços e outras operações essenciais para garantir a integridade e o bom funcionamento do sistema.

- **Desenvolvido em .NET 6:** O Projeto Driver foi construído utilizando o framework .NET 6, oferecendo o mais alto padrão de desempenho, segurança e compatibilidade. .NET 6 fornece uma base sólida para o desenvolvimento de aplicativos modernos e escaláveis.

- **Utilização do Dapper:** Para interagir com o banco de dados de forma eficiente e simplificada, o Driver utiliza o Dapper, um micro-ORM (Object-Relational Mapping) para .NET. O Dapper permite executar consultas SQL de forma eficaz, fornecendo um desempenho superior e uma experiência de desenvolvimento ágil.

## Como Começar

Para começar a utilizar o Projeto Driver, siga estas etapas simples:

1. **Configuração do Ambiente:**
   - Certifique-se de ter o [docker](https://docs.docker.com/desktop/install/windows-install/) instalado em sua máquina.
   - Clone este repositório em seu ambiente de desenvolvimento.
   - Execute o docker-compose na pasta raiz do seu projeto, você pode encontra-lo [aqui](https://github.com/gabriel-ao/driver/blob/main/Driver/docker-compose.yml)

2. **Configuração do Banco de Dados:**
   - Siga as instruções fornecidas na seção "Configuração do Banco de Dados" no arquivo README.md para configurar o banco de dados e as tabelas necessárias.

3. **Execução do Projeto:**
   - Utilize o Visual Studio ou a linha de comando para iniciar o projeto. Certifique-se de ter o .NET 6 SDK instalado em sua máquina.


# Configuração do Banco de Dados

Siga os passos abaixo para configurar o ambiente do banco de dados:

1. **Configuração do PostgreSQL Docker:**
   - Antes de tudo, é necessário estabelecer uma conexão com o PostgreSQL Docker. Utilize as seguintes informações para configurar a conexão:
      ```
         - Nome da Conexão: driver-db
         - Endereço do Host: drive-db
         - Porta: 5432
         - Banco de Dados de Manutenção: postgres
         - Nome de Usuário: postgres
         - Senha: postgres
      ```
   - Crie um banco de dados com o nome `driver-db`.

2. **Criação das Tabelas:**
   - Execute o script inicial para criar todas as tabelas necessárias. Você pode encontrá-lo [neste diretório](https://github.com/gabriel-ao/driver/blob/main/Driver/database/driver-db/scripts/initial-driver-db.sql).

3. **Implementação de Lógicas e Validações:**
   - Utilize os scripts de funções para adicionar lógica e validações ao banco de dados. Eles estão disponíveis [neste diretório](https://github.com/gabriel-ao/driver/tree/main/Driver/database/driver-db/functions).


4. **Adicione sua string de conexão ao código dotnet com exemplo:**
   ```sql
    string connectionString = "Host=localhost;Port=<PORTA_POSTGRES>;Database=driver;User Id=<USER_NAME>;Password=<PASSWORD>;";


Certifique-se de executar esses passos na ordem apresentada para garantir que o ambiente do banco de dados seja configurado corretamente e todas as funcionalidades estejam disponíveis para a aplicação.


# Documentação de API

## Backend: Driver.Auth
## Base rota: /Auth

### **POST**: fazer login para acessar a plataforma

POST /Auth/Create

request:
```json
{
  "userData": "string",
  "password": "string"
}
```

response: 
```json
{
  "message": "string",
  "error": true,
  "token": "string"
}
```

**OBS:**
   - **A aplicação do token é obrigatória para as rotas. o campo userDate é cnh para motorista e email para admin**
   - **todas rotas tem o retorno "error" e "message", em caso de algum erro por regra ou aplicação, o "error" é retornado true e a informação da mensagem no "message"**

# Header das rotas:
```json
{
  "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiOWE0MjE3YzYtYTY1MC00Mzc3LWI4YzEtMmVkYTVhMzczN2FlIiwianRpIjoiYzAwNjRiNWJmNDI2NDY5NmE3YWU3YjQ4ZTVjNzAwMWEiLCJ1bmlxdWVfbmFtZSI6IjlhNDIxN2M2LWE2NTAtNDM3Ny1iOGMxLTJlZGE1YTM3MzdhZSIsIm5iZiI6MTcxMjQzNDExMiwiZXhwIjoxNzEyNjkzMzEyLCJpYXQiOjE3MTI0MzQxMTIsImlzcyI6Imh0dHA6Ly9teWFwaS5jb20iLCJhdWQiOiJkcml2ZXJBdXRoIn0.lwwhVSi39pxvevkJHFkEApKjQgMDYaw0zO-LXv1a6IY"
}
```


## Backend: Driver.Manager
## Base rota: /Vehicle

### **POST**: Criar um novo veículo

POST /Vehicle/Create

request:
```json
{
  "year": 0,
  "model": "string",
  "plate": "string"
}
```

response: 
```json
{
  "message": "string",
  "error": true
}
```

### **Get**: listar veículos

#### Parâmetros de entrada
- `plate` (opcional): Filtra os veículos.

Get /Vehicle/Get

response: 
```json
{
  "message": "string",
  "error": true,
  "vehicles": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "year": 0,
      "model": "string",
      "plate": "string",
      "status": "string",
      "driver": "string"
    }
  ]
}
```


### **PUT**: Atualizar a placa do veículo

PUT /Vehicle/Update

request:
```json
{
  "newPlate": "string",
  "vehicleId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

response: 
```json
{
  "message": "string",
  "error": true
}
```

### **DEL**: remover um veículo

DEL /Vehicle/Delete

request:
```json
{
  "vehicleId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

response:
```json
{
  "message": "string",
  "error": true
}
```



## Backend: Driver.Manager
## Base rota: /Delivery

### **POST**: Criar um novo pedido

POST /Delivery/Create

request:
```json
{
  "title": "string",
  "description": "string",
  "price": 0
}
```

response:
```json
{
  "message": "string",
  "error": true
}
```

### **Get**: listar notificações de pedidos

#### Parâmetros de entrada
- `orderId`: listar motoristas notificados por esse pedido.

Get /Vehicle/Get

response:
```json 
{
  "message": "string",
  "error": true,
  "vehicles": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "year": 0,
      "model": "string",
      "plate": "string",
      "status": "string",
      "driver": "string"
    }
  ]
}
```
## Backend: Driver
## Base rota: /Driver

### **POST**: Criar um novo motorista

POST /Driver/Create

request:
```json
{
  "firstName": "string",
  "lastName": "string",
  "cnpj": "string",
  "birthDate": "2024-04-05T03:16:58.913Z",
  "cnhNumber": "string",
  "cnhID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "password": "string"
}
```

response:
```json
{
  "message": "string",
  "error": true
}
```


### **POST**: Criar uma nova reserva

POST /Driver/Create/Rent

request:
```json
{
  "planId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

response:
```json
{
  "message": "string",
  "error": true,
  "rentId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}

```


### **Put**: Atualizar uma nova reserva

PUT /Driver/Update/Rent

request:
```json
{
  "previousDate": "2024-04-05T03:19:03.146Z",
  "rentId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

response:
```json
{
  "message": "string",
  "error": true,
  "finishDate": "2024-04-05T03:19:03.153Z",
  "price": 0
}
```



### **Put**: atualização de CNH

#### Parâmetros de entrada
- `documentImage`: Adiciona um arquivo de imagem para salvar a CNH do motorista.

response:
```json
{
  "message": "string",
  "error": true
}
```



### **POST**: Aceitar um novo pedido de entrega


POST /Driver/AcceptDeliveryOrder

request :
```json
{
  "orderId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

response:
```json
{
  "message": "string",
  "error": true
}
```


### **PUT**: Finalizar o pedido

PUT /Driver/FinishDeliveryOrder

request:
```json
{
  "orderId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

response:
```json
{
  "message": "string",
  "error": true
}
```


### **Get**: Buscar tipos de cnh registrados na plataforma

Get /Driver/Cnh

response:
```json
{
  "message": "string",
  "error": true,
  "cnhTypes": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "description": "string"
    }
  ]
}
```



### **Get**: Buscar planos registrados na plataforma

Get /Driver/Plans

response:
```json
{
  "message": "string",
  "error": true,
  "plans": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "days": 0,
      "price": 0
    }
  ]
}
```


### **Get**: listar minhas notificações

Get /Driver/GetNotifications

response:
```json
{
  "message": "string",
  "error": true,
  "notifications": [
    {
      "orderID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "title": "string",
      "deliveryStatus": "string",
      "read": true,
      "createDate": "2024-04-07T14:34:00.207Z"
    }
  ]
}
```


### **Get**: detalhe de notificação

Get /Driver/NotificationDetails/{orderId}

response:
```json
{
  "message": "string",
  "error": true,
  "orderID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "string",
  "description": "string",
  "createDate": "2024-04-07T14:34:44.414Z"
}
```
