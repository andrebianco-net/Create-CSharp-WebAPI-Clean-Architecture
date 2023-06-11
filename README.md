# create-csharp-webapi-clean-architecture

## Overview:
Creating a C# Web API based on Clean Architecture.

This example is an adaptation based on an example used in a course I have done. So it is a mixed with that example and my knowledge. Check the references to know more about, please.

In order to know more aboute my career check my Linkedin profile, please.

https://www.linkedin.com/in/andrebianco-net/

## General Scope:

Product Registration Service implementation propose a small example of how to create a REST API using C#.

C# Web API .Net 7.0 based on Clean Architecture, using Web API, Repository, xUnit to test the implementation and some aspects of CQS (Command Query Separation)/CQRS (Command Query Responsability Separation). I tryed to follow the theory bellow.

The Solution will be a Web API/ RESTful which can be reached by a CRUD or an Integration from other Solution or by Postman tool.

The Domain will be defined by two entities, Product and Category:

1. Domain Product (child): Id (int, identity), Name (string), Description (string), Price (decimal), Stock (int), Image (string).
2. Domain Category (parent): CategoryId (int, identity), Name (string).

1:N relationship: One Category can have multiple Products.


## General Scope - Business rules for Product

1. Define functionality to display the products.
2. Define functionality to create a new product.
3. Allow to modify the product properties (Id can't be modified).
4. Define functionality to delete a product by Id.
5. Define relationship between Product and Category (Navigation property).
6. Do not allow to create an inconsistent state product (Creating a parameterized constructor).
7. Do not allow that product properties be changed externally (private setter).
8. Do not allow that product properties Id, Price and Stock have negative values.
9. Do not allow that product properties Name and Description be null or empty.
10. Allow the property image be null.
11. The Name attribute don't be less than 3 characters.
12. The Description attribute don't be less than 5 characters.
13. The Image don't be contain more than 250 characters.
14. The Image attribute shall be a http link.
15. Define business rules validation for the domain product.


## General Scope - Business rules for Category

1. Define functionality to display the categories.
2. Define functionality to create a new category.
3. Allow to modify the category properties (Id can't be modified).
4. Define functionality to delete a category by Id.
5. Define relationship between Category and Product (Navigation property).
6. Do not allow to create an inconsistent state category (Creating a parameterized constructor).
7. Do not allow that category properties be changed externally (private setter).
8. Do not allow that category properties CategoryId have negative value.
9. Do not allow that category properties Name be null or empty.
10. The Name attribute don't be less than 3 characters.
11. Define business rules validation for the domain category.


## General Scope - Data persistence

1. Use relational database: SQL Server.
2. Use a ORM tool: Entity Framework Core.
3. Use the Entity Framework Core Code-First approach in order to create a database and tables.
4. Use a database provider: Microsoft.EntityFrameworkCore.SqlServer.
5. Use a tool for apply the migrations: Microsoft.EntityFrameworkCore.Tools.
6. Decouple the access layer from ORM: Repository Pattern.


## General Scope - Nomenclature

1. Use the recommended nomenclature by Microsoft in order to name classes, methods, params and variables.
2. Use CamelCase. Ex: valueOfDiscount.
3. Use PascalCase. Ex: FirstName, CalculateIncomeTax();
4. Language defined: English.

|Resource|Nomenclature|Example|
|---|---|---|
|Class|PascalCase|Product, AddCategory|
|Interface|I + PascalCase|IUser, ICalculateTotal|
|Method, Property|PascalCase|Address, FirstName|
|Variable, Params|CamelCase|stock, taxValue|
|Constants|Capital with underscore|DISCONT_VALUE|


## Theory:

### S.O.L.I.D Principles

<ins>Single Responsability:</ins> "A class should have only one reason to change" - Uncle Bob. Related to Separation of Concerns where each one class/module have a unique responsability with their own methods and behaviors in the system. It promotes low coupling (don't instantiate directly, use dependency injection) and high cohesion (each class can't be able to exists without its methods and its methods are implemented inside never mixed in other place).

<ins>Open/Closed:</ins> Promotes re-utilization. Linked to polymorphism,  inheritance, extendance and overriding. A original class shall be able to be replaced for another class by extending an interface that the original class use and overriding a method with the same signature and result.

<ins>Liskov Substitution:</ins> A concept addition to Open/Closed. It preaches the same than the Open/Closed principle, but complement saying that if you will create a derived class to replace/add functionalities that one new need follow totaly the abstract class implemeting all definitions inside the contract without modifications.

<ins>Interface Segregation:</ins> "More interfaces are better than too little". Split the interfaces and abstract classes ever it is possible. Use extension to build a relation either them.

<ins>Dependency Inversion:</ins> An abstraction shouldn't depend of details from an implementation, but the details of an implementation needs depend from an abstraction.


### Separation of Concerns - SoC

Linked to Solid Single Responsibility, explained above, and DRY - Don't Repeat Yourself, that talks about don't create more lines of the same code.

### Dependency Inversion Principle - DIP

One business class shouldn't depends neither infrastructure class nor any other concrete class, but both of them need to know their own abstractions.

The Inversion of Dependency occurs when we inject (Inject of Dependency) a concrete implementation inside a class's constructor using the interface of that class injected. When it happens we use the mechanic of Inversion of Control provided to create an instantiation of class. A IoC container is provide by .Net Core (Microsoft.Extensions.DependencyInjection). 

## How to run this project

#### 1. Clone project:

$ git clone https://github.com/andrebianco-net/create-csharp-webapi-clean-architecture.git

#### 2. Update file appsettings.json with a valid connection string:

"ConnectionStrings": {
    "DefaultConnection": "Data Source=XXXXXX;Initial Catalog=XXXXXX;User Id=XXXXXX;Password=XXXXXX;TrustServerCertificate=true;"
},

#### 3. Use migrations commands:

<ins>Create migrations:</ins> dotnet ef migrations add InitialDatabase --project ProductRegistrationService.Infra.Data/ProductRegistrationService.Infra.Data.csproj --startup-project ProductRegistrationService.WebAPI/ProductRegistrationService.WebAPI.csproj --verbose

<ins>Update database:</ins> dotnet ef database update --project ProductRegistrationService.Infra.Data/ProductRegistrationService.Infra.Data.csproj --startup-project ProductRegistrationService.WebAPI/ProductRegistrationService.WebAPI.csproj --verbose

<ins>Remove migrations:</ins> dotnet ef migrations remove --project ProductRegistrationService.Infra.Data/ProductRegistrationService.Infra.Data.csproj --startup-project ProductRegistrationService.WebAPI/ProductRegistrationService.WebAPI.csproj --verbose

<ins>List migrations:</ins> dotnet ef migrations list --project ProductRegistrationService.Infra.Data/ProductRegistrationService.Infra.Data.csproj --startup-project ProductRegistrationService.WebAPI/ProductRegistrationService.WebAPI.csproj --verbose

#### 4. Compile project:

$ dotnet build

#### 5. Test project:

$ dotnet test

#### 6. Run project:

$ dotnet run --project ProductRegistrationService.WebAPI/ProductRegistrationService.WebAPI.csproj

## References

Solid Principles with C# .NET Core with Practical Examples & Interview Questions. Pro Code Guide. From https://procodeguide.com/design/solid-principles-with-csharp-net-core/

SOLID Design Principles Explained in a Nutshell. A Dev' Story. From https://www.youtube.com/watch?v=69sfWNzxTMc

Clean Architecture Essencial - ASP .NET Core com C#. Macoratti. From https://www.udemy.com/course/clean-architecture-essencial-asp-net-core-com-c/