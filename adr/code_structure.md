# Code Structure

## Status

Testing it out, trying to understand it more

## Context

Code structure is important because it enhances maintainability, collaboration, scalability, reusability, debugging, and performance. A well-structured codebase is easier to understand, modify, and extend, which is crucial for long-term project success and efficient teamwork.
Following defined patterns makes it easy for new developers see where things are and how they work. It also makes it easier for developers to know where to put new code.

## Decision

I will adopt Domain Driven Design (DDD) principles. This includes using patterns like entities, value objects, aggregates, repositories, and domain events. I will also attempt to define clear bounded contexts to separate different parts of the system.
I will also be using clean code practices to ensure that the code is easy to read and understand.

## Consequences

### Positive

- Improved collaboration between developers and domain experts.
- A software structure that closely reflects business concepts.
- Clear boundaries between different parts of the system, reducing unnecessary coupling.
- Better management of complex business logic.

### Negative

- Initial learning curve for the team to understand and apply DDD principles.
- Potential increase in development time due to the need for more detailed design and modeling.

## Technical explanation DDD

DDD emphasizes collaboration between developers and domain experts to create a shared ubiquitous language, ensuring that the software’s structure closely reflects business concepts. DDD introduces patterns like entities, value objects, aggregates, repositories, and domain events to manage complex business logic effectively. It also promotes bounded contexts, which define clear boundaries between different parts of the system, preventing unnecessary coupling and ensuring that each domain model remains consistent and meaningful within its own context.

## Aggregates and Aggregate Roots

Aggregates are a cluster of associated objects that we treat as a single unit for the purpose of data changes. Each aggregate has a root and a boundary. The root is a single entity within the aggregate that is responsible for maintaining the integrity of the aggregate. The boundary is the set of all objects within the aggregate. The root is the only object that outside objects can hold references to. The root is responsible for ensuring that the aggregate remains in a consistent state.
Concurrent changes to the aggregate should be guarded against. There are 2 distinct ways of doing this. The first is optimistic concurrency control which is where the aggregate checks that the version of the aggregate that it has is the same as the version of the aggregate in the database. The second is pessimistic concurrency control which is where the aggregate locks the aggregate in the database so that no other process can change it.

## Domain Events

these are dispatched after the entity is saved, there is debate if these should be transactional or not but I've opted for them not to be. For more information see (this)[https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-events-design-implementation#single-transaction-across-aggregates-versus-eventual-consistency-across-aggregates].
Each event can have multiple handlers, each handler should be responsible for a single thing.

There is a question raised over consistency between aggregates and systems when using events, if there is a failure between the aggregate state being saved and the event being emmited then the system will be out of sync.
This can be mitigated by using a transactional outbox but I'm unsure if this overhead is necessary for domain events or if its more of a consideration for integration events.

## Bounded Context

A bounded context is a logical boundary around a specific part of your domain model, defining a consistent language and rules for that domain. Each bounded context focuses on a specific business capability, like "Accounts" or "Identities," ensuring clarity and separation of concerns.

Each bounded context typically aligns to a single microservice, encapsulating its domain logic, database, and APIs.

## Shared Kernel

A Shared project goes slightly against the idea of bounded context but it only contain domain-related concepts that are truly shared across bounded contexts. These should be stable, business-relevant, and unlikely to change frequently per context.

Entities and Value objects, shared services, shared base implementations

## Value Objects

These are where their properties is what defines them, e.g. an address wouldn't have an Guid ID associated because its the combination of its properties which gives it uniqueness. They are immutable

# Technical explanation Clean code notes

## Layers in the system

Layers should only point inwards i.e. the domain layer shouldn't reference any of the layers mentioned above it and usually this is done by having interfaces in the domain layer which are implemented in the layers above it. This is because the domain layer is the most important and should be the most stable.

### Api Layer

Very thin layer which is only concerned with validating incoming requests, mapping them to commands or queries and constructing appropriate responses

### Application Layer

Purely the logic for how the application works, ties together the business logic which takes place in the Domain layer with underlying processes which actually carry out the application concerns e.g. savings things to the database or sending an email (note that the gubbins which actually goes into implementing these things doesn't live here - just the calls to do those things)

### Infrastructure Layer

The gubbins layer - this is where the business happens. Third party services should only be mentioned here. They should implement interfaces which are defined in the domain layer.

## Repositories

arguments that EF is already an abstraction over database connections and repository pattern is redundant. But Its a nice abstraction IMO (and its all I've ever used)

### Domain Layer

Business logic using business language

# Questions

How do domain events get sent between aggregates - is a transactional outbox needed or is in process messaging reliable enough?

What should the scope of a bounded context / aggregate be

if you're going to outbox integration events does it make sense to outbox domain events too?

I want my integration events to be transactional but they are raised in response to domain events which I've specifically raised after the aggregate is saved

What should the scope of a bounded context / aggregate be
