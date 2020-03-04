//using BookStore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// note that we changed the models in BookStore.DAL to handle json
// but we prob. didn't need to do that since we're using these 
// ViewModels (hopefully then we don't ever have to mess with them again)

// these field formats do have to match those tho.

namespace BookStore.ViewModels
{
    public class BookViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [Required]
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "edition")]
        public string Edition { get; set; }

        [JsonProperty(PropertyName = "binding")]
        public string Binding { get; set; }

        [JsonProperty(PropertyName = "publisher")]
        public string Publisher { get; set; }
        
        [JsonProperty(PropertyName = "isbn")]
        public Nullable<long> Isbn { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "thumbnailImage")]
        public string ThumbnailImage { get; set; }

        [JsonProperty(PropertyName = "largeImage")]
        public string LargeImage { get; set; }

        [JsonProperty(PropertyName = "price")]
        public string Price { get; set; }

        [JsonProperty(PropertyName = "authors")]
        public virtual List<AuthorViewModel> Authors { get; set; }

    }

}

