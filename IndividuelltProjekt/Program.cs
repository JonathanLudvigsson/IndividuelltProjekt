using System;
using System.Runtime.CompilerServices;
using System.Threading.Channels;

namespace IndividuelltProjekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserLogin();
            FunctionChoice();

        }

        static void GetLogInFromUser(out string userName, out string passWord)
        {
            Console.WriteLine("Var vänlig skriv in ditt användarnamn");
            userName = Console.ReadLine();
            Console.WriteLine("Skriv nu in ditt lösenord");
            passWord = Console.ReadLine();
        }
        static void UserLogin()
        {
            string[,] usersArray = new string[5, 2] {
                { "DR4g0NslAy3r", "SUT22" },
                { "Super", "12345" },
                { "1337", "MyPassword" },
                { "TestKonto", "Lösenord123" },
                { "iron", "C123" }
            };

            Console.WriteLine("Välkommen till SUT22 banken!");

            int logInTries = 0;
            while (logInTries < 3)
            {
                GetLogInFromUser(out string userName, out string passWord);

                for (int userNameIndex = 0; userNameIndex < 5; userNameIndex++)
                {
                    if (userName == usersArray[userNameIndex, 0] && passWord == usersArray[userNameIndex, 1])
                    {
                        Console.WriteLine("Du skrev in rätt användarnamn och lösenord");
                        return;
                    }
                }

                logInTries++;
                Console.WriteLine($"Fel användarnamn eller lösenord. Du har {3 - logInTries} försök kvar.");
            }

            Console.WriteLine("Du skrev in fel användarnamn eller lösenord för många gånger.");
            Console.WriteLine("Stänger programmet...");
            Environment.Exit(0);
        }
        static void ViewAccounts()
        {

        }
        static void TransferMoney()
        {

        }
        static void WithdrawMoney()
        {

        }
        static void FunctionChoice()
        {
            int wrongInput;
            Console.WriteLine("Välkommen till SUT22 banken. Vad skulle du vilja göra?");
            do
            {
                wrongInput = 0;
                Console.WriteLine("1. Se dina konton och saldo");
                Console.WriteLine("2. Överföring mellan konton");
                Console.WriteLine("3. Ta ut pengar");
                Console.WriteLine("4. Logga ut");
                int functionChoice = Int32.Parse(Console.ReadLine());
                switch (functionChoice)
                {
                    case 1:
                        ViewAccounts();
                        break;
                    case 2:
                        TransferMoney();
                        break;
                    case 3:
                        WithdrawMoney();
                        break;
                    case 4:
                        Console.WriteLine("Tack för att du använde SUT22 banken! Vi loggar ut dig nu.");
                        UserLogin();
                        break;
                    default:
                        Console.WriteLine("Fel input. Skriv in ett värde mellan 1 och 4");
                        wrongInput = 1;
                        break;
                }
            } while (wrongInput == 1);
            return;
        }
    }
}

