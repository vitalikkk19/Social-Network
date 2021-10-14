using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MongoDB.Interfaces
{
    interface IUserDal
    {
        void Check(string check);
        Task FollowAsync(string n, string s, string e);
    }
}
