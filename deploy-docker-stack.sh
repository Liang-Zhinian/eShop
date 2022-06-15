#!/bin/sh

eval $(docker-machine env eshop)

# start nfs-client
docker-machine ssh eshop "sudo /usr/local/etc/init.d/nfs-client start"


docker-compose build
# REGISTRY=liangzhinian2018 TAG=2.1.505 docker stack deploy --compose-file=docker-compose.yml --compose-file=docker-compose.override.yml eshop_stack
env $(cat .env | grep ^[A-Z] | xargs) docker stack deploy --compose-file=docker-compose.yml --compose-file=docker-compose.override.yml eshop_stack