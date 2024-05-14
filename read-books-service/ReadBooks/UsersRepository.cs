namespace ReadBooks;

public interface UsersRepository
{
    bool AreFriends(User user, User otherUser);
}