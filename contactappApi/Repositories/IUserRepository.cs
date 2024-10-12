using contactappApi.Models;

namespace contactappApi.Repositories
{
    public interface IUserRepository
    {
        public List<User> GetAllUsers();
        public User GetUserById(int id);
        public void AddUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(int id);


    }
}
