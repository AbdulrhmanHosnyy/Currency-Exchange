using CurrencyExchangeAPI.Helpers;
using CurrencyExchangeAPI.Responses;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace CurrencyExchangeAPI.Handlers
{
	public class CurrencyExchangeQueryHandler
	{
		#region Variables
		private readonly CurrencyExchangeSettings _settings;
		private readonly IMemoryCache _cache;
		private readonly ILogger<CurrencyExchangeQueryHandler> _logger;
		#endregion

		#region Constructors
		public CurrencyExchangeQueryHandler(IOptions<CurrencyExchangeSettings> settings, IMemoryCache cache, ILogger<CurrencyExchangeQueryHandler> logger)
		{
			_settings = settings.Value;
			_cache = cache;
			_logger = logger;
		}
		#endregion

		#region Methods
		public async Task<FetchMultiCurrenciesResponse> ExchangeCurrency(string from, List<string> to)
		{
			if (from == null || to == null || to.Count == 0)
				throw new HttpRequestException("Invalid input. 'From' and 'To' currencies must be provided.");

			if (from.Length != 3 || to.Any(x => x.Length != 3))
				throw new HttpRequestException("Invalid input. 'From' and 'To' currencies must be of size 3.");

			string cacheKey = $"{from}_{string.Join("_", to)}";

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			if (_cache.TryGetValue(cacheKey, out FetchMultiCurrenciesResponse cachedResponse))
			{
				stopwatch.Stop();
				TimeSpan elapsedTime = stopwatch.Elapsed;
				_logger.LogInformation($"Response served from cache. Time taken: {elapsedTime.TotalMilliseconds} ms");
				return cachedResponse!;
			}

			string toCurrenciesString = string.Join(",", to);
			string apiUrl = $"{_settings.BaseUrl}/fetch-multi?api_key={_settings.ApiKey}&from={from}&to={toCurrenciesString}";

			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Accept", "application/json");

				HttpResponseMessage response = await client.GetAsync(apiUrl);

				stopwatch.Stop();
				TimeSpan elapsedTime = stopwatch.Elapsed;

				if (response.IsSuccessStatusCode)
				{
					string responseBody = await response.Content.ReadAsStringAsync();

					FetchMultiCurrenciesResponse exchangeResponse =
						Newtonsoft.Json.JsonConvert.DeserializeObject<FetchMultiCurrenciesResponse>(responseBody)!;

					// Cache the response
					_cache.Set(cacheKey, exchangeResponse, TimeSpan.FromMinutes(5));

					_logger.LogInformation($"Response fetched from API. Time taken: {elapsedTime.TotalMilliseconds} ms");

					return exchangeResponse!;
				}
				else
				{
					throw new HttpRequestException($"Invalid From or To code!");
				}
			}
		}
		#endregion

	}
}
