using System;

namespace BlakeBot.Web.Api.Services {
    public class WordMuddler : IWordMuddler {
        private const int Threshold = 75;

		private readonly IRandomiser _randomiser;

		public WordMuddler(IRandomiser randomiser) {
			_randomiser = randomiser;
		}

        public string MuddleWord(string input) {
            var arr = input.ToCharArray();

            for (int i = 0; i < arr.Length - 1; i++) {
                if (!char.IsPunctuation(arr[i]) && !char.IsPunctuation(arr[i + 1])
						&& _randomiser.Next(100) > Threshold) {
                    // swap letters
                    char a = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = a;
                }
            }

            return new string(arr);
        }
    }
}
