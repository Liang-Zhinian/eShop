#!/bin/sh

set -e

eval $(docker-machine env eshop)

docker stack rm eshop_stack
docker rmi $(docker images | grep "eshop/")

docker volume rm --force eshop_stack_rabbitmq || echo "Stacks already removed"
docker volume rm --force eshop_stack_mysqlinit || echo "Stacks already removed"
docker volume rm --force eshop_stack_mysqlconf || echo "Stacks already removed"
docker volume rm --force eshop_stack_mysqldata || echo "Stacks already removed"
docker volume rm --force eshop_stack_nosqldata || echo "Stacks already removed"
docker volume rm --force eshop_stack_redis || echo "Stacks already removed"