# eShop - Microservices Architecture and Containers based Reference Application
Sample .NET Core reference application, powered by Microsoft, based on a simplified microservices architecture and Docker containers.

## Build and deploy the app on the swarm node
### Get the IP address of the swarm node
```
$ docker-machine env [docker-machine-name]
$ eval $(docker-machine env [docker-machine-name])
$ docker-machine ip [docker-machine-name]
192.168.99.109

```

### Config NFS with Docker "Local"​ volume driver
```
===== config nfs-server =====

$ sudo nano /etc/exports
/Users/sprite/Documents/docker_data_storage/mysql/data -alldirs -rw -maproot=root:wheel -network 172.20.10.0 -mask 255.255.255.0
/Users/sprite/Documents/docker_data_storage/mysql/conf -alldirs -rw -maproot=root:wheel -network 172.20.10.0 -mask 255.255.255.0
/Users/sprite/Documents/docker_data_storage/mysql/init -alldirs -rw -maproot=root:wheel -network 172.20.10.0 -mask 255.255.255.0
```

```
===== update nfs-server =====

$ sudo nfsd update
$ showmount -e
```

```
===== start nfs-client =====

$ docker-machine ssh [docker-machine-name] "sudo /usr/local/etc/init.d/nfs-client start"
```

### Export the needed env
```
$ export TAG=2.1.505
$ export REGISTRY=eva
$ export ESHOP_EXTERNAL_DNS_NAME_OR_IP=192.168.99.109
$ export ESHOP_AZURE_STORAGE_CATALOG_URL=http://192.168.99.109:5202/api/v1/c/catalog/items/[0]/pic/
$ export ESHOP_PROD_EXTERNAL_DNS_NAME_OR_IP=192.168.99.109
```

