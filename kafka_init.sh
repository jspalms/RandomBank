#!/bin/bash
echo "Waiting for Kafka broker to be ready..."
sleep 20

echo "Creating Kafka topics..."
kafka-topics --bootstrap-server broker:29092 --create --if-not-exists --topic account-created --partitions 3 --replication-factor 1
kafka-topics --bootstrap-server broker:29092 --create --if-not-exists --topic transaction-created --partitions 3 --replication-factor 1
kafka-topics --bootstrap-server broker:29092 --create --if-not-exists --topic account-balance-updated --partitions 3 --replication-factor 1

echo "Kafka topics created successfully!"
echo "Listing all topics:"
kafka-topics --bootstrap-server broker:29092 --list