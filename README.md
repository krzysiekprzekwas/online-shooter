# Shoterio

Shoterio is an online FPS game created as a base project for our engineer's thesis. The main focus of our thesis is the impact of lag compensation algorithms on  performance and hardware requirements of hosts.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. 

### Prerequisites

To reduce the impact of external libraries and frameworks we're trying to keep our dependencies as small as possible.

For both backend and frontend application just host with .Net Core 2 support should be enough.

### Installing

A step by step series of examples that tell you have to get a development env running

Let's make backend server up and running. 

Move to folder GameServer and restore all dependencies 

```
cd GameServer
dotnet restore
```
Then try to build and run project

```
dotnet build
dotnet run
```

If everything goes fine program should prompt you on which IP adress and port he's running

When the server has been successfully started we can move to frontend aplication. Just open another terminal and repeat previous commands in GameClient directory

```
cd GameClient
dotnet restore
dotnet build
dotnet run
```

Same as before, the application should prompt you with an adress where you can access your game in the browser!

### IP settings

If after following steps above you still can't see anything in your browser or you're receiving errors in browser console, it's probably caused by IP mismatch between server, client and browser.

#### Server

IP's and ports where the server is listening are set up in Program.cs file in GameServer project.
```
.UseUrls("http://*:1000", "http://0.0.0.0:5000")                
```
#### Client

Client's IP is set up in the same place in GameClient project.
```
.UseUrls("http://*:80")               
```

#### Browser

The client application is hosting html, css and js files to end user. In one of these files, there's separate line telling the browser where to look for the server instance. That file is connection.js and address is set up right there at the top.
```
const socket = new WebSocket('ws://localhost:1000/ws');            
```

## Built With

* [.Net Core](https://www.microsoft.com/net/download/windows)
* [BabylonJS](https://www.babylonjs.com/)

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Authors

* **Tomasz Rutkowski** - [tomejzen](https://github.com/tomejzen)
* **Krzysztof Przekwas** - [krzysiekprzekwas](https://github.com/krzysiekprzekwas)

Supervised by:

* **dr Adam Przybyłek** - [GUT](https://pg.edu.pl/c2f8068c38_adam.przybylek)

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
