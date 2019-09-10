using System.Text;
using BlakeBot.Web.Api.Configuration;
using Microsoft.Extensions.Options;

namespace BlakeBot.Web.Api.Services
{
	public class PunctuationRemover : IPunctuationRemover
	{
		private readonly IRandomiser _randomiser;
		private readonly Thresholds _thresholds;

		public PunctuationRemover(IRandomiser randomiser, IOptions<Thresholds> thresholds)
		{
			_randomiser = randomiser;
			_thresholds = thresholds.Value;
		}

		public string RemovePunctuation(string input)
		{
			var stringBuilder = new StringBuilder(input);
			for (var i = input.Length - 1; i >= 0; i--)
			{
				if (char.IsPunctuation(input, i) && _randomiser.Next(100) <= _thresholds.ChanceToRemovePunctuation)
				{
					stringBuilder.Remove(i, 1);
				}
			}

			return stringBuilder.ToString();
		}
	}
}
