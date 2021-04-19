# Payment Gateway test
## Prerequisits
For succesful compilation and execution the following tools and packages are required:
* .net 5 SDK
* MySQL (locally)
* Docker Desktop
* WSL 2 (Ubuntu 20.04)

For running in docker `rundocker.bat` must contain the valid path to `aspnetapp.pfx` certificate.

## Buildings

Run `build.bat` in order to build, test and publish the application. This command also build a docker image.

## TODO
 - Docker launch doesn't contain all dependencies. Work on docker-compose.yml is in progress. 
- End-to-end integration tests with MockServer (once all dependencies are in place).
