using MauiApp3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Service
{
    internal interface IUserDataService
    {
        public Task<IEnumerable<User>> Get();
        public Task<User> Get(int id);
        public Task<bool> Post(User value);
        public Task<bool> Put(int id,User value);
        public Task<bool> Delete(int id);
        public Task<bool> Get(User values);
    }
}
