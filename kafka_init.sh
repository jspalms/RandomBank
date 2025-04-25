#!/bin/sh

# Wait for Kafka to be ready
echo "Waiting for Kafka to be ready..."
sleep 10

# Create 'users' topic
kafka-topics --bootstrap-server broker:9092 --create \
  --topic users --partitions 1 --replication-factor 1 \
  --if-not-exists

# Create 'accounts' topic
kafka-topics --bootstrap-server broker:9092 --create \
  --topic accounts --partitions 1 --replication-factor 1 \
  --if-not-exists

echo "Kafka topics 'users' and 'accounts' created (if they didn't already exist)."