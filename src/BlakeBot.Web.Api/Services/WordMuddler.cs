using System.Linq;
using System.Text.RegularExpressions;
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
			if (input.All(char.IsPunctuation))
			{
				return input;
			}

			var noPrecedingPunctuation = Regex.Replace(input, $"^[^a-zA-Z]*", string.Empty);
			var precedingPunctuation = input.Substring(0, input.Length - noPrecedingPunctuation.Length);
			var noTrailingPunctuation = Regex.Replace(noPrecedingPunctuation, $"[^a-zA-Z]*$", string.Empty);
			var trailingPunctuation = noPrecedingPunctuation.Substring(noTrailingPunctuation.Length);

			var arr = noTrailingPunctuation.ToCharArray();

			for (var i = 0; i < arr.Length - 1; i++)
			{
				var chance = i == 0 || i == arr.Length - 2
					? _thresholds.ChanceToSwapFirstAndLastCharacters
					: _thresholds.ChanceToSwapCharacters;

				if (char.IsLetterOrDigit(arr[i]) && char.IsLetterOrDigit(arr[i + 1])
						&& _randomiser.Next(100) <= chance)
				{
					// swap letters
					var a = arr[i];
					arr[i] = arr[i + 1];
					arr[i + 1] = a;
					i++;
				}
			}

			return precedingPunctuation + new string(arr) + trailingPunctuation;
		}
	}
}
