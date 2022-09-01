# LoginTreeMerkle

Demo project, which use Merkle's tree for save user data and password.
Uses a library [FastEndpoints](https://github.com/FastEndpoints/Library), which allows to create fast Web API.

Angular is a client - side, for easy access to WebAPI.

Database, back - end and front - end is launching separate.

## Live demo

**Work in progress**

## How launch it?

### Database

You only have to set up MS SQL (e.g. docker) and get connection string.

### Back - end

1. You have to have .NET 6.0 or higher.
2. ```dotnet restore```
3. Create ```appsettings.json```, which has to look like this:
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionString": "Your_connection_string",
  "Salt": "any_salt",
  "JWTSigninKey": "jwt sign in key"
}
```

### Front - end

In environment, for ```api_url``` write your url. Then, just ```npm start```. 

**That`s all!**