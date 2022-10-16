using System;
//SUT22 Jonathan Ludvigsson
namespace IndividuelltProjekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserLogin(out string userName, out string passWord);
            GetAllBankAccounts(out string[,] bankAccounts);
            GetNumberOfMyBankAccounts(userName, bankAccounts, out int numberOfMyBankAccounts);
            while (true)
            {
                switch (FunctionChoice(userName))
                {
                    case 1:
                        Console.Clear();
                        ViewAccounts(userName, bankAccounts);
                        ReadKeyMethod();
                        break;
                    case 2: 
                        TransferMoney(userName, bankAccounts, numberOfMyBankAccounts);
                        break;
                    case 3: 
                        WithdrawMoney(userName, passWord, bankAccounts, numberOfMyBankAccounts);
                        break;
                    case 4: 
                        UserLogin(out userName, out passWord);
                        GetNumberOfMyBankAccounts(userName, bankAccounts, out numberOfMyBankAccounts);
                        break;

                }
            }

        }

        static void UserLogin(out string userName, out string passWord)
        {
            string[,] usersArray = new string[,]
            {
                { "DR4g0NslAy3r", "SUT22" },
                { "Super", "12345" },
                { "1337", "MyPassword" },
                { "TestKonto", "Lösenord123" },
                { "Iron", "C123" }
            };
            userName = "";
            passWord = "";

            Console.Clear();
            Console.WriteLine("Välkommen till SUT22 banken!");

            int logInTries = 0;
            while (logInTries < 3)
            {
                GetLogInFromUser(out userName, out passWord);

                for (int userNameIndex = 0; userNameIndex < 5; userNameIndex++)
                {
                    if (userName == usersArray[userNameIndex, 0] && passWord == usersArray[userNameIndex, 1])
                    {
                        return;
                    }
                }

                logInTries++;
                if (logInTries != 3)
                {
                    Console.WriteLine($"Fel användarnamn eller lösenord. Du har {3 - logInTries} försök kvar.");
                }

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
        static void GetAllBankAccounts(out string[,] bankAccounts)
        {
            bankAccounts = new string[,]
            {
                {"DR4g0NslAy3r", "1. Sparkonto", "5000,00"},
                {"DR4g0NslAy3r", "2. Spenderingskonto", "2500,00"},
                { "Super", "1. Sparkonto", "2350,24" },
                { "Super", "2. Xyzkonto", "100,09" },
                { "Super", "3. Bortakonto", "4700,47" },
                { "1337", "1. MittKonto", "10,01" },
                { "TestKonto", "1. Sparkonto", "3213,3324" },
                { "TestKonto", "2. Spenderingskonto", "1000,00" },
                { "Iron", "1. Sparkonto", "1000,00" },
                { "Iron", "2. Spenderingskonto", "2000,00" },
                { "Iron", "3. Pengarkonto", "3000,01" },
                { "Iron", "4. Programmeringskonto", "35000,02" },
                { "Iron", "5. Kaffekonto", "3721900,93" }
                };
        }
        static void ViewAccounts(string userName, string[,] bankAccounts)
        {
            for (int i = 0; i <= bankAccounts.GetUpperBound(0); i++)
            {
                if (bankAccounts[i,0] == userName)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        Console.Write(bankAccounts[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
        static void GetNumberOfMyBankAccounts(string userName, string[,] bankAccounts, out int numberOfMyBankAccounts)
        {
            numberOfMyBankAccounts = -1;
            for (int i = 0; i <= bankAccounts.GetUpperBound(0); i++)
            {
                if (bankAccounts[i, 0] == userName)
                {
                    numberOfMyBankAccounts++;
                }
            }
        }
        static void TransferMoney(string userName, string[,] bankAccounts, int numberOfMyBankAccounts)
        {
            int account1 = 0;
            int account2 = 0;
            decimal moneyAmount = 0;
            decimal accountAmount1 = 0;
            decimal accountAmount2 = 0;
            bool noErrors;
            Console.Clear();

            do
            {
                ViewAccounts(userName, bankAccounts);

                Console.WriteLine("Här överför du pengar mellan dina konton. Från vilket konto vill du föra över?");
                noErrors = Int32.TryParse(Console.ReadLine(), out account1);
                account1 -= 1;

                if (account1 > numberOfMyBankAccounts || account1 < 0 || noErrors == false)
                {
                    noErrors = false;
                    Console.WriteLine("Det där är inte ett giltigt val, skriv in en siffra som motsvarar ett av bankkonton");
                    ReadKeyMethod();
                    Console.Clear();
                }
                
                if (noErrors == true)
                {

                    Console.WriteLine("Hur mycket vill du föra över?");
                    noErrors = Decimal.TryParse(Console.ReadLine(), out moneyAmount);

                    if (moneyAmount <= 0 || noErrors == false)
                    {
                        noErrors = false;
                        Console.WriteLine("Du skrev inte in en mängd");
                        ReadKeyMethod();
                        Console.Clear();
                    }

                    for (int i = 0; i <= bankAccounts.GetUpperBound(0); i++)
                    {
                        if (bankAccounts[i, 0] == userName)
                        {
                            accountAmount1 = Decimal.Parse(bankAccounts[i + account1, 2]);
                            accountAmount1 -= moneyAmount;
                            break;
                        }
                    }

                    if (accountAmount1 < 0)
                    {
                        Console.WriteLine("Ditt konto har inte nog med pengar för att skicka över så mycket.");
                        noErrors = false;
                        ReadKeyMethod();
                        Console.Clear();
                    }
                }

                if (noErrors == true)
                {
                    Console.WriteLine("Vilket konto vill du föra över till?");
                    noErrors = Int32.TryParse(Console.ReadLine(), out account2);
                    account2 -= 1;

                    if (account2 > numberOfMyBankAccounts || account2 < 0 || noErrors == false)
                    {
                        noErrors = false;
                        Console.WriteLine("Det där är inte ett giltigt val, skriv in en siffra som motsvarar ett av bankkonton");
                        ReadKeyMethod();
                        Console.Clear();
                    }

                }

            } while (noErrors == false);


            for (int i = 0; i <= bankAccounts.GetUpperBound(0); i++)
            {
                if (bankAccounts[i, 0] == userName)
                {
                    bankAccounts[i + account1, 2] = Convert.ToString(accountAmount1);
                    accountAmount2 = Decimal.Parse(bankAccounts[i + account2, 2]);
                    accountAmount2 += moneyAmount;
                    bankAccounts[i + account2, 2] = Convert.ToString(accountAmount2);
                    break;
                }
            }

            Console.WriteLine();

            ViewAccounts(userName, bankAccounts);
            ReadKeyMethod();

        }
        static void WithdrawMoney(string userName, string passWord, string[,] bankAccounts, int numberOfMyBankAccounts)
        {
            int account = 0;
            decimal moneyAmount = 0;
            decimal accountAmount = 0;
            bool noErrors;
            Console.Clear();

            do
            {
                ViewAccounts(userName, bankAccounts);

                Console.WriteLine("Här kan du ta pengar ur dina konton. Vilket konto vill du ta ut ur?");
                noErrors = Int32.TryParse(Console.ReadLine(), out account);
                account -= 1;

                if (account > numberOfMyBankAccounts || account < 0 || noErrors == false)
                {
                    noErrors = false;
                    Console.WriteLine("Det där är inte ett giltigt val, skriv in en siffra som motsvarar ett av bankkonton");
                    ReadKeyMethod();
                    Console.Clear();
                }

                if (noErrors == true)
                {
                    Console.WriteLine("Hur mycket pengar vill du ta ut?");
                    noErrors = Decimal.TryParse(Console.ReadLine(), out moneyAmount);

                    if (moneyAmount <= 0 || noErrors == false)
                    {
                        Console.WriteLine("Du skrev inte in en mängd");
                        noErrors = false;
                        ReadKeyMethod();
                        Console.Clear();
                    }

                    for (int i = 0; i <= bankAccounts.GetUpperBound(0); i++)
                    {
                        if (bankAccounts[i, 0] == userName)
                        {
                            accountAmount = Decimal.Parse(bankAccounts[i + account, 2]);
                            accountAmount -= moneyAmount;
                            break;
                        }
                    }
                    if (accountAmount < 0)
                    {
                        Console.WriteLine("Du har inte tillräckligt med pengar för att ta ut så mycket");
                        noErrors = false;
                        ReadKeyMethod();
                        Console.Clear();
                    }

                    if (noErrors == true)
                    {
                        Console.WriteLine("Var vänlig skriv in ditt lösenord för att bekräfta att du vill ta ut pengar");
                        if (Console.ReadLine() != passWord)
                        {
                            Console.WriteLine("Du skrev in fel lösenord");
                            noErrors = false;
                            ReadKeyMethod();
                            Console.Clear();
                        }
                    }

                }

            } while (noErrors == false);

            for (int i = 0; i <= bankAccounts.GetUpperBound(0); i++)
            {
                if (bankAccounts[i, 0] == userName)
                {
                    bankAccounts[i + account, 2] = Convert.ToString(accountAmount);
                    break;
                }
            }

            Console.WriteLine();

            ViewAccounts(userName, bankAccounts);
            ReadKeyMethod();
        }
        static int FunctionChoice(string userName)
        {
            bool noErrors;
            int functionChoice;
            do
            {
                Console.Clear();
                Console.WriteLine($"Välkommen {userName} till SUT22 banken. Vad vill du göra?");
                Console.WriteLine("1. Se dina konton och saldo");
                Console.WriteLine("2. Överföring mellan konton");
                Console.WriteLine("3. Ta ut pengar");
                Console.WriteLine("4. Logga ut");

                noErrors = Int32.TryParse(Console.ReadLine(), out functionChoice);

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
                        noErrors = false;
                        ReadKeyMethod();
                        break;
                }
            } while (noErrors == false);
            return 0;
        }
        static void ReadKeyMethod()
        {
            Console.WriteLine("Klicka på Enter för att fortsätta...");
            while (true)
            {
                ConsoleKeyInfo enter = Console.ReadKey(true);
                if (enter.Key == ConsoleKey.Enter)
                {
                    return;
                }
            }

        }
    }
}

