# random_bank

## Application overview:

Its a dummy bank system which can be to register as a user then open and interact with a bank account.

There are two disparate systems (a) Users system (b) Accounts system

User can sign-up and edit their details

User can open / close an account and deposit / withdraw funds

There is a job which runs on a schedule to apply interest to the account

The user is notified when an action happens on the account

Admin can add and edit products i.e. a kind of bank account

## Concepts to learn

- Authentication end to end on an ASP.NET application (Including configuring the IDP)
- DDD and clean code principles
- Transactional Outbox
- HTTPS configurations
- Optimistic locking - should be very easy
- Producing and consuming to kafka

## Things to add

1. Add kafka to produce and consume events
2. Add certificate for HTTPS
3. Containerize the application

### Potential technologies to look into

- Api gateway
- Open telemetry for logging through seq (or similar) - want to trace through the paplication
- Deploy to AWS

# Future projects

- Event sourcing version
- Temporal version
-
