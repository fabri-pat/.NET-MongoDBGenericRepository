# Mongo DB Base Repository

A generic asynchronous repository implementation using the MongoDB .NET driver.

Inspired by Spring Data Jpa.

Official stable relase available from [nuget.org](https://www.nuget.org/packages/MongoDbBaseRepository).

# How to use

This repository can be used in two ways:

-   use standard implementation.
-   inheriting the standard implementation to override/extend standard functionalities

# Common steps

> In your `appsettings.json` you have to specify adding mongoDB setting:

-   the connection string
-   the name of the MongoDB database you want to connect with.

Example:

```json
"MongoSettings": {
    "ConnectionString": [CONNECTION_STRING],
    "DatabaseName": [DATABASE_NAME]
}
```

> In your `program.cs` file you have to add mongo instance database with:

```csharp
builder.Services.AddMongo();
```

> The CLASS_TYPE you want to manage MUST implement `IEntity` interface typed by the id type.

Example:

```csharp
public class User : IEntity<Guid>
{
    public User(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; init; }
}
```

# Using base implementation

> You have to specify the collection name and what type of entity and the type of its ID to manage.
> It can be simply done with this line of code:

```csharp
builder.Services.AddMongoRepository<[CLASS_TYPE], [ID_TYPE]>
```

> **Last step:**
>
> You have to inject in the class where you want to access to mongoDB data the typed IRepository class.

Example:

```csharp
public class UserService : IUserService
{
    private readonly IRepository<User, Guid> userRepository;  

    public UserService(IRepository<User, Guid> userRepository)
    {
        this.userRepository = userRepository;
    }
}
```

## _THAT'S ALL!_

#

# Extend base functionalities

> You have to create your own repository interface extending `IRepository<T, K>` interface

Example:

```csharp
public interface IUserRepository : IRepository<User, Guid> {}
```

> Implement the created interface injecting `IServiceProvider` to call inherited constructor providing its value and the name of collection you want to manage.

Example:

```csharp
public class UserRepository : MongoRepository<User, Guid>, IUserRepository
{
    public UserRepository(IServiceProvider serviceProvider)
        : base(serviceProvider, typeof(User).Name){}
}
```

> Register the dependency injection for the created interface and its implementation

Example:

```csharp
builder.Services.AddScoped<IUserRepository, UserRepository>();
```

> **Last step:**
>
> Inject the interface in the class where you need to access data

Example:

```csharp
public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(
        IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
```
## _THAT'S ALL!_

#

# LICENSE

The MIT License (MIT)

Copyright (c) 2023 Fabrizio Patruno

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
