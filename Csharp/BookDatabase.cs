using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookCatalog
{
    /// <summary>
    /// Вспомогательный класс для работы с базой данных книг.
    /// </summary>
    public static class BookDatabase
    {
        /// <summary>
        /// Читает базу данных из бинарного файла.
        /// </summary>
        public static List<Book> LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл базы данных не найден. Создана пустая база.");
                return new List<Book>();
            }

            try
            {
                List<Book> books = new List<Book>();

                using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
                {
                    int count = reader.ReadInt32();

                    for (int i = 0; i < count; i++)
                    {
                        int id = reader.ReadInt32();
                        string title = reader.ReadString();
                        string author = reader.ReadString();
                        int year = reader.ReadInt32();
                        double price = reader.ReadDouble();
                        int pages = reader.ReadInt32();
                        string genre = reader.ReadString();

                        Book book = new Book(id, title, author, year, price, pages, genre);
                        books.Add(book);
                    }
                }

                return books;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при чтении файла: " + ex.Message);
                return new List<Book>();
            }
        }

        /// <summary>
        /// Сохраняет базу данных в бинарный файл.
        /// </summary>
        public static void SaveToFile(string filePath, List<Book> books)
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
                {
                    writer.Write(books.Count);

                    foreach (Book book in books)
                    {
                        writer.Write(book.Id);
                        writer.Write(book.Title);
                        writer.Write(book.Author);
                        writer.Write(book.Year);
                        writer.Write(book.Price);
                        writer.Write(book.Pages);
                        writer.Write(book.Genre);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при сохранении файла: " + ex.Message);
            }
        }

        /// <summary>
        /// Выводит все книги на экран.
        /// </summary>
        public static void PrintAll(List<Book> books)
        {
            if (books.Count == 0)
            {
                Console.WriteLine("База данных пуста.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("=== КАТАЛОГ КНИГ ===");
            Console.WriteLine(new string('-', 80));

            foreach (Book book in books)
            {
                Console.WriteLine(book.ToString());
            }

            Console.WriteLine(new string('-', 80));
            Console.WriteLine("Всего книг: " + books.Count);
        }

        /// <summary>
        /// Добавляет новую книгу в базу.
        /// </summary>
        public static void AddBook(List<Book> books, Book newBook)
        {
            var exists = books.Where(b => b.Id == newBook.Id);

            if (exists.Count() > 0)
            {
                Console.WriteLine("Ошибка: книга с ID " + newBook.Id + " уже существует!");
                return;
            }

            books.Add(newBook);
            Console.WriteLine("Книга успешно добавлена.");
        }

        /// <summary>
        /// Удаляет книгу по ID.
        /// </summary>
        public static bool RemoveBook(List<Book> books, int id)
        {
            Book bookToRemove = books.FirstOrDefault(b => b.Id == id);

            if (bookToRemove == null)
            {
                Console.WriteLine("Книга с ID " + id + " не найдена.");
                return false;
            }

            books.Remove(bookToRemove);
            Console.WriteLine("Книга \"" + bookToRemove.Title + "\" удалена.");
            return true;
        }

        /// <summary>
        /// Запрос 1 (перечень): Все книги заданного жанра.
        /// </summary>
        public static List<Book> GetBooksByGenre(List<Book> books, string genre)
        {
            var result = books.Where(b => b.Genre.ToLower() == genre.ToLower());
            return result.ToList();
        }

        /// <summary>
        /// Запрос 2 (перечень): Книги дороже заданной цены, отсортированные по убыванию цены.
        /// </summary>
        public static List<Book> GetExpensiveBooks(List<Book> books, double minPrice)
        {
            var result = books.Where(b => b.Price > minPrice)
                              .OrderByDescending(b => b.Price);
            return result.ToList();
        }

        /// <summary>
        /// Запрос 3 (одно значение): Средняя цена всех книг.
        /// </summary>
        public static double GetAveragePrice(List<Book> books)
        {
            if (books.Count == 0)
            {
                return 0.0;
            }

            return books.Average(b => b.Price);
        }

        /// <summary>
        /// Запрос 4 (одно значение): Общее количество страниц во всех книгах.
        /// </summary>
        public static int GetTotalPages(List<Book> books)
        {
            if (books.Count == 0)
            {
                return 0;
            }

            return books.Sum(b => b.Pages);
        }
    }
}