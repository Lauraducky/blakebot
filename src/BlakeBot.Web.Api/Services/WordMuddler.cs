using System;

namespace BlakeBot.Web.Api.Services {
    public class WordMuddler : IWordMuddler {
        private const int Threshold = 75;

        public string MuddleWord(string input) {
            var arr = input.ToCharArray();

            var rand = new Random();
            for (int i = 0; i < arr.Length - 1; i++) {
                var num = rand.Next(100);
                if (num > Threshold) {
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
