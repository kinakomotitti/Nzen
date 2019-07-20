docker network create nzen-dev

docker run  -p 5432:5432 --network nzen-dev postgres

docker run -d -e PGADMIN_DEFAULT_EMAIL="kinakomotitti@com" -e PGADMIN_DEFAULT_PASSWORD="passw0rd" -p 80:80 --network nzen-dev dpage/pgadmin4