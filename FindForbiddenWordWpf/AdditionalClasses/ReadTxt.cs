using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindForbiddenWordWpf.AdditionalClasses
{
   public class ReadTxt
    {
        public string FileName { get; set; }
        public List<ForbiddenWord> GetWords()
        {
            var words = File.ReadAllText(FileName).Split('\n');
            List<ForbiddenWord> forbiddenWords = new List<ForbiddenWord>();
            int i = 0;
            foreach (var item in words)
            {
                ForbiddenWord forbiddenWord = new ForbiddenWord()
                {
                    Word = item
                };
                forbiddenWords.Add(forbiddenWord);
            }
            return forbiddenWords;
        }
    }
}
