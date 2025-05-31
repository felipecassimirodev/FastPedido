#  FastPedido API

API para cadastro e processamento de pedidos com integração ao RabbitMQ, utilizando MongoDB como banco de dados e orquestração via Docker.

---

##  Funcionalidades

-  Criar pedidos (`POST /pedidos`)
-  Listar todos os pedidos (`GET /pedidos`)
-  Publicar mensagem no RabbitMQ ao criar um pedido
-  Worker consumidor que simula o processamento do pedido, alterando o status de `pendente` para `processado` após alguns segundos

---

##  Tecnologias Utilizadas
    - ASP.NET Core (C#)
    - MongoDB
    - RabbitMQ
    - Docker & Docker Compose

##  Como executar o projeto

###  Pré-requisitos

    - Docker
    - Docker Compose

###  Subindo os serviços

1. Clone este repositório e navegue até a pasta do projeto:

`bash
    -git clone `https://github.com/felipecassimirodev/FastPedido.git`
    -cd FastPedido

2.Subir os serviços

Ao executar o comando abaixo o docker-compose subira o mongoDB, RabbitMQ e a API configurando suas conexões    
### bash ou cmd na pasta src
    docker-compose up --build

### O Docker Compose irá iniciar os seguintes serviços:
    -MongoDB
    -RabbitMQ
    -API FastPedido

 Uma vez a aplicação em execução, acesse:
    -Swagger: `http://localhost:8000/swagger`
    -API: `http://localhost:8000/pedidos`

## Gerar imagem do containe
 ### Execute o comando abaixo na pasta src
     docker build --no-cache -f FastPedidoAPI/Dockerfile -t api-pedido .

## Como testar via Postman
Você pode testar os endpoints da API FastPedido utilizando o Postman.

1. Criar um pedido
Método: POST
    - URL: `http://localhost:8000/pedidos`

Headers:
Content-Type: application/json
    Body (raw → JSON):
    {
        "nomeCliente": "Felipe Cassimiro",
        "descricao": "Teste 01",
        "valor": 10.50
    }

![image](https://github.com/user-attachments/assets/22807c4c-5672-49d9-b1ca-9e0a2a4e228e)

2. Listar todos os pedidos
Método: GET
    - URL: `http://localhost:8000/pedidos`

![image](https://github.com/user-attachments/assets/eddf83b7-0faa-4dcf-9c3f-3ae52b4e26fc)


## Como testar via curl
Método: POST
curl -X POST http://localhost:8000/pedidos \
     -H "Content-Type: application/json" \
     -d '{ "nomeCliente": "Felipe Cassimiro", "descricao": "Teste 01" ,"valor": 11.50 }'

Método: GET
  curl http://localhost:8000/pedidos

## 🧠 Diferenciais Implementados (Plus)
✅ Criação de um servidor **Linux (VM)** na **Azure** para execução do projeto.

✅ Instalação e configuração completa do ambiente na VM:
- Docker
- Docker Compose
- Clonagem do repositório
- Geração das imagens (API, MongoDB, RabbitMQ) via `docker-compose`

✅ Exposição e liberação de portas no Azure:
- Permite acesso externo à **API**
- Permite acesso externo à interface do **RabbitMQ** (geralmente porta `15672`)


![image](https://github.com/user-attachments/assets/f826a277-9281-46e0-9550-7fe271c176df)

![image](https://github.com/user-attachments/assets/1aa9e70a-231a-4630-91c4-7def289e9c9a)

  
