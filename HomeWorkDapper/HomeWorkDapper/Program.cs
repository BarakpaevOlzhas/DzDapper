using HomeWorkDapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new SqlConnection("Data Source=localhost;Integrated Security=True")) //Initial Catalog = UserBase;
            {
                string name = "UserBase";
                var sqlQuery = $"if db_id('UserBase') is null CREATE DATABASE { name }";
                var affectedRows = connection.Execute(sqlQuery);
            }

            using (var connection = new SqlConnection("Data Source=localhost;Initial Catalog = UserBase;Integrated Security=True")) 
            {
                string name = "Users";
                var sqlQuery = $"if object_id('Users')  is null CREATE table { name } (Id int primary key IDENTITY (1,1), Login nvarchar(100),Password nvarchar(100))";              
                var affectedRows = connection.Execute(sqlQuery);
            }

            int choise;

            Console.WriteLine("1)Войти");
            Console.WriteLine("2)Регистрация");

            int.TryParse(Console.ReadLine(),out choise);

            if (choise == 1)
            {
                string login;
                string password;

                Console.WriteLine("Введите логин");
                login = Console.ReadLine();

                Console.WriteLine("Введите пароль");
                password = Console.ReadLine();

                using (var connection = new SqlConnection("Data Source=localhost;Initial Catalog = UserBase;Integrated Security=True"))
                {
                    var person = connection.Query<User>($"SELECT * FROM Users where Login = '{login}' and Password = '{password}'").ToList();
                    if (person.Count != 0) {
                        if (person[0].Login == login && person[0].Password == password)
                        {
                            Console.WriteLine("Вы вошли!");
                        }                        
                    }
                    else
                    {
                        Console.WriteLine("не найден!");
                    }
                }
            }
            else if (choise == 2)
            {
                string login;
                string password;

                Console.WriteLine("Введите логин");
                login = Console.ReadLine();

                Console.WriteLine("Введите пароль");
                password = Console.ReadLine();

                using (var connection = new SqlConnection("Data Source=localhost;Initial Catalog = UserBase;Integrated Security=True"))
                {
                    var sqlQuery = $"INSERT INTO Users VALUES('{login}', '{password}')";

                    var affectedRows = connection.Execute(sqlQuery);

                    Console.WriteLine("Перезапустите приложение");
                }
            }
        }
    }
}
