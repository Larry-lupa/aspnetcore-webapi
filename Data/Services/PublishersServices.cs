using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Models;
using my_books.Data.ViewModels;
using my_books.Exceptions;

namespace my_books.Data.Services
{
    public class PublishersServices
    {
        private AppDbContext _context;

        public PublishersServices(AppDbContext context)
        {
            _context = context;
        }

        public Publisher AddPublisher(PublisherVM publisher)
        {
            if (StringStartsWithNumber(publisher.Name)) throw new PublisherNameException("Publisher name starts with number", publisher.Name);

            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }
        public List<Publisher> GetAllPublishers() => _context.Publishers.ToList();

        public Publisher GetPublisherById(int id) => _context.Publishers.FirstOrDefault(n => n.Id == id);

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publishers.Where(n => n.Id == publisherId)
                .Select(n => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = n.Name,
                    BookAuthors = n.Books.Select(n => new BookAuthorVM()
                    {
                        BookName = n.Title,
                        BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();

            return _publisherData;
        }

        public void DeletePublisherById(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);
            if (_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Publisher with ID: {id} does not exist.");
            }
        }

        public void DeleteAllPublishers()
        {
            var _book = this.GetAllPublishers();
            _context.RemoveRange(_book);
            _context.SaveChanges();
        }
        private bool StringStartsWithNumber(string name) => Regex.IsMatch(name, @"^\d");
    }
}
