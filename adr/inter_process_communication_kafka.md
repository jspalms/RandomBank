# Inter process communication via kafka

## Status

Accepted

## Context

I'm modeling this system with a "Microservice" architecture, this seems like a contentious approach with many people now advocating the initial use of monoliths and only splitting out systems when its appropriate. That being said, I want to have an example of how communication can happen between systems so lets pretend it makes perfect sense to have things split out.

smaller disparate systems offer a number of advantages:

1. Independent scaling 
2. Independent development 
3. Easier maintenance (theoretically should be to remove one component without taking the whole system down)
4. Fault tolerance - similarly to easier maintenance, taking one system out shouldn't down the whole application
5. Improved domain modeling - the system can be viewed with reference to its single use

However there needs to be a robust approach to inter-process communication between the systems. 

## Decision

Microservices will communicate via kafka. This allows them to function independently i.e. a producer need not have any awareness of a consumer. It allows the publish subscribe model which is preferable over the single point to point messaging when many systems need to be aware of changes in one

Alternatives include: 

- REST api calls, tightly couples the application, single point to point communication
- Message Queues, no audit log, usually messages are only from one system to another (although this can be changed with additional more overhead), message duplication when adding more consumers

There are many more advantages to using kafka, resilience, high throughput etc.

## Consequences

Eventual consistency 

Data replication across systems 

Decoupled 

Need to host the kafka eco system 

Need appropriate configuration of publishers and consumers 

either need a central repository for message schemas or publishers need to expose message schema 

