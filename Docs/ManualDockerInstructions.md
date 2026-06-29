# Manual Docker Instructions

- If you wish to run Docker manually instead of through docker-compose, follow these instructions.

1. Create network
```bash
docker network create commercefabric-network
```

2. Run PostgreSQL container
```bash
docker run -d --name postgressql-userservice --network commercefabric-network -e POSTGRES_PASSWORD=admin -e POSTGRES_DB=userservice -p 5432:5432 postgres:18.0
```

> **Note:** PostgreSQL must **not** already be running locally on port **5432** unless you change the port mapping.

3. Build microservice image
```bash
docker build -t danielmusselwhite/commercefabric_user_microservice:1.0.0 -f .\CommerceFabric.API\Dockerfile .
```

4. Run microservice
```bash
docker run --network commercefabric-network -p 9090:9090 danielmusselwhite/commercefabric_user_microservice:1.0.0
```

5. Push to Docker Hub
```bash
docker push danielmusselwhite/commercefabric_user_microservice:1.0.0
```