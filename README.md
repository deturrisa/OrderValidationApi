# Order Validation WebApi

The Order Validation WebApi supports .netcore 3.1
### SupportedClients
 - A
 - B
 - C
### Installation
From postman import the collection script if depending on if you plan to debug in VS or Postman
| Script | Platform |
| ------ | ------ |
| OrderValidationTests.vs.postman_collection | Visual Studio|
| OrderValidationTests.pm.postman_collection | Postman|

Open powershell and run


```sh
$ cd OrderValidation\OrderValidation.WebApi\bin\Debug\netcoreapp3.1
$ dotnet OrderValidation.WebApi.dll
```
### Requests

One or more Order request objects can be provided in a request as shown below
 **NOTE**: Date format in OrderId is ```QF-{DD/MM/YYYY}-{Index}```. 
 ```sh
 POST http://localhost:5000/api/order/{clientId}}
 ```
```sh{
  "stocks": [
    {
      "weight": <decimal>,
      "notionalAmount": <decimal>,
      "orderId": <string>,
      "currency": <string>,
      "orderType": <enum>,
      "destination": <string>
    },
    {
      "weight": <decimal>,
      "notionalAmount": <decimal>,
      "orderId": <string>,
      "currency": <string>,
      "orderType": <enum>,
      "destination": <string>
    }
  ],
  "symbol": <string>
}
```