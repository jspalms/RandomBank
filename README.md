# random_bank

## Application overview:

Its a dummy bank system used to learn new concepts and act as a solid base for a DDD, clean code Asp.net api.

There are two disparate applications which aren't split out to represent micro-services (a) Users (b) Accounts



There is a job which runs on a schedule to apply interest to the account

The user is notified when an action happens on the account

Admin can add and edit products i.e. a kind of bank account

## Concepts covered

- Authentications by OAuth2.0
- DDD and clean code principles
- Transactional Outbox
- Optimistic locking - should be very easy
- Producing and consuming to kafka

## Things to add

1. Finish keycloak setup
2. Add kafka to produce integration event off the back of a domain event
3. Add some endpoint with an authentication policy
3a. Add logging - make it structured with tracing and look nice
4. Containerise it
5. Finish the readme
6. Add a frontend?
7. Policy based auth for admins to use endpoints and view specific actions
8. HTTPS

### Down the line

- Api gateway
- Open telemetry for logging through seq (or similar) - want to trace through the application
- Deploy to AWS
- Add certificate for HTTPS

# Future projects

- Event sourcing version
- Temporal version
-
