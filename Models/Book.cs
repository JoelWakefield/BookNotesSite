using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BookNotesSite.Models
{
    public class Book
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "isbn")]
        public string Isbn { get; set; }

        [JsonProperty(PropertyName = "imageSrc")]
        public string ImageSrc { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }
        [JsonProperty(PropertyName = "publishDate")]
        public DateTime PublishDate { get; set; }
        [JsonProperty(PropertyName = "rating")]
        public int Rating { get; set; }
        [JsonProperty(PropertyName = "affiliateLink")]
        public string AffiliateLink { get; set; }
        [JsonProperty(PropertyName = "nonAffiliateLink")]
        public string NonAffiliateLink { get; set; }

        [JsonProperty(PropertyName = "sections")]
        public List<Note> Sections { get; set; }


        public void CreateId()
        {
            string title = Title.Trim().ToLower();

            //  Remove all symbols
            string pattern = "[^a-z]";
            Regex remove = new Regex(pattern);
            title = remove.Replace(title, "");

            //  Replace white space with dashes
            pattern = "\\s+";
            Regex replace = new Regex(pattern);
            Id = replace.Replace(title, "-");
        }
    }
}
