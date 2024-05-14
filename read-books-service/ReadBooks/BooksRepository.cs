using System.Collections.Generic;

namespace ReadBooks;

public interface BooksRepository
{
    List<Book> GetBooksBy(User friend);
}