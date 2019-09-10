using BlakeBot.Web.Api.Configuration;
using Microsoft.Extensions.Options;

namespace BlakeBot.Web.Api.Services
{
	public class WordMuddler : IWordMuddler
	{
		private readonly IRandomiser _randomiser;
		private readonly Thresholds _thresholds;

		public WordMuddler(IRandomiser randomiser, IOptions<Thresholds> thresholds)
		{
			_randomiser = randomiser;
			_thresholds = thresholds.Value;
		}

		public string MuddleWord(string input)
		{
			var arr = input.ToCharArray();

			for (var i = 0; i < arr.Length - 1; i++)
			{
				if (char.IsLetterOrDigit(arr[i]) && char.IsLetterOrDigit(arr[i + 1])
						&& _randomiser.Next(100) <= _thresholds.ChanceToSwapCharacters)
				{
					// swap letters
					var a = arr[i];
					arr[i] = arr[i + 1];
					arr[i + 1] = a;
					i++;
				}
			}

			return new string(arr);
		}
	}
}
