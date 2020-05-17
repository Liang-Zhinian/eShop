#!/bin/sh

eval $(docker-machine env myvm1)

docker stack rm eshop_stack
docker rmi $(docker images | grep "eshop/")