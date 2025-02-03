using System;
using System.Collections.Generic;

namespace TestPRN231.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int AuthorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
