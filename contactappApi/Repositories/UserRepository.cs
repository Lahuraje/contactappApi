using contactappApi.Models;
using Newtonsoft.Json;

namespace contactappApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _filePath = "data.json";
        


        public List<User> GetAllUsers()
        {
            if (!File.Exists(_filePath))
            {
                return new List<User>();
            }
            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<User>>(jsonData);
        }

        public User GetUserById(int id)
        {
            var users = GetAllUsers();
            return users.FirstOrDefault(u => u.Id == id);
        }

        public void AddUser(User user)
        {
            var users = GetAllUsers();
            user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
            users.Add(user);
            SaveUsers(users);
        }

        public void UpdateUser(User user)
        {
            var users = GetAllUsers();
            var existingUser = users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                SaveUsers(users);
            }
        }

        public void DeleteUser(int id)
        {
            var users = GetAllUsers();
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                users.Remove(user);
                SaveUsers(users);
            }
        }

        private void SaveUsers(List<User> users)
        {
            var jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }
    }
}
