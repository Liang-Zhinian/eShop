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

## List stacks
```
$ docker stack ls
NAME                SERVICES            ORCHESTRATOR
eshop_stack         22                  Swarm
```

## List services
```
$ docker service ls
ID                  NAME                                   MODE                REPLICAS            IMAGE                                  PORTS
ourtqg2hzyst        eshop_stack_basket-api                 replicated          1/1                 eva/basket.api:2.1.505                 *:5103->80/tcp
on3b2wpqudyl        eshop_stack_basket-data                replicated          1/1                 redis:alpine                           *:6379->6379/tcp
c8qpd3kwn475        eshop_stack_catalog-api                replicated          1/1                 eva/catalog.api:2.1.505                *:5101->80/tcp
k8dfhnbml41q        eshop_stack_identity-api               replicated          1/1                 eva/identity.api:2.1.505               *:5105->80/tcp
gbat9xqlh6r7        eshop_stack_locations-api              replicated          1/1                 eva/locations.api:2.1.505              *:5109->80/tcp
9hwhw3axlaau        eshop_stack_marketing-api              replicated          1/1                 eva/marketing.api:2.1.505              *:5110->80/tcp
fnymqokswudr        eshop_stack_mobilemarketingapigw       replicated          1/1                 eva/ocelotapigw:2.1.505                *:5201->80/tcp
731q6gxfw1aq        eshop_stack_mobileshoppingagg          replicated          1/1                 eva/mobileshoppingagg:2.1.505          *:5120->80/tcp
qmc24zyfbi8s        eshop_stack_mobileshoppingapigw        replicated          1/1                 eva/ocelotapigw:2.1.505                *:5200->80/tcp
yumdipsli413        eshop_stack_nosql-data                 replicated          1/1                 mongo:latest                           *:27017->27017/tcp
mw3c6ojuf72y        eshop_stack_ordering-api               replicated          1/1                 eva/ordering.api:2.1.505               *:5102->80/tcp
xxycg3p936l2        eshop_stack_ordering-backgroundtasks   replicated          1/1                 eva/ordering.backgroundtasks:2.1.505   *:5111->80/tcp
tjtkk30y49wq        eshop_stack_ordering-signalrhub        replicated          1/1                 eva/ordering.signalrhub:2.1.505        *:5112->80/tcp
lwjb5wwr1m9f        eshop_stack_payment-api                replicated          1/1                 eva/payment.api:2.1.505                *:5108->80/tcp
vvfzg2alr1b5        eshop_stack_rabbitmq                   replicated          1/1                 rabbitmq:3.7-management-alpine         *:5672->5672/tcp, *:15672->15672/tcp
if11bqqivvrl        eshop_stack_seq                        replicated          1/1                 datalust/seq:latest                    *:5340->80/tcp
p9r95spo04vc        eshop_stack_sql-data                   replicated          1/1                 mysql/mysql-server:5.7                 *:3306->3306/tcp
7vw39wtpkomh        eshop_stack_webmarketingapigw          replicated          1/1                 eva/ocelotapigw:2.1.505                *:5203->80/tcp
eanuarxiey5f        eshop_stack_webmvc                     replicated          1/1                 eva/webmvc:2.1.505                     *:5100->80/tcp
t5ijuwev4j5f        eshop_stack_webshoppingagg             replicated          1/1                 eva/webshoppingagg:2.1.505             *:5121->80/tcp
7ihi71me7elt        eshop_stack_webshoppingapigw           replicated          1/1                 eva/ocelotapigw:2.1.505                *:5202->80/tcp
o6hyofzw4hbt        eshop_stack_webstatus                  replicated          1/1                 eva/webstatus:2.1.505                  *:5107->80/tcp
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

