# Shoterio
[![Build status](https://dev.azure.com/shoterio-win/shoterio-win/_apis/build/status/shoterio-win%20-%20CI)](https://dev.azure.com/shoterio-win/shoterio-win/_build/latest?definitionId=1)

Shoterio is an online FPS game created as a base project for our engineer's thesis. The main focus of our thesis is the impact of lag compensation algorithms on performance and hardware requirements of hosts.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. 

### Prerequisites

To reduce the impact of external libraries and frameworks we're trying to keep our dependencies as small as possible. Inside project we use [LibMan](https://docs.microsoft.com/pl-pl/aspnet/core/client-side/libman/libman-vs?view=aspnetcore-2.1 "LibMan's Documentation"). However for current state not all dependent libraries are attached to cdnjs, so we keep them localy. 

For hosting .Net Core 2.1 support should be enough, rest of dependecies should be automatically downloaded during build process

### Installing

A step by step series of examples that tell you have to get a development env running

Let's make server up and running. 

Move to folder GameServer and to be sure we're fresh - restore all dependencies 

```
cd GameServer
dotnet restore
```
Then try to build and run project

```
dotnet build
dotnet run
```

If everything goes fine program should prompt you that he's running and that he's listening on port 80
Now you can access your game in the browser, by connecting to localhost or host ip.

### Port settings

If after following steps above you still can't see anything in your browser or you're receiving errors in browser console, it's probably caused by taken port on Your machine. 

## Built With

* [.Net Core 2.1](https://www.microsoft.com/net/download/windows)
* [P5JS](https://p5js.org/)
* [SignalR](https://www.asp.net/signalr)

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Authors

* **Tomasz Rutkowski** - [tomejzen](https://github.com/tomejzen)
* **Krzysztof Przekwas** - [krzysiekprzekwas](https://github.com/krzysiekprzekwas)

Supervised by:

* **dr Adam Przybyłek** - [GUT](https://pg.edu.pl/c2f8068c38_adam.przybylek)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
