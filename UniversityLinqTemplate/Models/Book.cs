using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityLinqTemplate.Models
{
    /// <summary>
    /// книга библиотеки. Связана с карточкой (1:1) и с выдачами (1:N).
    /// </summary>
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int PublishYear { get; set; }
        public string Genre { get; set; } = string.Empty;

        // связь один-к-одному с библиотечной карточкой
        public BookCard? Card { get; set; }

        // связь с выдачами (одна книга может выдаваться много раз)
        public List<Loan> Loans { get; set; } = new();
    }
}
