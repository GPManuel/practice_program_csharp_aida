using System.Collections.Generic;

namespace ReadBooks;

public class ReadBooksService
{
    public ReadBooksService(UserSession userSession, BooksRepository booksRepository, UsersRepository usersRepository)
    {
    }

    public List<Book> GetBooksReadByUser(User user)
    {
        return new List<Book>();
    }
}