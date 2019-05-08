# Custom-Manual-ORM
## A Simple example of a Custom Manual ORM
### Using .net standard 2.0, it's a repository manager that map entities /code manually/ and allow it to have a simple ORM.
#### The initial idea is using  a /database first/ manually implementing a dictionary with <property,database column> or using a data annotation attibute([Field(Name = "customName")]) or simply reading the class property to map from/to db. and then we are figuring out a way to develop such as a /code first/ and so on.

### Tip: On the Example Custom.Manual.ORM.Domain:"DemoClass" instead of using class on name,just use the context such as "Car" or "Customer"

#### Next steps: The idea is to elaborate the cache project and let the Custom-Manual-ORM grow to be able to let user decide when to cache or not, when to commit its data or not.
#### Next steps: Also to let possible to implement "Unit of work pattern"


### Custom.Manual.ORM.Base
#### Abstractions for Entity Manager ans SQL scripts base abstracted classes to consume data information from some DB (SQL Server based in the moment).

### Custom.Manual.ORM.Data
#### Database Connection and Settings elements consumed by the Custom.Manual.ORM.Base project.

### Custom.Manual.ORM.Domain
#### It represent the project you'1l have to work on modeling, so you can create your own Domain project to do it.
#### Meant to model the Domain Entities and it point to Custom.Manual.ORM.Base to implement its Interfaces and to model with its Data Fields.

### Custom.Manual.ORM.Data.Repositories
#### The project that agredate all the others, where you'll be creating the context of use and will be used by your project
#### So it refer to Custom.Manual.ORM.Base, Custom.Manual.ORM.Data and Custom.Manual.ORM.Domain.

### OS Tested
#### Windows Platforms X86 and X64
#### MacOS 10.13.** and higer versions

### Visual Studio
#### Windows Visual Studio 2017
#### Visual Studio for MacOS 7.8.3
#### Visual Studio for MacOS 8.0 Preview
