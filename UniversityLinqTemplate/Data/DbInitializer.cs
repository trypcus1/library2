using System;
using System.Collections.Generic;
using System.Text;
using UniversityLinqTemplate.Models;

namespace UniversityLinqTemplate.Data
{
    public static class DbInitializer
    {
        public static void Initialize(UniversityContext context)
        {
            // пересоздаём БД с нуля при каждом запуске (защита от рассинхрона моделей)
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // 1. читатели
            var reader1 = new Reader { FullName = "Смирнов Алексей Петрович", Phone = "+7 (900) 111-22-33", Email = "smirnov@mail.ru", RegistrationDate = DateTime.Now.AddMonths(-6) };
            var reader2 = new Reader { FullName = "Иванова Мария Сергеевна", Phone = "+7 (911) 222-33-44", Email = "ivanova@mail.ru", RegistrationDate = DateTime.Now.AddMonths(-3) };
            var reader3 = new Reader { FullName = "Кузнецов Дмитрий Олегович", Phone = "+7 (922) 333-44-55", Email = "kuznetsov@mail.ru", RegistrationDate = DateTime.Now.AddMonths(-1) };
            context.Readers.AddRange(reader1, reader2, reader3);

            // 2. книги (у каждой есть карточка и жанр)
            var book1 = new Book { Title = "Война и мир", Author = "Лев Толстой", PublishYear = 1869, Genre = "Роман-эпопея" };
            var book2 = new Book { Title = "Анна Каренина", Author = "Лев Толстой", PublishYear = 1877, Genre = "Роман" };
            var book3 = new Book { Title = "1984", Author = "Джордж Оруэлл", PublishYear = 1949, Genre = "Антиутопия" };
            var book4 = new Book { Title = "Три товарища", Author = "Эрих М. Ремарк", PublishYear = 1936, Genre = "Роман" };
            var book5 = new Book { Title = "Мастер и Маргарита", Author = "Михаил Булгаков", PublishYear = 1967, Genre = "Мистический роман" };
            context.Books.AddRange(book1, book2, book3, book4, book5);

            // 3. карточки книг (связь 1:1)
            context.BookCards.AddRange(
                new BookCard { Book = book1, InventoryNumber = "БИБ-001", ShelfLocation = "Зал А, стеллаж 3", ArrivalDate = DateTime.Now.AddYears(-5) },
                new BookCard { Book = book2, InventoryNumber = "БИБ-002", ShelfLocation = "Зал А, стеллаж 3", ArrivalDate = DateTime.Now.AddYears(-5) },
                new BookCard { Book = book3, InventoryNumber = "БИБ-003", ShelfLocation = "Зал Б, стеллаж 1", ArrivalDate = DateTime.Now.AddYears(-3) },
                new BookCard { Book = book4, InventoryNumber = "БИБ-004", ShelfLocation = "Зал Б, стеллаж 2", ArrivalDate = DateTime.Now.AddYears(-2) },
                new BookCard { Book = book5, InventoryNumber = "БИБ-005", ShelfLocation = "Зал В, стеллаж 4", ArrivalDate = DateTime.Now.AddYears(-1) }
            );

            // 4. записи о выдачах (часть уже возвращена, часть — ещё на руках)
            context.Loans.AddRange(
                new Loan { Reader = reader1, Book = book1, LoanDate = DateTime.Now.AddDays(-30), ReturnDate = DateTime.Now.AddDays(-20), Fine = 0m },
                new Loan { Reader = reader1, Book = book3, LoanDate = DateTime.Now.AddDays(-15), ReturnDate = DateTime.Now.AddDays(-5), Fine = 50m },
                new Loan { Reader = reader2, Book = book1, LoanDate = DateTime.Now.AddDays(-25), ReturnDate = DateTime.Now.AddDays(-12), Fine = 0m },
                new Loan { Reader = reader2, Book = book5, LoanDate = DateTime.Now.AddDays(-10), ReturnDate = null, Fine = 0m },
                new Loan { Reader = reader3, Book = book2, LoanDate = DateTime.Now.AddDays(-7), ReturnDate = null, Fine = 0m },
                new Loan { Reader = reader3, Book = book4, LoanDate = DateTime.Now.AddDays(-40), ReturnDate = DateTime.Now.AddDays(-20), Fine = 150m }
            );

            context.SaveChanges();
        }
    }
}