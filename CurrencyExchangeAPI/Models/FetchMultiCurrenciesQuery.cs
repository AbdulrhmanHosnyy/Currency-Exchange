namespace CurrencyExchangeAPI.Models
{
	public class FetchMultiCurrenciesQuery
	{
		public string? From { get; set; }
		public List<string>? To { get; set; }
	}
}
