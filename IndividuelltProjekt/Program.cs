using System;
//SUT22 Jonathan Ludvigsson
namespace IndividuelltProjekt
{
    internal class Program
    {
        static void Main(string[] args)
        {

            UserLogin(out string userName);
            GetUserAccounts(userName, out string[,] accountsArray);
            while (true)
            {
                switch (FunctionChoice(userName))
                {
                    case 1:
                        Console.Clear();
                        ViewAccounts(accountsArray);
                        Console.ReadKey();
                        break;
                    case 2: 
                        TransferMoney(accountsArray);
                        break;
                    case 3: 
                        WithdrawMoney(accountsArray);
                        break;
                    case 4: 
                        UserLogin(out userName);
                        GetUserAccounts(userName, out accountsArray);
                        break;
                }
            }

        }

        static void UserLogin(out string userName)
        {
            string[,] usersArray = new string[5, 2]
            {
                { "DR4g0NslAy3r", "SUT22" },
                { "Super", "12345" },
                { "1337", "MyPassword" },
                { "TestKonto", "Lösenord123" },
                { "Iron", "C123" }
            };
            userName = "";

            Console.Clear();
            Console.WriteLine("Välkommen till SUT22 banken!");

            int logInTries = 0;
            while (logInTries < 3)
            {
                GetLogInFromUser(out userName, out string passWord);

                for (int userNameIndex = 0; userNameIndex < 5; userNameIndex++)
                {
                    if (userName == usersArray[userNameIndex, 0] && passWord == usersArray[userNameIndex, 1])
                    {
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
        static void GetLogInFromUser(out string userName, out string passWord)
        {
            Console.WriteLine("Var vänlig skriv in ditt användarnamn");
            userName = Console.ReadLine();
            Console.WriteLine("Skriv nu in ditt lösenord");
            passWord = Console.ReadLine();
        }
        static void GetUserAccounts(string userName, out string[,] accountsArray)
        {
            accountsArray = new string[,] { };

            switch (userName)
            {
                case "DR4g0NslAy3r": accountsArray = new string[,]
                {
                    { "1. Sparkonto", "5000,00" },
                    { "2. Spenderingskonto", "2500,00" }
                };
                    break;
                case "Super": accountsArray = new string[,]
                {
                    { "1. Sparkonto", "5000,00" },
                    { "2. Spenderingskonto", "2500,00" },
                    { "3. Hemkonto", "4700,47" },
                };
                    break;
                case "1337": accountsArray = new string[,]
                {
                    { "1. Sparkonto", "5000,00" },
                    { "2. Spenderingskonto", "2500,00" }
                };
                    break;
                case "TestKonto": accountsArray = new string[,]
                {
                    { "1. Sparkonto", "5000,00" },
                    { "2. Spenderingskonto", "2500,00" }
                };
                    break;
                case "Iron": accountsArray = new string[,]
                {
                    { "1. Sparkonto", "5000,00" },
                    { "2 Spenderingskonto", "2500,00" }
                };
                    break;
            }

        }
        static void ViewAccounts(string[,] accountsArray)
        {
            foreach (string accounts in accountsArray)
            {
                foreach (var item in accounts)
                {
                    Console.Write(item);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static void TransferMoney(string[,] accountsArray)
        {
            Console.Clear();
            int account1 = 0;
            int account2 = 0;
            decimal moneyAmount = 0;

            ViewAccounts(accountsArray);

            Console.WriteLine("Här överför du pengar mellan dina konton. Från vilket konto vill du föra över?");
            account1 = Int32.Parse(Console.ReadLine()) - 1;

            Console.WriteLine("Hur mycket vill du föra över?");
            moneyAmount = Decimal.Parse(Console.ReadLine());

            Console.WriteLine("Vilket konto vill du föra över till?");
            account2 = Int32.Parse(Console.ReadLine()) - 1;

            decimal amountBefore1 = Decimal.Parse(accountsArray[account1, 1]);
            decimal amountAfter1 = amountBefore1 - moneyAmount;
            accountsArray[account1, 1] = Convert.ToString(amountAfter1);

            decimal amountBefore2 = Decimal.Parse(accountsArray[account2, 1]);
            decimal amountAfter2 = amountBefore2 + moneyAmount;
            accountsArray[account2, 1] = Convert.ToString(amountAfter2);

        }
        static void WithdrawMoney(string[,] accountsArray)
        {
            Console.Clear();
            int account = 0;
            decimal moneyAmount = 0;

            ViewAccounts(accountsArray);

            Console.WriteLine("Här kan du ta pengar ur dina konton. Vilket konto vill du ta ut ur?");
            account = Int32.Parse(Console.ReadLine()) - 1;
            Console.WriteLine("Hur mycket pengar vill du ta ut?");
            moneyAmount = Decimal.Parse(Console.ReadLine());

            decimal amountBefore = Convert.ToDecimal(accountsArray[account, 1]);
            decimal amountAfter = amountBefore - moneyAmount;
            accountsArray[account, 1] = Convert.ToString(amountAfter);
        }
        static int FunctionChoice(string userName)
        {
            int wrongInput;
            Console.Clear();
            Console.WriteLine($"Välkommen {userName} till SUT22 banken. Vad vill du göra?");
            do
            {
                wrongInput = 0;
                int functionChoice = 0;
                Console.WriteLine("1. Se dina konton och saldo");
                Console.WriteLine("2. Överföring mellan konton");
                Console.WriteLine("3. Ta ut pengar");
                Console.WriteLine("4. Logga ut");
                try
                {
                    functionChoice = Int32.Parse(Console.ReadLine());
                }
                catch
                {

                }

                switch (functionChoice)
                {
                    case 1:
                        return 1;
                    case 2:
                        return 2;
                    case 3:
                        return 3;
                    case 4:
                        return 4;
                    default:
                        Console.WriteLine("Ogiltigt val. Skriv in ett värde mellan 1 och 4");
                        wrongInput = 1;
                        break;
                }
            } while (wrongInput == 1);
            return 0;
        }
    }
}

