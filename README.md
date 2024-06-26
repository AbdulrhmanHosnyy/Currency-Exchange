# CurrencyExchangeAPI
CurrencyExchangeAPI is a .NET Core web API application that provides currency exchange rate information. It integrates with external APIs to fetch real-time exchange rates and supports caching for improved performance.

## Features
- Fetch exchange rates for multiple currencies (fetch-multi endpoint).
- Integrated with Swagger for API documentation and testing.
- Utilizes memory caching to optimize API response times.
- Configured with logging to console for application monitoring.

## Prerequisites
- .NET 6 SDK
- Visual Studio or Visual Studio Code (optional)

## API Endpoints
- Fetch Exchange Rates
  - Endpoint: /api/CurrencyExchange/Fetch-Multi-Currency-Rates
  - Method: GET
  - Query Parameters:
    - from (string, required): The base currency code.
    - to (array of strings, required): List of target currency codes.
  - Response:
    - 200 OK: Returns a JSON object with exchange rates for specified currencies (FetchMultiCurrenciesResponse).
    - 400 Bad Request: If the request is invalid, returns an error message.

## Configuration
Update appsettings.json to configure API base URL (BaseUrl) and API key (ApiKey) for external currency exchange service.

## Dependencies
- Swashbuckle.AspNetCore: Swagger UI and API documentation.
- Microsoft.Extensions.Caching.Memory: Memory caching for API responses.
- Microsoft.Extensions.Logging.Console: Console logging provider.
