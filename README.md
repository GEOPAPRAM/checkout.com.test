# Payment Gateway test
## Prerequisits
For succesful compilation and execution the following tools and packages are required:
* .net 5 SDK
* MySQL (locally)
* Docker Desktop
* WSL 2 (Ubuntu 20.04)

For running in docker `rundocker.bat` must contain the valid path to `aspnetapp.pfx` certificate.

## Generating dev certificate
Run the following commands in order to generate self-signed ASP.NET development certificate that is used for https endpoint.
```
dotnet dev-certs https --clean
dotnet dev-certs https -ep ./dev.pfx -p supersecret
dotnet dev-certs https --trust
```

## Building

Run `build.bat` in order to build, test and publish the application. This command also build a docker image.

## Running
Service and all its dependencies can be launched with docker-compose. Run `rundocker.bat` command, wait around 20 seconds (while database is up and schema is restored). Try accessing the URL `https://localhost:5001/swagger`.
You'll probably see the warning from your browser that certificate is not trusted. Please, accept the certificate in order to proceed.

## Next steps
TODO: Implements WebServer mock and add some logic to service' payment providers. Add end-to-end tests.

## Caveats
Please, when you run `build.bat` and `rundocker.bat` on WSL2 be sure that those files are saved with LF line ending.