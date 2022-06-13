#!/bin/sh

eval $(docker-machine env eshop)

# start nfs-client
docker-machine ssh eshop "sudo /usr/local/etc/init.d/nfs-client start"


docker stack deploy --compose-file=docker-compose.yml --compose-file=docker-compose.override.yml eshop_stack