#!/bin/sh

# set -e

eval $(docker-machine env docker)

# images=$(docker-machine ssh docker "docker container ls --format 'table {{.Image}}' | grep 'eva'")
services=($(docker service ls --filter name=eshop_stack --format "{{.ID}}\t{{.Name}}"))
# arr=(${services})
# echo ${services[0]}

i=0
for svc in ${services[@]}
do
    j=`expr $i + 1`
    val=`expr $i % 2`
    if [ $val == 0 ]
    then
        # echo $i:${services[i]}
        echo $j:${services[j]}
        if [ ${services[j]} == "eshop_stack_sql-data" ]
        then
            docker service update --force ${services[i]}
            let "i++"
            continue;
        fi
        
        
        
    fi

    # docker push liangzhinian2018/${img}

    let "i++"
done

i=0
for svc in ${services[@]}
do
    j=`expr $i + 1`
    val=`expr $i % 2`
    if [ $val == 0 ]
    then
        # echo $i:${services[i]}
        echo $j:${services[j]}
        if [ ${services[j]} == "eshop_stack_webstatus" ]
        then
            echo "do nothing to ${services[j]}"
            let "i++"
            continue;
        fi

        if [ ${services[j]} == "eshop_stack_seq" ]
        then
            echo "do nothing to ${services[j]}"
            let "i++"
            continue;
        fi

        if [ ${services[j]} == "eshop_stack_sql-data" ]
        then
            echo "do nothing to ${services[j]}"
            let "i++"
            continue;
        fi

        if [ ${services[j]} == "eshop_stack_rabbitmq" ]
        then
            echo "do nothing to ${services[j]}"
            let "i++"
            continue;
        fi

        if [ ${services[j]} == "eshop_stack_nosql-data" ]
        then
            echo "do nothing to ${services[j]}"
            let "i++"
            continue;
        fi
        # echo $j:${services[j]}
        docker service update --force ${services[i]}
        
    fi

    # docker push liangzhinian2018/${img}

    let "i++"
done