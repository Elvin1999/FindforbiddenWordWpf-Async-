using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindForbiddenWordWpf.AdditionalClasses
{
   public class Config
    {
        public List<ForbiddenWord> ForbiddenWords { get; set; }
        public string FileName { get; set; }
        public void SeriailizeFilialsToJson()
        {
            using (StreamWriter sw = new StreamWriter("words.json"))
            {
                var item = JsonConvert.SerializeObject(ForbiddenWords);
                sw.WriteLine(item);
            }
        }
        public List<ForbiddenWord> DeserializeFilialsFromJson()
        {
            try
            {
                var context = File.ReadAllText("words.json");
                ForbiddenWords = JsonConvert.DeserializeObject<List<ForbiddenWord>>(context);
            }
            catch (Exception)
            {
            }

            return ForbiddenWords;
        }
    }
}
