
dotnet build --configuration Release

dotnet test --results-directory .\test_results\  --logger:"xunit"

dotnet publish -c Release -o dist

docker build . -t paymentgateway:dev