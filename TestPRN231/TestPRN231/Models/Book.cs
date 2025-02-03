using System;
using System.Collections.Generic;

namespace TestPRN231.Models
{
    public partial class Book
    {
        public Book()
        {
            Authors = new HashSet<Author>();
            Libraries = new HashSet<Library>();
        }

        public int BookId { get; set; }
        public string? TitleBook { get; set; }
        public DateTime? PublishDate { get; set; }
        public int? PublisherId { get; set; }
        public int? GenreId { get; set; }

        public virtual Genre? Genre { get; set; }
        public virtual Publisher? Publisher { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Library> Libraries { get; set; }
    }
}
