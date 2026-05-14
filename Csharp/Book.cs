using System;

namespace BookCatalog
{
    /// <summary>
    /// Класс, представляющий книгу в каталоге.
    /// </summary>
    [Serializable]
    public class Book
    {
        private int id;
        private string title;
        private string author;
        private int year;
        private double price;
        private int pages;
        private string genre;

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public Book()
        {
            id = 0;
            title = "Неизвестно";
            author = "Неизвестен";
            year = 2000;
            price = 0.0;
            pages = 0;
            genre = "Не указан";
        }

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        public Book(int id, string title, string author, int year, double price, int pages, string genre)
        {
            this.id = id;
            this.title = title;
            this.author = author;
            this.year = year;
            this.price = price;
            this.pages = pages;
            this.genre = genre;
        }

        /// <summary>
        /// Свойство Id.
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        /// <summary>
        /// Свойство Title.
        /// </summary>
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        /// <summary>
        /// Свойство Author.
        /// </summary>
        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
            }
        }

        /// <summary>
        /// Свойство Year.
        /// </summary>
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
            }
        }

        /// <summary>
        /// Свойство Price.
        /// </summary>
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }

        /// <summary>
        /// Свойство Pages.
        /// </summary>
        public int Pages
        {
            get
            {
                return pages;
            }
            set
            {
                pages = value;
            }
        }

        /// <summary>
        /// Свойство Genre.
        /// </summary>
        public string Genre
        {
            get
            {
                return genre;
            }
            set
            {
                genre = value;
            }
        }

        /// <summary>
        /// Перегруженный метод ToString().
        /// </summary>
        public override string ToString()
        {
            string result = "ID: " + id + " | \"" + title + "\" | " + author + " | " + year + " г. | " + price.ToString("F2") + " руб. | " + pages + " стр. | " + genre;
            return result;
        }
    }
}