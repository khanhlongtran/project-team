using System;
using System.Collections.Generic;

namespace TestPRN231.Models
{
    public partial class Library
    {
        public Library()
        {
            Books = new HashSet<Book>();
        }

        public int LibraryId { get; set; }
        public string? LibraryName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
