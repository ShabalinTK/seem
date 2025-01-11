using System.ComponentModel.DataAnnotations.Schema;

namespace MyHttpServer.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleEng { get; set; }
        public string ReleaseYear { get; set; }
        public string Country { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Player { get; set; }
        public string ImagePath { get; set; }
        public string Actors { get; set; }
        public Director Director { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
    }

}
