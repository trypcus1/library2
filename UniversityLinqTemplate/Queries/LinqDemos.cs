using System;
using System.Collections.Generic;
using System.Text;
using UniversityLinqTemplate.Data;
using Microsoft.EntityFrameworkCore;

namespace UniversityLinqTemplate.Queries
{
    public static class LinqDemos
    {
        // 1. ФИЛЬТРАЦИЯ + СОРТИРОВКА (Where с &&/||, OrderByDescending + ThenBy)
        public static void DemoFilteringAndSorting(UniversityContext db)
        {
            Console.WriteLine("\n=== 1. Книги жанра 'Роман' или 'Антиутопия', изданные после 1900 г. ===");
            Console.WriteLine("    (сортировка: год публикации по убыванию, затем по названию)");

            var result = db.Books
                .Where(b => b.PublishYear > 1900 && (b.Genre == "Роман" || b.Genre == "Антиутопия"))
                .OrderByDescending(b => b.PublishYear)
                .ThenBy(b => b.Title)
                .ToList();

            foreach (var b in result)
                Console.WriteLine($"  {b.PublishYear} — '{b.Title}' ({b.Genre}), автор: {b.Author}");
        }

        // 2. ПРОЕКЦИЯ (Select в анонимный тип)
        public static void DemoProjection(UniversityContext db)
        {
            Console.WriteLine("\n=== 2. Проекция: читатель, книга и срок выдачи (в днях) ===");

            var result = db.Loans
                .Include(l => l.Reader)
                .Include(l => l.Book)
                .Select(l => new
                {
                    ReaderName = l.Reader!.FullName,
                    BookTitle = l.Book!.Title,
                    DaysHeld = (l.ReturnDate ?? DateTime.Now).Subtract(l.LoanDate).Days,
                    Returned = l.ReturnDate != null
                })
                .ToList();

            foreach (var item in result)
            {
                var status = item.Returned ? "возвращена" : "на руках";
                Console.WriteLine($"  {item.ReaderName} — '{item.BookTitle}' | держал(а) {item.DaysHeld} дн. ({status})");
            }
        }

        // 3. АГРЕГАЦИЯ (Count, Sum, Average, Min, Max)
        public static void DemoAggregations(UniversityContext db)
        {
            Console.WriteLine("\n=== 3. Агрегатные вычисления по фонду и выдачам ===");

            int totalBooks = db.Books.Count();
            int activeLoans = db.Loans.Count(l => l.ReturnDate == null);
            decimal totalFines = db.Loans.Sum(l => l.Fine);
            double avgLoanDays = db.Loans.Average(l => (l.ReturnDate ?? DateTime.Now).Subtract(l.LoanDate).Days);
            int oldestBook = db.Books.Min(b => b.PublishYear);
            int newestBook = db.Books.Max(b => b.PublishYear);

            Console.WriteLine($"  Всего книг в фонде:           {totalBooks}");
            Console.WriteLine($"  Выдано и не возвращено:       {activeLoans}");
            Console.WriteLine($"  Сумма всех штрафов:           {totalFines:F2} руб.");
            Console.WriteLine($"  Средний срок удержания книги: {avgLoanDays:F1} дн.");
            Console.WriteLine($"  Самая старая книга:           {oldestBook} г.");
            Console.WriteLine($"  Самая новая книга:            {newestBook} г.");
        }

        // 4. ГРУППИРОВКА (GroupBy с агрегатами)
        public static void DemoGrouping(UniversityContext db)
        {
            Console.WriteLine("\n=== 4. Группировка книг по жанру (Count + Average) ===");

            var summary = db.Books
                .GroupBy(b => b.Genre)
                .Select(g => new
                {
                    Genre = g.Key,
                    BookCount = g.Count(),
                    AvgYear = g.Average(b => b.PublishYear)
                })
                .OrderByDescending(g => g.BookCount)
                .ThenBy(g => g.Genre)
                .ToList();

            foreach (var item in summary)
                Console.WriteLine($"  Жанр '{item.Genre}': книг = {item.BookCount}, средний год = {item.AvgYear:F0}");
        }

        // 5. СВЯЗИ + ЖАДНАЯ ЗАГРУЗКА (Include + ThenInclude)
        public static void DemoJoinsAndIncludes(UniversityContext db)
        {
            Console.WriteLine("\n=== 5. Читатели и их взятые книги (Include / ThenInclude) ===");

            var readers = db.Readers
                .Include(r => r.Loans)
                    .ThenInclude(l => l.Book)
                        .ThenInclude(b => b!.Card)
                .ToList();

            foreach (var reader in readers)
            {
                Console.WriteLine($"\n  {reader.FullName} (тел. {reader.Phone}):");
                if (!reader.Loans.Any())
                {
                    Console.WriteLine("    — (нет выдач)");
                    continue;
                }

                foreach (var loan in reader.Loans)
                {
                    var book = loan.Book!;
                    var card = book.Card;
                    var shelf = card != null ? card.ShelfLocation : "не указано";
                    var status = loan.ReturnDate == null ? "на руках" : "возвращена";
                    Console.WriteLine($"    • '{book.Title}' [{status}, полка: {shelf}]");
                }
            }
        }
    }
}