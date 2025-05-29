# FastPedido

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

