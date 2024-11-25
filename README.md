# random_bank

## Things to add

1. Define the models and build migrations
2. Add an optimistic locking mechanism to the account aggregate
3. Add a transactional outbox for committing integration events
4. Add kafka to produce and consume events

a. Containerize the application
b. Add authorization using some IdAAS

### Potential technologies to look into

- MassTransit for transactional outbox
- Api gateway
- Open telemetry for logging through seq (or similar)

# DDD Notes

## Aggregates and Aggregate Roots

Aggregates are a cluster of associated objects that we treat as a single unit for the purpose of data changes. Each aggregate has a root and a boundary. The root is a single entity within the aggregate that is responsible for maintaining the integrity of the aggregate. The boundary is the set of all objects within the aggregate. The root is the only object that outside objects can hold references to. The root is responsible for ensuring that the aggregate remains in a consistent state.
Concurrent changes to the aggregate should be guarded against. There are 2 distinct ways of doing this. The first is optimistic concurrency control which is where the aggregate checks that the version of the aggregate that it has is the same as the version of the aggregate in the database. The second is pessimistic concurrency control which is where the aggregate locks the aggregate in the database so that no other process can change it.

## Domain Events

these are dispatched after the entity is saved, there is debate if these should be transactional or not but I've opted for them not to be. For more information see (this)[https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-events-design-implementation#single-transaction-across-aggregates-versus-eventual-consistency-across-aggregates].
Each event can have multiple handlers, each handler should be responsible for a single thing.

There is a question raised over consistency between aggregates and systems when using events, if there is a failure between the aggregate state being saved and the event being emmited then the system will be out of sync.
This can be mitigated by using a transactional outbox but I'm unsure if this overhead is necessary for domain events or if its more of a consideration for integration events.

## Value Objects

These are where their properties is what defines them, e.g. an address wouldn't have an Guid ID associated because its the combination of its properties which gives it uniqueness. They are immutable

# Clean code notes

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

# Future projects

- Event sourcing version
- Temporal version
-
