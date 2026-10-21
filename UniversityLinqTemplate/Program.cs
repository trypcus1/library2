using UniversityLinqTemplate.Data;
using UniversityLinqTemplate.Queries;

Console.Title = "LINQ Template — Библиотека";
Console.OutputEncoding = System.Text.Encoding.UTF8;

using (var db = new UniversityContext())
{
    Console.WriteLine("Инициализация базы данных SQLite...");
    DbInitializer.Initialize(db);
    Console.WriteLine("База данных готова к работе!");

    LinqDemos.DemoFilteringAndSorting(db);
    LinqDemos.DemoProjection(db);
    LinqDemos.DemoAggregations(db);
    LinqDemos.DemoGrouping(db);
    LinqDemos.DemoJoinsAndIncludes(db);
}

Console.WriteLine("\n\nНажмите любую клавишу для завершения...");
Console.ReadKey();