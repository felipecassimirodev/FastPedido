# FastPedido
# 🛒 FastPedido API

API para cadastro e processamento de pedidos com integração ao RabbitMQ, utilizando MongoDB como banco de dados e orquestração via Docker.

---

## 📌 Funcionalidades

- 📥 Criar pedidos (`POST /pedidos`)
- 📄 Listar todos os pedidos (`GET /pedidos`)
- 📨 Publicar mensagem no RabbitMQ ao criar pedido
- 🔄 Worker consumidor que simula o processamento do pedido (altera status de `pendente` para `processado` após alguns segundos)

---

## 🧱 Tecnologias Utilizadas

- ASP.NET Core (C#)
- MongoDB
- RabbitMQ
- Docker & Docker Compose

---

## 🐳 Como subir o projeto

### 1. Pré-requisitos

- Docker e Docker Compose instalados

### 2. Subir os serviços

```bash ou cmd na pasta src
docker-compose up --build



#Gerar imagem do containe
Execute o comando abaixo na pasta src
docker build --no-cache -f FastPedidoAPI/Dockerfile -t api-pedido .


#Inicializar imagem 
 #Docker run --name pedido-api -p 8000:80  api-pedido .

#Verifica os containers criados
 #Docker container ls   

#Parar o container
 #Docker stop Nome_do_container

#Remover o container
 #Docker rm Nome_do_container

 docker-compose down
docker-compose up -d


docker-compose up -d