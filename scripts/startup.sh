#!/bin/sh

set -e

docker-machine start eshop
eval $(docker-machine env eshop)

