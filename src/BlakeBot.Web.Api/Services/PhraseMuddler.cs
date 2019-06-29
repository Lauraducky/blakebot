using System;
using System.Linq;

namespace BlakeBot.Web.Api.Services {
    public class PhraseMuddler : IPhraseMuddler {
        private readonly IWordMuddler _wordMuddler;

        public PhraseMuddler(IWordMuddler wordMuddler) {
            _wordMuddler = wordMuddler;
        }

        public string MuddlePhrase(string input) {
            string[] words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var muddled = words.Select(x => _wordMuddler.MuddleWord(x));

            return string.Join(' ', muddled);
        }
    }
}