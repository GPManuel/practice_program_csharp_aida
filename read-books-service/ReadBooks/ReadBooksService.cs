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
        var loggedUser = _userSession.GetLoggedUser();
        if (IsUserNotLogged(loggedUser))
        {
            throw new UserNotLoggedException();
        }
        if (AreNotFriends(user, loggedUser))
        {
            return new List<Book>();
        }

        return _booksRepository.GetBooksBy(user);
    }

    private bool IsUserNotLogged(User loggedUser)
    {
        return loggedUser == null;
    }

    private bool AreNotFriends(User user, User loggedUser)
    {
        return !_usersRepository.AreFriends(loggedUser, user);
    }
}