# eShop - Microservices Architecture and Containers based Reference Application (**BETA state** - Visual Studio 2017 and CLI environments compatible)
Sample .NET Core reference application, powered by Microsoft, based on a simplified microservices architecture and Docker containers.

## Build and run in docker swarm mode
```
$ docker-machine env [docker-machine-name]
$ eval $(docker-machine env [docker-machine-name])
$ docker-machine ip [docker-machine-name]
192.168.99.109
$ export TAG=2.1.505
$ export REGISTRY=eva
$ export ESHOP_EXTERNAL_DNS_NAME_OR_IP=192.168.99.109
$ export ESHOP_AZURE_STORAGE_CATALOG_URL=http://192.168.99.109:5202/api/v1/c/catalog/items/[0]/pic/
$ export ESHOP_PROD_EXTERNAL_DNS_NAME_OR_IP=192.168.99.109

$ docker-compose build
$ docker stack deploy  --compose-file=docker-compose.yml --compose-file=docker-compose.override.yml eshop_stack
```

## List the tasks in the stack
```
$ docker stack ps eshop_stack --no-trunc
```

## Rebuild and force re-create a particular service
```
$ docker-compose build ordering-api
$ docker service update --force eshop_stack_ordering-api
```

## REMOVE STACK
```
$ docker stack remove eshop_stack
```

