# random_bank


## Things to add

1. Complete the README.md file
2. Complete the end to end process for each API
3. Decide what db to use if any 
4. Add Kafka Debezium for CDC
4. Containerise the application




### Potential technologies to look into 

- MassTransit for transactional outbox
- Ef core for db or Marten db for event sourcing
- Api gateway


# DDD Notes

## Aggregates and Aggregate Roots


## Value Objects

These are where their properties is what defines them, e.g. an address wouldn't have an Guid ID associated because its the combination of its properties which gives it uniqueness. They are immutable


## Layers in the system

Layers should only point inwards i.e. the domain layer shouldn't reference any of the layers mentioned above it

### Api Layer

Very thin layer which is only concerned with validating incoming requests, mapping them to commands or queries and constructing appropriate responses

### Application Layer

Purely the logic for how the application works, ties together the business logic which takes place in the Domain layer with underlying processes which actually carry out the application concerns e.g. savings things to the database or sending an email (note that the gubbins which actually goes into implementing these things doesn't live here - just the calls to do those things)

### Infrastructure Layer

The gubbins layer - this is where the business happens

## Repositories 


### Domain Layer

Business logic using business language 

## Domain Events

# Questions

How do domain events get sent between aggregates 

What should the scope of a bounded context / aggregate be 