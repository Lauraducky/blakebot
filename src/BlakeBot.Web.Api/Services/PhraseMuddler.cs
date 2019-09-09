using System;
using System.Linq;

namespace BlakeBot.Web.Api.Services {
    public class PhraseMuddler : IPhraseMuddler {
        private readonly IWordMuddler _wordMuddler;
		private readonly ICapitalisationRemover _capitalisationRemover;
		private readonly IPunctuationRemover _punctuationRemover;

        public PhraseMuddler(IWordMuddler wordMuddler,
			ICapitalisationRemover capitalisationRemover,
			IPunctuationRemover punctuationRemover)
		{
            _wordMuddler = wordMuddler;
			_capitalisationRemover = capitalisationRemover;
			_punctuationRemover = punctuationRemover;
        }

        public string MuddlePhrase(string input) {
            string[] words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var muddledWords = words.Select(x => {
				var muddled = _wordMuddler.MuddleWord(x);
				var lowercased = _capitalisationRemover.RemoveCapitalisation(muddled);
				return lowercased;
			});

            var full = string.Join(' ', muddledWords);
			var punctuationRemoved = _punctuationRemover.RemovePunctuation(full);
			return punctuationRemoved;
        }
    }
}
