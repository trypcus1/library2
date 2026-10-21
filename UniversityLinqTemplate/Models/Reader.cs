using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityLinqTemplate.Models
{
    /// <summary>
    /// читатель библиотеки. Один читатель может взять много книг (связь 1:N через Loan).
    /// </summary>
    public class Reader
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        // связь с выдачами книг
        public List<Loan> Loans { get; set; } = new();
    }
}
