# Overview
A MQTT demonstration application that connects & subscribes to MQTT topics.
All MQTT connection handling , subscription & message processing is implemented as Background service with simple abstraction
for application to register message processing logic
- [Console application connects & process MQTT messages from the MQTT Server](https://github.com/madhub/mqtt-poc/tree/master/MqttDemo/MqttWorkerServiceSubscriber). 
- [ASP.NET Core API application connects & process MQTT messages from the MQTT Server](https://github.com/madhub/mqtt-poc/tree/master/MqttDemo/AspNetCoreWebApiWithMqttSubscription)

## Highlevel context overview
![image](https://github.com/madhub/mqtt-poc/assets/8907962/2407e2bf-f793-4024-84e5-3b763f7b82e4)

## How to test
A cloud deployed **mosquitto MQTT broker** is used for demonstration **wss://test.mosquitto.org:8081**  . Refer [mosquitto test mqtt broker information page](https://test.mosquitto.org/)  
A Kubernetes deployment yaml also available to host open source mosquitto MQTT broker in Docker desktop with Kubernetes support
```shell
cd deployment
kubectl apply -f mosquitto-deployment.yaml
```









