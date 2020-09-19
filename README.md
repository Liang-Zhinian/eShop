# eShop - Microservices Architecture and Containers based Reference Application (**BETA state** - Visual Studio 2017 and CLI environments compatible)
Sample .NET Core reference application, powered by Microsoft, based on a simplified microservices architecture and Docker containers.

## BUILD AND RUN IN DOCKER-MACHINE!
>export TAG=2.1.505
>export REGISTRY=2.1.505
>export ESHOP_EXTERNAL_DNS_NAME_OR_IP=192.168.99.109
>export ESHOP_AZURE_STORAGE_CATALOG_URL=http://192.168.99.109:5202/api/v1/c/catalog/items/[0]/pic/
>export ESHOP_PROD_EXTERNAL_DNS_NAME_OR_IP=192.168.99.109

>docker-compose build
>docker stack deploy  --compose-file=docker-compose.yml --compose-file=docker-compose.override.yml >eshop_stack

