# BrasGames
Showcase of the ASP.NET: Minimal API (.NET REST API), LINQ, Identity Framework, Entity Framework, Microservices, DTOs, Seeds, etc and SQlite.
[![Status](https://img.shields.io/badge/status-active-success.svg)]()
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](/LICENSE)

---

## The Journey Begins (The Problem)
I am a stubborn brazilian gamer ready to give my life to realize my dream: Become rich by only selling brazilian games (yes, I am crazy), but as a solo entrepeneur I have to create an API to manage the products, staff and the money of my business. Can I succeed or will I fail miserably and drive my game store to bankruptcy?

## The Solution
A Minimal API will authenticate and authorize users based on their role (Owner, Employee, or Client), restricting access to critical API endpoints accordingly. The API allows Clients to search for available or specific Games, Controllers, and Consoles in the store. Employees can perform all Client actions, plus add new products, delete individual products, and update existing products in the Service database. The Owner can perform all Employee actions, plus delete all products at once and access the Business database to get, post, put, and delete data related to daily business performance and employee information. While this won’t guarantee success, it will certainly make managing the Store easier.

## 📝 Table of Contents

- [Getting Started](#getting_started)
- [Usage](#usage)
- [Built Using](#built_using)
- [Authors](#authors)


## 🏁 Getting Started <a name = "getting_started"></a>

 - The Authentication and Authorization of the user is handled by [Default Identity Enpoints](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-8.0) + developer defined (made by me) /logout, /client-confirm and /employee-confirm enpoints. All auth enpoints can be access with domain/identity/desired-endpoint.

 - The product related endpoints can be accessed by domain/service/desired-endpoint -> service/controller, service/game and service/console and service/controller/id, etc for operations on a specific product.

 - The business related endpoints can be accessed by domain/business/desired-endpoint -> business/employee, business/agenda and business/agenda/id, etc for operations on a specific product.

 - Non query enpoints have don't have query parameters.

 - All business and service endpoints have GET and DELETE query operations and can be accessed by /service/game/query/, /business/agenda/query, etc. The query is made with the url query string.The names of the query's non numerical parameters are the name of the object's non numerical properties, the numerical properties have two equivalent query parameters to determine their range: priceLowerBound and priceUpperBound for example. (numerical query parameters are always followed by LowerBound and UpperBound).

### Prerequisites

[.NET SDK](https://learn.microsoft.com/en-us/dotnet/core/install/windows)
[SQlite](https://www.sqlite.org/download.html)

#### ArchLinux
```
sudo pacman -S dotnet-sdk
sudo pacman -S sqlite
```

#### Windows
Information can be found in [Built Using](#built_using) links


## 🎈 Usage <a name="usage"></a>

For these to work the application must be running.
If you logged in with cookies enabled remove "Authorization Bearer {key}" from the request header.

#### REST Client VsCode Extension

Go to BrasGames/Identity/HttpDemos/ and BrasGames/HttpDemos/ folders for pre-made API endpoint requests.

#### Curl and Postman

You can also make requests with Curl or through Postman. Here are some curl request examples to my API:

**1. Logout Endpoint** (Requires Authorization):
```c
  curl -X POST http://yourapi.com/identity/logout \
  -H "Authorization: Bearer YOUR_TOKEN_HERE" \
  -H "Content-Type: application/json" \
  -d '{}'
```
**2. Get All Games Endpoint** (Requires Authorization):
```c
curl -X GET http://yourapi.com/service/game/ \
-H "Authorization: Bearer YOUR_TOKEN_HERE"
```
**3. Delete All Employees Endpoint** (Requires Authorization):
```c
curl -X DELETE http://yourapi.com/business/employee/ \
-H "Authorization: Bearer YOUR_TOKEN_HERE"
```


## 🚀 Deployment <a name = "deployment"></a>

To be added

## ⛏️ Built Using <a id="built-using" name = "built_using"></a>

- <p> - 
  <a href="https://learn.microsoft.com/pt-br/dotnet/csharp/" target="_blank" rel="noreferrer"> 
    <img src="https://github.com/devicons/devicon/blob/master/icons/csharp/csharp-line.svg" alt="csharp" width="40" height="40"/> 
  </a> ->  Programming Language
</p>
<p>
- [ASP.NET CORE](https://dotnet.microsoft.com/pt-br/apps/aspnet) - Framework
</p>
<p>
- [SQLite](https://www.sqlite.org/) - Database
</p>

## ✍️ Authors <a name = "TiagomSalviano"></a>

- [@TmSalviano](https://github.com/TmSalviano) - Idea & Complete Implementation.

