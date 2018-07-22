http://0b3224a5.m.daocloud.io


sudo docker ps -a -q --filter "status=exited" | xargs sudo docker rm
sudo docker rmi `sudo docker images -q --filter "dangling=true"`

docker exec -it eshop_sql.data_1 mysql -uroot -pP@ssword
ALTER USER 'root'@'localhost' IDENTIFIED BY 'P@ssword';
CREATE USER 'book2'@'%' IDENTIFIED BY 'P@ssword';
GRANT ALL ON *.* TO 'book2'@'%';
