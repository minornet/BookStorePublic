using Newtonsoft.Json;

namespace BookStore.ViewModels
{
    public class BookAuthorViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "bookId")]
        public int BookId { get; set; }

        [JsonProperty(PropertyName = "authorId")]
        public int AuthorId { get; set; }
    }
}

