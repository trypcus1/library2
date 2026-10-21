using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityLinqTemplate.Models
{
    /// <summary>
    /// библиотечная карточка книги. Связь один-к-одному с Book.
    /// </summary>
    public class BookCard
    {
        public int Id { get; set; }
        public string InventoryNumber { get; set; } = string.Empty; // напр. "БИБ-001"
        public DateTime ArrivalDate { get; set; } = DateTime.Now;
        public string ShelfLocation { get; set; } = string.Empty;

        // внешний ключ и навигация к книге (1:1)
        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}