### Build and deploy the app
```
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
ID                          NAME                                     IMAGE                                                                                                    NODE                DESIRED STATE       CURRENT STATE              ERROR                                                          PORTS
gpanopr92njpwl6f7g48msgzy   eshop_stack_identity-api.1               eva/identity.api:2.1.505                                                                                 docker              Running             Running 17 minutes ago                                                                    
sq28o5uxjuhi2f2y41r1gb2qb   eshop_stack_sql-data.1                   mysql/mysql-server:5.7@sha256:6d6fdd5bd31256a484e887c96c41abfc9ee3e3deb989de83ebdb8694fcc83485           docker              Running             Running 30 minutes ago                                                                    
y1puf5euk8t9ku5fphyinwqi3   eshop_stack_ordering-api.1               eva/ordering.api:2.1.505                                                                                 docker              Running             Running 41 minutes ago                                                                    
sopo77kxdz985jnsr1q4dj8wv   eshop_stack_sql-data.1                   mysql/mysql-server:5.7@sha256:6d6fdd5bd31256a484e887c96c41abfc9ee3e3deb989de83ebdb8694fcc83485           docker              Shutdown            Failed 32 minutes ago      "task: non-zero exit (137): dockerexec: unhealthy container"   
zvcfinnlo4fvyzz6wpxfhgw4k   eshop_stack_mobileshoppingapigw.1        eva/ocelotapigw:2.1.505                                                                                  docker              Running             Running 3 hours ago                                                                       
1p6s21i3meqb5z5t4vfeguo28   eshop_stack_locations-api.1              eva/locations.api:2.1.505                                                                                docker              Running             Running 3 hours ago                                                                       
8ho3beiqcbvvaffzp9edpn6uf   eshop_stack_webstatus.1                  eva/webstatus:2.1.505                                                                                    docker              Running             Running 3 hours ago                                                                       
767281064b6rvaqiccxkclhg5   eshop_stack_ordering-backgroundtasks.1   eva/ordering.backgroundtasks:2.1.505                                                                     docker              Running             Running 3 hours ago                                                                       
hpgklb7nkxj72ev4flkdzq1lt   eshop_stack_ordering-api.1               eva/ordering.api:2.1.505                                                                                 docker              Shutdown            Shutdown 41 minutes ago                                                                   
1hz0cy95ur0ck28odq2glf1sg   eshop_stack_webmvc.1                     eva/webmvc:2.1.505                                                                                       docker              Running             Running 3 hours ago                                                                       
65lv120ae6i5zgrzt2inhavz3   eshop_stack_sql-data.1                   mysql/mysql-server:5.7@sha256:6d6fdd5bd31256a484e887c96c41abfc9ee3e3deb989de83ebdb8694fcc83485           docker              Shutdown            Failed about an hour ago   "task: non-zero exit (137)"                                    
so1w0gaors10arqeg7ft5t4cx   eshop_stack_catalog-api.1                eva/catalog.api:2.1.505                                                                                  docker              Running             Running 3 hours ago                                                                       
08v15c5mc4gg24ffgwhejpxea   eshop_stack_basket-data.1                redis:alpine@sha256:4015d7a6a0901920a3adfae3a538bf8489325738153948f95ca2b637944bdfe5                     docker              Running             Running 3 hours ago                                                                       
z9i6jtrgzv43ejhsrjkiks3ty   eshop_stack_seq.1                        datalust/seq:latest@sha256:264885b551896ed8a2bb534654601140ac71e3082a19b184aea54f47425fa6e4              docker              Running             Running 3 hours ago                                                                       
yvahkwa8pisywqgzh6qgeurzw   eshop_stack_payment-api.1                eva/payment.api:2.1.505                                                                                  docker              Running             Running 3 hours ago                                                                       
vd8f1gcpghe1gh208a0na2rxb   eshop_stack_nosql-data.1                 mongo:latest@sha256:ebcdb042054d9974c8c3160d761b0bdb39b55115448242de1a5161c124ddb0af                     docker              Running             Running 3 hours ago                                                                       
hz0qkcp0bk35lez5ew4ww3zpr   eshop_stack_mobileshoppingagg.1          eva/mobileshoppingagg:2.1.505                                                                            docker              Running             Running 3 hours ago                                                                       
qi1wembgw7swzzr2irza7lxrv   eshop_stack_mobilemarketingapigw.1       eva/ocelotapigw:2.1.505                                                                                  docker              Running             Running 3 hours ago                                                                       
x382b6jhnkr5wa0ko4zmrx8yc   eshop_stack_identity-api.1               eva/identity.api:2.1.505                                                                                 docker              Shutdown            Shutdown 17 minutes ago                                                                   
fgjdxaqqj2k100jczg2mphx4g   eshop_stack_ordering-signalrhub.1        eva/ordering.signalrhub:2.1.505                                                                          docker              Running             Running 3 hours ago                                                                       
svd7rrwxhv1e0i5lu8s217ry6   eshop_stack_basket-api.1                 eva/basket.api:2.1.505                                                                                   docker              Running             Running 3 hours ago                                                                       
6sb9ko9ak7fhdqap1g7yaowbl   eshop_stack_rabbitmq.1                   rabbitmq:3.7-management-alpine@sha256:e97f6bb68aab3f747f8a6f4dc52ed57401de91ca480a0be3a36e7476ac273169   docker              Running             Running 3 hours ago                                                                       
9kdshglawv8hoo213r0z7mlo9   eshop_stack_marketing-api.1              eva/marketing.api:2.1.505                                                                                docker              Running             Running 3 hours ago                                                                       
vhzoslkcus0y4c3mz7h00d6to   eshop_stack_webshoppingapigw.1           eva/ocelotapigw:2.1.505                                                                                  docker              Running             Running 3 hours ago                                                                       
scvmhjqg7pji8vpha6sogttz7   eshop_stack_webshoppingagg.1             eva/webshoppingagg:2.1.505                                                                               docker              Running             Running 3 hours ago                                                                       
8f67uvb1oq1yshr6roow1984w   eshop_stack_webmarketingapigw.1          eva/ocelotapigw:2.1.505                                                                                  docker              Running             Running 3 hours ago             
```

## Rebuild and force re-create a particular service
```
$ docker-compose build ordering-api
$ docker service update --force eshop_stack_ordering-api
eshop_stack_ordering-api
overall progress: 1 out of 1 tasks 
1/1: running   [==================================================>] 
verify: Service converged
```

## Remove stack
```
$ docker stack remove eshop_stack
Removing service eshop_stack_basket-api
Removing service eshop_stack_basket-data
Removing service eshop_stack_catalog-api
Removing service eshop_stack_identity-api
Removing service eshop_stack_locations-api
Removing service eshop_stack_marketing-api
Removing service eshop_stack_mobilemarketingapigw
Removing service eshop_stack_mobileshoppingagg
Removing service eshop_stack_mobileshoppingapigw
Removing service eshop_stack_nosql-data
Removing service eshop_stack_ordering-api
Removing service eshop_stack_ordering-backgroundtasks
Removing service eshop_stack_ordering-signalrhub
Removing service eshop_stack_payment-api
Removing service eshop_stack_rabbitmq
Removing service eshop_stack_seq
Removing service eshop_stack_sql-data
Removing service eshop_stack_webmarketingapigw
Removing service eshop_stack_webmvc
Removing service eshop_stack_webshoppingagg
Removing service eshop_stack_webshoppingapigw
Removing service eshop_stack_webstatus
Removing network eshop_stack_default

```

