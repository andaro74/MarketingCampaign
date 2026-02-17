# MarketingCampaign

`MarketingCampaign` is a sample project that demonstrates a game company marketing campaign via REST APIs. It exposes sweepstakes data for digital video games so the Game Company website and partner sites can display current sweepstakes to attract customers.

## API

- `GET /sweepstakes`: Returns the list of available sweepstakes.
- `GET /sweepstakes/{sku}`: Returns a sweepstake by SKU.

## Docker Compose

The compose stack includes:

- `marketingcampaign`: Builds the .NET 10 API from the local `Dockerfile`.
- `dynamodb`: Runs Amazon DynamoDB Local for development storage.
- `kafka`: Runs Apache Kafka (KRaft mode) for local messaging.
- `kafka-ui`: Runs Kafka UI for browsing topics and brokers.

Run all containers:

`docker compose up -d`

## OpenAPI

OpenAPI is enabled in development. After running the app, view:

- `http://localhost:8080/openapi/v1.json`
- `http://localhost:8080/openapi/v1.yaml`
