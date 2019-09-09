using System.Text;
using System;

namespace BlakeBot.Web.Api.Services {
	public class PunctuationRemover : IPunctuationRemover {
		private const int Threshold = 25;

		private readonly IRandomiser _randomiser;

		public PunctuationRemover(IRandomiser randomiser) {
			_randomiser = randomiser;
		}

		public string RemovePunctuation(string input) {
			StringBuilder stringBuilder = new StringBuilder(input);
			for (int i = input.Length - 1; i >= 0; i--) {
				if (char.IsPunctuation(input, i) && _randomiser.Next(100) > Threshold) {
					stringBuilder.Remove(i, 1);
				}
			}

			return stringBuilder.ToString();
		}
	}
}
