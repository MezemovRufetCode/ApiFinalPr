using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMVC.DTOs.BookDto
{
    public class BookListDto
    {
        public List<BookListItemDto> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
