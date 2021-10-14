using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using DTO.MongoDB;
using BussinesLogic;

namespace Social_Network
{
    public class Program
    {
        static void Main(string[] args)
        {
            MongoUser mu;
            NeoUser nu;
            Console.WriteLine("Hello,sweety,enter your email,please!");
            var em = Console.ReadLine();
            mu.Login(em);

            mu.ReadAllPosts();
            Menu(em, mu, nu);

            Console.ReadKey();

        }
        public static void Menu(string e, MongoUser m, NeoUser n)
        {
            Console.WriteLine("Do you want to:\n 1-Find new friend \n " +
                "2-Write a post\n" +
                "3-Look over s-bodies post\n" +
                "4-Go out");
            var x = Console.ReadLine();
            switch (x)
            {
                case "1":
                    Console.WriteLine("Write name of searched person ");
                    string N = Console.ReadLine();
                    Console.WriteLine("Write surname now");
                    string S = Console.ReadLine();
                    if (n.Relationship == true) { Console.WriteLine("There is relationship"); }
                    else { Console.WriteLine("There is not relationship"); }
                    Console.WriteLine("The lenght is", n.PathBetweenID(e, S));

                    m.ToFollow(N, S, e);
                    n.Follow(e, S);
                    Menu(e, m, n);

                    break;
                case "3":
                    Console.WriteLine("Write name of searched person ");
                    N = Console.ReadLine();
                    Console.WriteLine("Write surname now");
                    S = Console.ReadLine();
                    m.PostReaction(N, S, e);
                    Menu(e, m, n);
                    break;
                case "2":
                    m.CreatePost(e);
                    Menu(e, m, n);
                    break;
                case "4":
                    Console.WriteLine("Bye:(");
                    Thread.Sleep(1000);
                    System.Environment.Exit(20);
                    break;
            }
        }

    }
}
