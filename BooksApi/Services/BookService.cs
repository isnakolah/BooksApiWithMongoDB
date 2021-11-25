using BooksApi.Models;

using MongoDB.Driver;

namespace BooksApi.Services;

public class BookService
{
    private readonly IMongoCollection<Book> _books;

    public BookService(IBookstoreDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        _books = database.GetCollection<Book>(settings.BooksCollectionName);
    }

    public List<Book> Get()
    {
        return _books.Find(book => true).ToList();
    }

    public Book Get(string id)
    {
        return _books.Find(book => book.ID == id).FirstOrDefault();
    }

    public Book Create(Book book)
    {
        _books.InsertOne(book);
        return book;
    }

    public void Update(string id, Book bookIn)
    {
        _books.ReplaceOne(book => book.ID == id, bookIn);
    }

    public void Remove(Book bookIn)
    {
        _books.DeleteOne(book => book.ID == bookIn.ID);
    }

    public void Remove(string id)
    {
        _books.DeleteOne(book => book.ID == id);
    }
}
