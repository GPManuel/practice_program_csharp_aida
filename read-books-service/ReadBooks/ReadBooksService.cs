using System.Collections.Generic;

namespace ReadBooks;

public class ReadBooksService
{
    private readonly UserSession _userSession;
    private readonly BooksRepository _booksRepository;
    private readonly UsersRepository _usersRepository;

    public ReadBooksService(UserSession userSession, BooksRepository booksRepository, UsersRepository usersRepository)
    {
        _userSession = userSession;
        _booksRepository = booksRepository;
        _usersRepository = usersRepository;
    }

    public List<Book> GetBooksReadByUser(User user)
    {
        if (!_usersRepository.AreFriends(_userSession.GetLoggedUser(), user))
        {
            return new List<Book>();
        }

        return _booksRepository.GetBooksBy(user);
    }
}