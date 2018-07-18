http://0b3224a5.m.daocloud.io


sudo docker ps -a -q --filter "status=exited" | xargs sudo docker rm
sudo docker rmi `sudo docker images -q --filter "dangling=true"`
