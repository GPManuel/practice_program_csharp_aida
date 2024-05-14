using System.Collections.Generic;
using System.Linq;

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
        if (AreNotFriends(user))
        {
            return new List<Book>();
        }

        return _booksRepository.GetBooksBy(user);
    }

    private bool AreNotFriends(User user)
    {
        return !_usersRepository.AreFriends(_userSession.GetLoggedUser(), user);
    }
}