using System;
using DbConnection;

namespace Simple_CRUD_MySQL
{
    class Program
    {
        static void Main(string[] args)
        {
            displayChoices();
        }

        public static void displayChoices(){
            Console.Clear();
            Console.WriteLine("***************************");
            Console.WriteLine("*                         *");
            Console.WriteLine("*    Add User [A]:        *");
            Console.WriteLine("*    Udate User [U]:      *");
            Console.WriteLine("*    Delete User [D]:     *");
            Console.WriteLine("*                         *");
            Console.WriteLine("***************************");
            Console.Write("Make a Selection: ");
            string InputLine = Console.ReadLine();
            if(InputLine=="a"){
                addNewUser();
            }else if(InputLine=="u"){
                updateUser();
            }else if(InputLine=="d"){
                deleteUser();
            }
        }

        public static void displayUsers(){
            var users = DbConnector.Query("SELECT * FROM users");

            Console.Clear();
            Console.WriteLine("*************************************");
            Console.WriteLine("*                                   *");
            foreach(var user in users){
                Console.WriteLine("*   "+user["id"] + ") " + user["FirstName"] + " " + user["LastName"] + " - Fav Num: " + user["FavoriteNumber"]);
            }
            Console.WriteLine("*                                   *");
            Console.WriteLine("*************************************");
        }

        public static void addNewUser(){
            Console.Clear();
            Console.WriteLine("*************************************");
            Console.Write("Enter First Name: ");
            string firstname = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastname = Console.ReadLine();

            Console.Write("Enter Favorite Number: ");
            string favNum = Console.ReadLine();

            string insertQuery = $"INSERT INTO users (FirstName, LastName, FavoriteNumber) VALUES ('{firstname}', '{lastname}', '{favNum}')";
            DbConnector.Execute(insertQuery);
            Console.WriteLine("*************************************");

            displayUsers();
            Console.Write("Press any key to continue: ");
            string InputLine = Console.ReadLine();
            displayChoices();
        }

        public static void updateUser(){
            displayUsers();

            Console.Write("Select User Id to edit: ");
            string userId = Console.ReadLine();
            if(userId == ""){
                displayChoices();
            }else{
                var user = DbConnector.Query($"SELECT * FROM consoledb.users where id = {userId}");

                Console.Clear();
                Console.WriteLine("Press Enter to keep values.");
                Console.WriteLine("*************************************");

                Console.Write("Update First Name ({0}): ", user[0]["FirstName"]);
                string firstname = Console.ReadLine();
                if(firstname == "") firstname = user[0]["FirstName"] as string;

                Console.Write("Enter Last Name ({0}): ", user[0]["LastName"]);
                string lastname = Console.ReadLine();
                if(lastname == "") lastname = user[0]["LastName"] as string;

                Console.Write("Enter Favorite Number ({0}): ", user[0]["FavoriteNumber"]);
                string favNum = Console.ReadLine();
                if(favNum == "") favNum = user[0]["FavoriteNumber"].ToString();

                Console.WriteLine("*************************************");

                string updateQuery = $"UPDATE users SET FirstName='{firstname}', LastName='{lastname}', FavoriteNumber='{favNum}' WHERE id="+userId;
                DbConnector.Execute(updateQuery);

                displayUsers();
                Console.Write("Press any key to continue: ");
                string InputLine = Console.ReadLine();
                displayChoices();
            }
        }

        public static void deleteUser(){
            displayUsers();

            Console.Write("Select User Id to delete: ");
            string userId = Console.ReadLine();
            if(userId == ""){
                displayChoices();
            }else{
                string deleteQuery = $"DELETE FROM users WHERE id="+userId;
                DbConnector.Execute(deleteQuery);

                displayUsers();
                Console.Write("Press any key to continue: ");
                string InputLine = Console.ReadLine();
                displayChoices();
            }
        }
    }
}
