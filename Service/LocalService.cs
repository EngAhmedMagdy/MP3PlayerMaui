using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Maui.Controls;
using MauiApp3.Models;

namespace MauiApp3.Service
{
    public class LocalService
    {
        private readonly string _dataFile;

        public LocalService()
        {
            // Set the data file path
            _dataFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "users.json");
        }

        public bool Register(string username, string password)
        {
            // Check if user already exists
            var existingUser = GetUserByUsername(username);
            if (existingUser != null)
            {
                return false;
            }

            // Hash the password
            var passwordHash = ComputeHash(password);
            // Create a new user object
            var newUser = new User
            {
                Username = username,
                PasswordHash = passwordHash
            };
            // Save the user to the data store
            var users = LoadUsers();
            users.Add(newUser);
            SaveUsers(users);

            return true;
        }

        public bool Login(string username, string password)
        {
            // Get the user by username
            var user = GetUserByUsername(username);
            if (user == null)
            {
                return false;
            }

            // Verify the password
            var passwordHash = ComputeHash(password);
            return user.PasswordHash == passwordHash;
        }

        private string ComputeHash(string value)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(value);
            var hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }

        private User GetUserByUsername(string username)
        {
            var users = LoadUsers();
            return users.Find(u => u.Username == username);
        }

        private List<User> LoadUsers()
        {
            if (File.Exists(_dataFile))
            {
                var json = File.ReadAllText(_dataFile);
                return JsonSerializer.Deserialize<List<User>>(json);
            }
            return new List<User>();
        }

        private void SaveUsers(List<User> users)
        {
            var json = JsonSerializer.Serialize(users);
            File.WriteAllText(_dataFile, json);
        }
    }


}
