# Overview
A signalr based app & client to demonstrate the pushing notification , commands to clients behind private network.


![image](https://user-images.githubusercontent.com/8907962/236670654-46cb60f2-fd2b-4259-ad2c-7d8f530f78e0.png)


## Client Application
.NET SignalR client , that subscribe to notification & commands from SignalR service. It also implements logic to **increase/decrease** the logging level based on the command from server without **restarting** the application
A persistant bidirectional connection (Websocket) created to signalr service and receives commands & notfication. A background service receives commands and acts on it.
## Server Application
ASP.NET Core SignalR service, that allows clients to subscribe for notification & commands.Provides a REST API to notify & send commands to connected clients.
### End to End demo.
#### Running Server Application
- Change to **SignalRService** directory
- Run the **SignalRService** server. Server will start on port **5189**
  ```shell
    dotnet run 
  ```
#### Running SiganlR Client
- Change to **SignalRClient** directory
- Run the **SignalRClient** app. Client app is hardcoded to connect to server @ **http://localhost5189**
  ```shell 
    dotnet run 
  ```
- Client print log message with default log level.
- Send Request SignalRService to increase log level of the **SignalRClient** using the REST API
 ```shell
 curl "http://localhost:5189/Notify/changloglevel"   -H "accept: */*"   -H "Content-Type: application/json"   -d "{ \"groupName\": \"configchange\",  \"logLevel\": \"Trace\",  \"logCategory\": \"SignalRClient.Worker\"}"

```
- Now you should be able to see Client pring info & trace messages

### SiganlR Service REST API 
- Swagger API - http://localhost:5189/swagger

### Running Service in Kubernetes
[Running Service in Kubernetes](SignalrService/readme.md)



