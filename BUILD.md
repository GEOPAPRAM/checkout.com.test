# Building PaymentGateway
## Prerequisits
* .net core 5
* Nuke.build with Nuke.Docker
* WSL2 
* Docker Desktop

## Get the source
``` console
$ git clone https://github.com/GEOPAPRAM/checkout.com.test.git
cd checkout.com.test
```

## Tools intallation

### Nuke build as global tool

``` console
$ dotnet tool install Nuke.GlobalTool --global
```
### Nuke.Docker

```console
$ docker add package Nuke.Docker --version 0.4.0
```
