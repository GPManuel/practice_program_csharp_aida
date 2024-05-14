using NSubstitute;
using NUnit.Framework;

namespace ReadBooks.Tests
{
    public class ReadBooksServiceTest
    {
        private UserSession _userSession;
        private UsersRepository _usersRepository;
        private BooksRepository _booksRepository;
        private ReadBooksService _readBooksService;

        [SetUp]
        public void SetUp()
        {
            _userSession = Substitute.For<UserSession>();
            _usersRepository = Substitute.For<UsersRepository>();
            _booksRepository = Substitute.For<BooksRepository>();
            _readBooksService = new ReadBooksService(_userSession, _booksRepository, _usersRepository);
        }

        [Test]
        public void no_return_books_when_user_is_not_friend_of_user_logged()
        {
            var userLogged = new User(2);
            User noFriend = new User(1);
            _userSession.GetLoggedUser().Returns(userLogged);
            _usersRepository.AreFriends(userLogged, noFriend).Returns(false);

            List<Book> friendBooks = _readBooksService.GetBooksReadByUser(noFriend);

            var emptyBooksList = new List<Book>();
            Assert.That(friendBooks, Is.EquivalentTo(emptyBooksList));
        }

        [Test]
        public void return_friend_books_when_user_is_friend_of_user_logged()
        {
            var userLogged = new User(2);
            User friend = new User(1);
            _userSession.GetLoggedUser().Returns(userLogged);
            _usersRepository.AreFriends(userLogged, friend).Returns(true);
            var books = new List<Book>() { new Book("Clean Code") };
            _booksRepository.GetBooksBy(friend).Returns(books);

            List<Book> friendBooks = _readBooksService.GetBooksReadByUser(friend);

            var expectedBooks = new List<Book>() { new Book("Clean Code") };
            Assert.That(friendBooks, Is.EquivalentTo(expectedBooks));
        }

    }
}