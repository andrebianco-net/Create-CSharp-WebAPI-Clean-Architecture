# create-csharp-webapi-clean-architecture

## Overview:
Creating a C# Web API based on Clean Architecture.


## Context:

Product Registration Service implementation propose a small example of how to create a REST API using C#.

C# Web API based on Clean Architecture, using some aspects of CQRS, Web API, Repository and xUnit to test the implementation. I tryed to follow the theory bellow.

The Solution will be a Web API/RESTful which can be reached by a CRUD or an Integration from other Solution or by Postman tool.

The Domain will be defined by two entities, Product and Category:

1. Domain Product (child): Id (int, identity), Name (string), Description (string), Price (decimal), Stock (int), Image (string).
2. Domain Category (parent): CategoryId (int, identity), Name (string).

1:N relationship: One Category can have multiple Products.


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

The Inversion of Dependency occurs when we inject (Inject of Dependency) a concrete implementation inside a class's constructor using the interface of that class injected. When it happens we use the mechanic of Inversion of Control provided to create an instantiation of class.

## References

Solid Principles with C# .NET Core with Practical Examples & Interview Questions. Pro Code Guide. From https://procodeguide.com/design/solid-principles-with-csharp-net-core/

SOLID Design Principles Explained in a Nutshell. A Dev' Story. From https://www.youtube.com/watch?v=69sfWNzxTMc

Clean Architecture Essencial - ASP .NET Core com C#. Macoratti. From https://www.udemy.com/course/clean-architecture-essencial-asp-net-core-com-c/