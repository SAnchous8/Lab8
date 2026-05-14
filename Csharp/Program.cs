using System;
using System.Collections.Generic;

namespace BookCatalog
{
    /// <summary>
    /// Основной класс программы.
    /// </summary>
    internal class Program
    {
        private static List<Book> books = new List<Book>();
        private static string filePath = "";

        /// <summary>
        /// Безопасный ввод целого числа.
        /// </summary>
        private static int ReadInt(string message)
        {
            int result = 0;
            string input = "";

            while (true)
            {
                Console.Write(message);
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Ошибка: ввод не может быть пустым!");
                    continue;
                }

                if (int.TryParse(input, out result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Ошибка: введите целое число!");
                }
            }
        }

        /// <summary>
        /// Безопасный ввод дробного числа.
        /// </summary>
        private static double ReadDouble(string message)
        {
            double result = 0.0;
            string input = "";

            while (true)
            {
                Console.Write(message);
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Ошибка: ввод не может быть пустым!");
                    continue;
                }

                input = input.Replace('.', ',');

                if (double.TryParse(input, out result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Ошибка: введите число!");
                }
            }
        }

        /// <summary>
        /// Безопасный ввод строки.
        /// </summary>
        private static string ReadString(string message)
        {
            string input = "";

            while (true)
            {
                Console.Write(message);
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Ошибка: ввод не может быть пустым!");
                    continue;
                }

                return input;
            }
        }

        /// <summary>
        /// Вывод перечня книг.
        /// </summary>
        private static void PrintBookList(List<Book> bookList)
        {
            if (bookList.Count == 0)
            {
                Console.WriteLine("Ничего не найдено.");
                return;
            }

            Console.WriteLine(new string('-', 80));

            foreach (Book book in bookList)
            {
                Console.WriteLine(book.ToString());
            }

            Console.WriteLine(new string('-', 80));
            Console.WriteLine("Найдено книг: " + bookList.Count);
        }

        /// <summary>
        /// Ручное заполнение базы данных с клавиатуры.
        /// </summary>
        private static void ManualFillDatabase()
        {
            int count = 0;

            while (true)
            {
                count = ReadInt("Сколько книг хотите добавить: ");

                if (count > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка: количество должно быть больше нуля!");
                }
            }

            Console.WriteLine();

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("--- Книга " + (i + 1) + " ---");

                int id = 0;

                while (true)
                {
                    id = ReadInt("Введите ID: ");

                    bool idExists = false;

                    foreach (Book b in books)
                    {
                        if (b.Id == id)
                        {
                            idExists = true;
                            break;
                        }
                    }

                    if (idExists)
                    {
                        Console.WriteLine("Ошибка: книга с таким ID уже существует!");
                    }
                    else
                    {
                        break;
                    }
                }

                string title = ReadString("Название: ");
                string author = ReadString("Автор: ");
                int year = ReadInt("Год издания: ");
                double price = ReadDouble("Цена (руб.): ");
                int pages = ReadInt("Количество страниц: ");
                string genre = ReadString("Жанр: ");

                Book newBook = new Book(id, title, author, year, price, pages, genre);
                books.Add(newBook);
                Console.WriteLine("Книга добавлена.\n");
            }

