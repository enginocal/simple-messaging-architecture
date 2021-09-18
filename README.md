# simple-messaging-architecture
It is a simple application that allows users to send messages to each other, as well as a number of features.

# System Overview

The application consists of 3 domains and each is only responsible for itself. It was tried to be written with current technical approaches.
DDD approach is adopted and command/event style is implemented.

1. Auth
2. User
3. Chat

# Basic Architecture Schema

![basicSchema](https://user-images.githubusercontent.com/536455/133866564-3a1d79ca-28e6-45a1-8e32-e004f952be74.PNG )

# 1. Auth
 This service is only responsible for login. Creates and sends tokens for successful logins.
 
# 2. User
This service is responsible for creating users and blocking other users. Publish CreateUser and BlockUser commands.

* The API manages the requests and responses from the client.
* User Service subscribes to commands.
![user](https://user-images.githubusercontent.com/536455/133866894-75c5d513-6c98-4fec-b251-cf2fe6ba5748.PNG)

![user_arch](https://user-images.githubusercontent.com/536455/133867125-e18eadeb-b13c-4117-a6ac-f86d508b6204.PNG)

# Chat 

Its architectural structure is like User. But domain seperated to conversation and message for storing highly scale messaging store system.
Two users have a conversation with each other. The messages of this conversation are kept in the Message collection.
```
Conversation : {
 id: Guid,
 members: [ user1, user2 ]
}
```
```
Message { conversationId: 123, author: user1, body: 'Home' }
Message { conversationId: 123, author: user2, body: 'Run' }
```

# How to Run All Applications

You can run all applications using the command below.
```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

```
Launch the services from urls :

* **User API      -> localhost:8006/swagger/index.html
* **User Service  -> localhost:8007/swagger/index.html
* **Idendity API  -> localhost:8005/swagger/index.html
* **Chat API      -> localhost:8008/swagger/index.html
* **Chat Service  -> localhost:8009/swagger/index.html

# Postman
You can find the request collection in Postman.

