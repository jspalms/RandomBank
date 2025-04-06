#!/bin/bash
set -e

# Keycloak Config
KEYCLOAK_URL="http://localhost:8080"
REALM_NAME="development"

ACCOUNTS_CLIENT_ID="random_bank_accounts"
ACCOUNTS_CLIENT_SECRET="accounts_client_secret"
ACCOUNTS_ROOT_URL="http://localhost:5250"
ACCOUNTS_HOME_URL="/swagger"
ACCOUNTS_REDIRECT_URIS='["/*"]'
ACCOUNTS_ALLOWED_ORIGINS='["+"]'

USERS_CLIENT_ID="random_bank_users"
USERS_CLIENT_SECRET="users_client_secret"
USERS_BASE_URL="http://localhost:5293"
USERS_HOME_URL="/swagger"
USERS_REDIRECT_URIS='["/*"]'
USERS_ALLOWED_ORIGINS='["+"]'


# Ensure kcadm.sh is accessible
export PATH=$PATH:/opt/keycloak/bin

# Wait for Keycloak to be ready using kcadm.sh instead of curl
until kcadm.sh config credentials --server "${KEYCLOAK_URL}" --realm master --user admin --password admin > /dev/null 2>&1; do
  echo "Waiting for Keycloak to start..."
  sleep 2
done

# Login to Keycloak
kcadm.sh config credentials --server "${KEYCLOAK_URL}" --realm master --user admin --password admin

# Check if the realm exists, create it if not
if ! kcadm.sh get realms | grep -q "\"id\":\"${REALM_NAME}\""; then
  echo "Creating realm ${REALM_NAME}..."
  kcadm.sh create realms -s realm=${REALM_NAME} -s enabled=true
fi

# Enable user registration in the development realm
echo "Enabling user registration in realm ${REALM_NAME}..."
kcadm.sh update realms/${REALM_NAME} -s 'registrationAllowed=true'

# Check if the user client exists, create it if not
if ! kcadm.sh get clients -r ${REALM_NAME} | grep -q "\"clientId\":\"${USERS_CLIENT_ID}\""; then
  echo "Creating client ${USERS_CLIENT_ID}..."
  USERS_CLIENT_ID_INTERNAL=$(kcadm.sh create clients -r ${REALM_NAME} -s clientId=${USERS_CLIENT_ID} -s enabled=true -s publicClient=false -s secret=${USERS_CLIENT_SECRET} -i)

  # Configure user client settings
  echo "Configuring client ${USERS_CLIENT_ID}..."
  kcadm.sh update clients/${USERS_CLIENT_ID_INTERNAL} -r ${REALM_NAME} \
    -s name="Random Users" \
    -s description="Client for managing user-related operations in Random Bank" \
    -s rootUrl="${USERS_BASE_URL}" \
    -s baseUrl="${USERS_HOME_URL}" \
    -s "redirectUris=${USERS_REDIRECT_URIS}" \
    -s "webOrigins=${USERS_ALLOWED_ORIGINS}"
fi

# Check if the account client exists, create it if not
if ! kcadm.sh get clients -r ${REALM_NAME} | grep -q "\"clientId\":\"${ACCOUNTS_CLIENT_ID}\""; then
  echo "Creating client ${ACCOUNTS_CLIENT_ID}..."
  ACCOUNTS_CLIENT_ID_INTERNAL=$(kcadm.sh create clients -r ${REALM_NAME} -s clientId=${ACCOUNTS_CLIENT_ID} -s enabled=true -s publicClient=false -s secret=${ACCOUNTS_CLIENT_SECRET} -i)

  # Configure account client settings
  echo "Configuring client ${ACCOUNTS_CLIENT_ID}..."
  kcadm.sh update clients/${ACCOUNTS_CLIENT_ID_INTERNAL} -r ${REALM_NAME} \
    -s name="Random Accounts" \
    -s description="Client for managing account-related operations in Random Bank" \
    -s rootUrl="${ACCOUNTS_ROOT_URL}" \
    -s baseUrl="${ACCOUNTS_HOME_URL}" \
    -s "redirectUris=${ACCOUNTS_REDIRECT_URIS}" \
    -s "webOrigins=${ACCOUNTS_ALLOWED_ORIGINS}"
fi