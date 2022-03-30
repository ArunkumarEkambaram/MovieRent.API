using System;

namespace MovieRent.API.Data.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string Genre { get; set; }
        public byte Rating { get; set; }
        public string CoverUrl { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
