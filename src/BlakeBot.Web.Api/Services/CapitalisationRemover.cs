using BlakeBot.Web.Api.Configuration;
using Microsoft.Extensions.Options;

namespace BlakeBot.Web.Api.Services
{
	public class CapitalisationRemover : ICapitalisationRemover
	{
		private readonly IRandomiser _randomiser;
		private readonly Thresholds _thresholds;

		public CapitalisationRemover(IRandomiser randomiser, IOptions<Thresholds> thresholds)
		{
			_randomiser = randomiser;
			_thresholds = thresholds.Value;
		}

		public string RemoveCapitalisation(string input)
		{
			var num = _randomiser.Next(100);

			return num <= _thresholds.ChanceToRemoveCapitals ? input.ToLower() : input;
		}
	}
}
