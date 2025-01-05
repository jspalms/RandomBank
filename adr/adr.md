# What is an ADR?

## Status

Accepted - because its only me making the decision...

## Contexts

I wanted a way of explaining some of the thoughts within this application but wanted to group those explanations with the source code itself. I want to be able to do more long form explanations (rather than comments in the code), provide source material for further reading, and include diagrams.

## Decision

ADRs (architectural decision records) are a popular way of capturing decisions around architecture so I thought that these might be a good way of explaining concepts whilst being a good example of how to document decisions within real-world applications. They can be in a variety of formats but should be consistent through the project. For instance here I will follow this  by Michael Nygard:

```markdown
# Title

## Status

What is the status, such as proposed, accepted, rejected, deprecated, superseded, etc.?

## Context

What is the issue that we're seeing that is motivating this decision or change?

## Decision

What is the change that we're proposing and/or doing?

## Consequences

What becomes easier or more difficult to do because of this change?

## Technical explanation

More detailed technical information about the proposed solution (if required)
```

ADRs should focus on a single topic and can be as long or as short as required to put across the required decision. I'm unsure on the level of decision which warrants being included in an ADR but I think having a rationale provided for most things is better than having nothing. No-one would ever complain about being given too much of an explanation right?

Common concepts which are captured in an ADR: 

1. High level architecture e.g. tech stack, microservice vs monolith, database variety 
2. Design patterns e.g. Event sourced, event driven, 
3. Communication and integration e.g. Rest API, which message broker
4. Third party providers - pretty obvious what that one means
5. Standards and best practices e.g. coding styles, how to integrate into the team, testing
6. Deployment and development e.g. CI/CD pipelines, local containers etc.

For a more in-depth explanation see [here](https://github.com/joelparkerhenderson/architecture-decision-record?tab=readme-ov-file)


## Consequences.  

### Pros
- I'll have a format to follow which should provide more structure to explanations. 
- Event if the format isn't great then it provides a kicking off points. 
- This one document will give me a good way to look back at what ADR's are.

### Cons
- The ADR format might not be entirely suited for explanations.
- The ADR format might be binned off.
