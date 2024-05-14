using NSubstitute;
using NUnit.Framework;

namespace ReadBooks.Tests
{
    public class ReadBooksServiceTest
    {
        [Test]
        public void no_return_books_when_user_is_not_friend_of_user_logged()
        {
            var _userSession = Substitute.For<UserSession>();
            var userLogged = new User(2);
            User noFriend = new User(1);
            _userSession.GetLoggedUser().Returns(userLogged);
            var _usersRepository = Substitute.For<UsersRepository>();
            _usersRepository.AreFriends(userLogged, noFriend).Returns(false);
            var _booksRepository = Substitute.For<BooksRepository>();
            var readBooksService = new ReadBooksService(_userSession, _booksRepository, _usersRepository);

            List<Book> friendBooks = readBooksService.GetBooksReadByUser(noFriend);

            var emptyBooksList = new List<Book>();
            Assert.That(friendBooks, Is.EquivalentTo(emptyBooksList));
        }
    }
}