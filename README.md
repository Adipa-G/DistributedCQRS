# Abstract CQRS

Abstract CQRS is a implementation of CQRS pattern with event sourcing. The design accommodates the scalability of the application and geared towards micro services architecture. 

The design allows combining of multiple micro services into one larger service at initial stages of the application, in order to reduce the deployment complexity. These can be split into different services as required and with minimal effort. 

Abstract CQRS is designed to target .Net Standard 2.0, the sample projects are .Net Core 2.0

## Design

### IRoot
Represents an aggregate root.

### ICommand
Represents a command.

### IEvent
Represents an event generated as a result of command processing.

### Services
Individual microservice is represented in the service. `AbstractCqrs.Core.Services` namespace contains infrastructure to implement a service. Easiest approach is to subclass the `BaseService` class. Each service has a Id and needs to subscribe to a list of buses where service receives commands.

### Service Locator
There are 2 interfaces needs to be implemented to provide necessary functionality. A set of utility classes are provided to make the service registration easier. 

`IRootTypeResolver` interface is used to resolve the Aggregate root type for a given command. 

`IServiceLocator` is a general service locator use to resolve different types of services.

`IUnitOfWorkFactory` is used to create unit of work.

`ServiceRegistrationAttribute` used to mark services in order to resolve them using provided class `ResolveUtils`. it provides method to resolve many of the services required for the `IRootTypeResolver` and `IServiceLocator`.

### Event Store
Name says it all. Easiest approach is to extend `BaseEventStore` class, and implement the abstract methods.

### Command / Events
`ICommandHandler` and `IEventHandler` interfaces has to be implemented for each command / event. 

`CommandProcessor` is the heart of the system and it coordinates with `CommandHandler` / `EventHandler` and `EventStore`.

### View Writer
`IViewWriter` is simple interface provided to update the views once the command is processed and resulting events are applied to the aggregate root.

## Example
There is a detailed example implementation provided for your reference in `AbstractCqrs.Sample.*` namespace. Enjoy!
