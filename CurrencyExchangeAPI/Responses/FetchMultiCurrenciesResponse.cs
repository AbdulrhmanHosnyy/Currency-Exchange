namespace CurrencyExchangeAPI.Responses
{
	public class FetchMultiCurrenciesResponse
	{
		public string? Base { get; set; }
		public Dictionary<string, decimal>? Results { get; set; }
	}
}
