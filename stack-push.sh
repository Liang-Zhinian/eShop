#!/bin/sh

# set -e

# eval $(docker-machine env docker)

# images=$(docker-machine ssh docker "docker container ls --format 'table {{.Image}}' | grep 'eva'")
images=$(docker container ls --format 'table {{.Image}}' | grep 'eva')

for img in ${images}
do
    echo liangzhinian2018/${img}
    # docker untag liangzhinian2018/${img}
    # docker push liangzhinian2018/${img}
done