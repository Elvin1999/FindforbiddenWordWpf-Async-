﻿using Newtonsoft.Json;
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
        public Reports Reports { get; set; }
        public Config()
        {
            ForbiddenWords = new List<ForbiddenWord>();
            Reports = new Reports();
        }
        public void SeriailizeWordsToJson()
        {
            using (StreamWriter sw = new StreamWriter("words.json"))
            {
                var item = JsonConvert.SerializeObject(ForbiddenWords);
                sw.WriteLine(item);
            }
        }
        public List<ForbiddenWord> DeserializeWordsFromJson()
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
        public void SeriailizeReportsToJson()
        {
            using (StreamWriter sw = new StreamWriter("reports.json"))
            {
                var item = JsonConvert.SerializeObject(Reports);
                sw.WriteLine(item);
            }
        }
        public Reports DeserializeReportsFromJson()
        {
            try
            {
                var context = File.ReadAllText("reports.json");
                Reports = JsonConvert.DeserializeObject<Reports>(context);
            }
            catch (Exception)
            {
            }
            return Reports;
        }
    }
}
