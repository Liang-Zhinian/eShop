#!/bin/sh

eval $(docker-machine env myvm1)

# start nfs-client
docker-machine ssh myvm1 "sudo /usr/local/etc/init.d/nfs-client start"


docker stack deploy --compose-file=docker-compose.yml --compose-file=docker-compose.override.yml eshop_stack