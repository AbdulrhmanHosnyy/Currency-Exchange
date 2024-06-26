using CurrencyExchangeAPI.Handlers;
using CurrencyExchangeAPI.Models;
using CurrencyExchangeAPI.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchangeAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CurrencyExchangeController : ControllerBase
	{
		#region Variables
		private readonly CurrencyExchangeQueryHandler _queryHandler;
		#endregion

		#region Constructors
		public CurrencyExchangeController(CurrencyExchangeQueryHandler queryHandler)
		{
			_queryHandler = queryHandler;
		}
		#endregion

		#region Controllers
		/// <summary>
		/// Fetches exchange rates for multiple currencies.
		/// </summary>
		/// <param name="query">Query parameters including 'from' and 'to' currencies.</param>
		/// <returns>Exchange rates for specified currencies.</returns>
		[HttpGet("Fetch-Multi-Currency-Rates")]
		[ProducesResponseType(typeof(FetchMultiCurrenciesResponse), 200)]
		[ProducesResponseType(typeof(string), 400)]
		public async Task<ActionResult<FetchMultiCurrenciesResponse>> FetchMultiCurrencies([FromQuery] FetchMultiCurrenciesQuery query)
		{
			try
			{
				FetchMultiCurrenciesResponse exchangeResponse =
					await _queryHandler.ExchangeCurrency(query.From!, query.To!);

				return Ok(exchangeResponse);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		#endregion

	}
}
