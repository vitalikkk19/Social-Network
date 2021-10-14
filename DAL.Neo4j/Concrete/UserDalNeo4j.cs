using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Neo4j.Interfaces;
using DTO.Neo4j;
using Neo4jClientRepository;
using Neo4jClient;
using Neo4j;
using DAL.Neo4j.Interfaces;
using Neo4jClient.Cypher;

namespace DAL.Neo4j.Concrete
{
    public class UserDalNeo4j : IUserDalNeo4j
    {
        private string connectionString;
        private string login;
        private string pass;
        public UserDalNeo4j(string connectionString, string login, string pass)
        {
            this.connectionString = connectionString;
            this.login = login;
            this.pass = pass;
        }
        public void AddRelationship(UserLableDTO u1, UserLableDTO u2)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                client.Cypher
                    .Match("(user1:User),(user2:User)")
                    .Where("user1.User_Id = {id1}")
                    .AndWhere("user2.User_Id = {id2}")
                    .WithParam("id1", u1.User_Id)
                    .WithParam("id2", u2.User_Id)
                    .Create("(user1)-[:Friends]->(user2)")
                    .ExecuteWithoutResults();
            }
        }
        public void AddRelationship(int u1_id, int u2_id)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                client.Cypher
                    .Match("(user1:User),(user2:User)")
                    .Where("user1.User_Email = {id1}")
                    .AndWhere("user2.User_Surname = {id2}")
                    .WithParam("id1", u1_id)
                    .WithParam("id2", u2_id)
                    .Create("(user1)-[:Friends]->(user2)")
                    .ExecuteWithoutResults();
            }
        }

        public void AddUser(UserLableDTO u)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();

                client.Cypher.Create("(u:User { UserID: {i},UserEmail: {e},FisrtName: {f},SurName: {s} })")
                    .WithParam("i", u.User_Id)
                    .WithParam("e", u.User_Email)
                    .WithParam("f", u.User_Name)
                    .WithParam("s", u.User_Surname)
                    .ExecuteWithoutResults();
            }
        }



        public bool HasRelationship(UserLableDTO u1, UserLableDTO u2)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                var is_friends = client.Cypher
                   .Match("(user1:User)-[r:Friends]-(user2:User)")
                   .Where((UserLableDTO user1) => user1.User_Id == u1.User_Id)
                   .AndWhere((UserLableDTO user2) => user2.User_Id == u2.User_Id)
                   .Return(r => r.As<Friends>()).Results;
                if (is_friends.Count() > 0)
                {
                    return true;
                }
                return false;

            }
        }

        public int MinPathBetween(UserLableDTO u1, UserLableDTO u2)
        {
            return this.MinPathBetween(u1.User_Id, u2.User_Id);
        }

        public int MinPathBetween(int id1, int id2)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                var res = client.Cypher
                    .Match("(u1:User{User_Email: {id1} }),(u2:User{User_Surname: {id2} })," +
                    " p = shortestPath((u1)-[:Friends*]-(u2))")
                    .WithParam("id1", id1)
                    .WithParam("id2", id2)
                    .Return(ret => ret.As<Result>())
                    .Results;
                int path_len = -1;
                foreach (var t in res)
                {
                    path_len = Convert.ToInt32(t.length);
                }
                return path_len;
            }
        }
        

    }
}
