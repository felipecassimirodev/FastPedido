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
bash ou cmd na pasta src
-docker-compose up --build

### O Docker Compose irá iniciar os seguintes serviços:
    -MongoDB
    -RabbitMQ
    -API FastPedido

 Uma vez a aplicação em execução, acesse:
-Swagger: `http://localhost:8000/swagger`
-API: `http://localhost:8000/pedidos`

## Gerar imagem do containe
 Execute o comando abaixo na pasta src
-docker build --no-cache -f FastPedidoAPI/Dockerfile -t api-pedido .

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
  "valor": 11.50
}

2. Listar todos os pedidos
Método: GET
- URL: `http://localhost:8000/pedidos`

## Como testar via curl
Método: POST
curl -X POST http://localhost:8000/pedidos \
     -H "Content-Type: application/json" \
     -d '{ "nomeCliente": "Felipe Cassimiro", "descricao": "Teste 01" ,"valor": 11.50 }'

Método: GET
  curl http://localhost:5000/pedidos