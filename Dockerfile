FROM mcr.microsoft.com/dotnet/aspnet:5.0

LABEL maintainer="George Papadopoulos <geopapram@gmail.com>"

ENV HOME /home/paymentgateway
WORKDIR $HOME

ARG config=Release

ADD ./dist $HOME/

EXPOSE 5000
EXPOSE 5001

CMD ["/usr/share/dotnet/dotnet", "PaymentGateway.dll"]