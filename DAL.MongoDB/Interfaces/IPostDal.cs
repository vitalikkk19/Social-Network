using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MongoDB.Interfaces
{
    interface IPostDal
    {
        void ReadAll();
        Task CreatePostAsync(string e);
        Task PostOverviewAsync(string n, string s, string e);
    }
}
