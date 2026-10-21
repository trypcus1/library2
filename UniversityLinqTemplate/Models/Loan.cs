using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityLinqTemplate.Models
{
    /// <summary>
    /// запись о выдаче книги читателю. Промежуточная сущность между Reader и Book.
    /// хранит даты выдачи/возврата и штраф за просрочку.
    /// </summary>
    public class Loan
    {
        public int Id { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }   // null => книга ещё не возвращена
        public decimal Fine { get; set; }            // штраф за просрочку

        public int ReaderId { get; set; }
        public Reader? Reader { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}