            BookDatabase.SaveToFile(filePath, books);
            Console.WriteLine("Все книги сохранены в файл.");
        }

        /// <summary>
        /// Показывает главное меню.
        /// </summary>
        private static void ShowMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║        КАТАЛОГ КНИГ — МЕНЮ          ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║  1. Просмотр всех книг              ║");
            Console.WriteLine("║  2. Добавить книгу                  ║");
            Console.WriteLine("║  3. Удалить книгу (по ID)           ║");
            Console.WriteLine("║  4. Запросы                         ║");
            Console.WriteLine("║  5. Сохранить базу в файл           ║");
            Console.WriteLine("║  6. Загрузить базу из файла         ║");
            Console.WriteLine("║  7. Заполнить базу вручную          ║");
            Console.WriteLine("║  0. Выход                           ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
        }

        /// <summary>
        /// Показывает меню запросов.
        /// </summary>
        private static void ShowQueryMenu()
        {
            Console.WriteLine();
            Console.WriteLine("--- МЕНЮ ЗАПРОСОВ ---");
            Console.WriteLine("  1. Книги заданного жанра (перечень)");
            Console.WriteLine("  2. Книги дороже заданной цены (перечень)");
            Console.WriteLine("  3. Средняя цена всех книг (одно значение)");
            Console.WriteLine("  4. Общее количество страниц (одно значение)");
            Console.WriteLine("  0. Назад");
        }

        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        private static void Main()
        {
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║   ПРИЛОЖЕНИЕ «КАТАЛОГ КНИГ»        ║");
            Console.WriteLine("╚══════════════════════════════════════╝");

            string programPath = AppDomain.CurrentDomain.BaseDirectory;
            filePath = programPath + "books.dat";

            Console.WriteLine();
            Console.WriteLine("Файл базы данных: " + filePath);

            books = BookDatabase.LoadFromFile(filePath);

            if (books.Count == 0)
            {
                Console.WriteLine("База данных пуста.");
                Console.WriteLine();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("  1. Заполнить базу вручную");
                Console.WriteLine("  2. Продолжить с пустой базой");

                int firstChoice = ReadInt("Ваш выбор: ");

                if (firstChoice == 1)
                {
                    ManualFillDatabase();
                }
            }
            else
            {
                Console.WriteLine("Загружено книг: " + books.Count);
            }

            bool running = true;

            while (running)
            {
                ShowMainMenu();
                int choice = ReadInt("\nВыберите действие: ");

                switch (choice)
                {
                    case 1:
                        BookDatabase.PrintAll(books);
                        break;

                    case 2:
                        {
                            Console.WriteLine();
                            Console.WriteLine("--- ДОБАВЛЕНИЕ НОВОЙ КНИГИ ---");

                            int id = 0;

                            while (true)
                            {
                                id = ReadInt("Введите ID: ");

                                bool idExists = false;

                                foreach (Book b in books)
                                {
                                    if (b.Id == id)
                                    {
                                        idExists = true;
                                        break;
                                    }
                                }

                                if (idExists)
                                {
                                    Console.WriteLine("Ошибка: книга с таким ID уже существует!");
                                }
                                else
                                {
                                    break;
                                }
                            }

                            string title = ReadString("Название: ");
                            string author = ReadString("Автор: ");
                            int year = ReadInt("Год издания: ");
                            double price = ReadDouble("Цена (руб.): ");
                            int pages = ReadInt("Количество страниц: ");
                            string genre = ReadString("Жанр: ");

                            Book newBook = new Book(id, title, author, year, price, pages, genre);
                            BookDatabase.AddBook(books, newBook);
                            break;
                        }

                    case 3:
                        {
                            Console.WriteLine();
                            Console.WriteLine("--- УДАЛЕНИЕ КНИГИ ---");
                            int removeId = ReadInt("Введите ID книги для удаления: ");
                            BookDatabase.RemoveBook(books, removeId);
                            break;
                        }

                    case 4:
                        {
                            bool queryMenu = true;

                            while (queryMenu)
                            {
                                ShowQueryMenu();
                                int queryChoice = ReadInt("\nВыберите запрос: ");

                                switch (queryChoice)
                                {
                                    case 1:
                                        {
                                            string genreQuery = ReadString("Введите жанр: ");
                                            List<Book> booksByGenre = BookDatabase.GetBooksByGenre(books, genreQuery);
                                            Console.WriteLine();
                                            Console.WriteLine("=== КНИГИ ЖАНРА «" + genreQuery.ToUpper() + "» ===");
                                            PrintBookList(booksByGenre);
                                            break;
                                        }

                                    case 2:
                                        {
                                            double minPrice = ReadDouble("Введите минимальную цену: ");
                                            List<Book> expensiveBooks = BookDatabase.GetExpensiveBooks(books, minPrice);
                                            Console.WriteLine();
                                            Console.WriteLine("=== КНИГИ ДОРОЖЕ " + minPrice.ToString("F2") + " РУБ. ===");
                                            PrintBookList(expensiveBooks);
                                            break;
                                        }

                                    case 3:
                                        {
                                            double avgPrice = BookDatabase.GetAveragePrice(books);
                                            Console.WriteLine();
                                            Console.WriteLine("Средняя цена всех книг: " + avgPrice.ToString("F2") + " руб.");
                                            break;
                                        }

                                    case 4:
                                        {
                                            int totalPages = BookDatabase.GetTotalPages(books);
                                            Console.WriteLine();
                                            Console.WriteLine("Общее количество страниц во всех книгах: " + totalPages);
                                            break;
                                        }

                                    case 0:
                                        queryMenu = false;
                                        break;

                                    default:
                                        Console.WriteLine("Неверный выбор.");
                                        break;
                                }
                            }

                            break;
                        }

                    case 5:
                        BookDatabase.SaveToFile(filePath, books);
                        Console.WriteLine("База данных сохранена.");
                        break;

                    case 6:
                        books = BookDatabase.LoadFromFile(filePath);
                        Console.WriteLine("Загружено книг: " + books.Count);
                        break;

                    case 7:
                        books.Clear();
                        ManualFillDatabase();
                        break;

                    case 0:
                        {
                            Console.WriteLine();
                            Console.Write("Сохранить изменения перед выходом? (да/нет): ");
                            string answer = Console.ReadLine();

                            if (answer != null && answer.ToLower() == "да")
                            {
                                BookDatabase.SaveToFile(filePath, books);
                                Console.WriteLine("Изменения сохранены.");
                            }

                            running = false;
                            Console.WriteLine("До свидания!");
                            break;
                        }

                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
    }
}