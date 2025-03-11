## Technical explanation

## Oauth2.0 and OpenID connect

Oauth2 basic concept: An Authorization server provides an access token to the client. The client can then use this in subsequent requests to can access to protected resources on an application or data server.

OpenID Connect (OIDC) is an identity layer built on top of OAuth 2.0. While OAuth 2.0 focuses on authorization (granting access to resources), OIDC adds an authentication layer, enabling it to verify the identity of a user and provide additional user-related information.

### KeyCloak

Can start in dev mode which adds default configuration using the command start-dev

Realms in KeyCloak represent environments i.e. Prod, Stg, Dev

Clients represent applications

http://keycloakhost:keycloakport/realms/{realm}/.well-known/openid-configuration

Set up a realm e.g. Dev
In realm settings allow user registration
Set up a client within that realm e.g. Random-Bank
Add origin and valid redirect URLS

### Questions

Need a way to hook into these events from the IDP or maybe we don't if each request is going to come with a auth token?
