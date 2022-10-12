﻿using System;
//SUT22 Jonathan Ludvigsson
namespace IndividuelltProjekt
{
    internal class Program
    {
        static void Main(string[] args)
        {

            UserLogin(out string userName);
            GetUserAccounts(userName, out string[,] bankAccounts);
            while (true)
            {
                switch (FunctionChoice(userName))
                {
                    case 1:
                        Console.Clear();
                        ViewAccounts(bankAccounts);
                        ReadKeyMethod();
                        break;
                    case 2: 
                        TransferMoney(bankAccounts);
                        break;
                    case 3: 
                        WithdrawMoney(bankAccounts);
                        break;
                    case 4: 
                        UserLogin(out userName);
                        GetUserAccounts(userName, out bankAccounts);
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
                if (logInTries < 3)
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
        static void GetUserAccounts(string userName, out string[,] bankAccounts)
        {
            bankAccounts = new string[,] { };

            switch (userName)
            {
                case "DR4g0NslAy3r": bankAccounts = new string[,]
                {
                    { "1. Sparkonto", "5000,00" },
                    { "2. Spenderingskonto", "2500,00" }
                };
                    break;
                case "Super": bankAccounts = new string[,]
                {
                    { "1. Sparkonto", "5000,00" },
                    { "2. Spenderingskonto", "2500,00" },
                    { "3. Hemkonto", "4700,47" },
                };
                    break;
                case "1337": bankAccounts = new string[,]
                {
                    { "1. Sparkonto", "5000,00" },
                    { "2. Spenderingskonto", "2500,00" }
                };
                    break;
                case "TestKonto": bankAccounts = new string[,]
                {
                    { "1. Sparkonto", "5000,00" },
                    { "2. Spenderingskonto", "2500,00" }
                };
                    break;
                case "Iron": bankAccounts = new string[,]
                {
                    { "1. Sparkonto", "5000,00" },
                    { "2 Spenderingskonto", "2500,00" }
                };
                    break;
            }

        }
        static void ViewAccounts(string[,] bankAccounts)
        {
            foreach (string accounts in bankAccounts)
            {
                foreach (var item in accounts)
                {
                    Console.Write(item);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static void TransferMoney(string[,] bankAccounts)
        {
            Console.Clear();
            int account1 = 0;
            decimal moneyAmount = 0;
            int account2 = 0;
            decimal amountBefore1 = 0;
            decimal amountAfter1 = 0;
            decimal amountBefore2 = 0;
            decimal amountAfter2 = 0;
            int wrongAmount = 0;

            do
            {
                wrongAmount = 0;
                ViewAccounts(bankAccounts);

                try
                {
                    Console.WriteLine("Här överför du pengar mellan dina konton. Från vilket konto vill du föra över?");
                    account1 = Int32.Parse(Console.ReadLine()) - 1;
                }
                catch
                {
                    wrongAmount = 1;
                    Console.WriteLine("Det där är inte ett giltigt val, skriv in en siffra som motsvarar ett av bankkonton");
                    ReadKeyMethod();
                    Console.Clear();
                }
                
                if (wrongAmount != 1)
                {
                    try
                    {
                        Console.WriteLine("Hur mycket vill du föra över?");
                        moneyAmount = Decimal.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        wrongAmount = 1;
                        Console.WriteLine("Skriv in ett heltal");
                        ReadKeyMethod();
                        Console.Clear();
                    }
                }

                if (moneyAmount < 0)
                {
                    Console.WriteLine("Du kan inte skicka över en negativ mängd pengar");
                    wrongAmount = 1;
                    ReadKeyMethod();
                    Console.Clear();
                }

                amountBefore1 = Decimal.Parse(bankAccounts[account1, 1]);
                amountAfter1 = amountBefore1 - moneyAmount;
                if (amountAfter1 < 0)
                {
                    Console.WriteLine("Ditt konto har inte nog med pengar för att skicka över så mycket.");
                    wrongAmount = 1;
                    ReadKeyMethod();
                    Console.Clear();
                }

                if (wrongAmount != 1)
                {
                    try
                    {
                        Console.WriteLine("Vilket konto vill du föra över till?");
                        account2 = Int32.Parse(Console.ReadLine()) - 1;
                    }
                    catch
                    {
                        wrongAmount = 1;
                        Console.WriteLine("Det där är inte ett giltigt val, skriv in en siffra som motsvarar ett av bankkonton");
                        ReadKeyMethod();
                        Console.Clear();
                    }

                }

            } while (wrongAmount == 1);

            bankAccounts[account1, 1] = Convert.ToString(amountAfter1);

            amountBefore2 = Decimal.Parse(bankAccounts[account2, 1]);
            amountAfter2 = amountBefore2 + moneyAmount;
            bankAccounts[account2, 1] = Convert.ToString(amountAfter2);

            Console.WriteLine();

            ViewAccounts(bankAccounts);
            ReadKeyMethod();

        }
        static void WithdrawMoney(string[,] bankAccounts)
        {
            Console.Clear();
            int account = 0;
            decimal moneyAmount = 0;
            int wrongAmount = 0;
            decimal amountAfter = 0;
            decimal amountBefore = 0;

            do
            {
                wrongAmount = 0;

                ViewAccounts(bankAccounts);

                try
                {
                    Console.WriteLine("Här kan du ta pengar ur dina konton. Vilket konto vill du ta ut ur?");
                    account = Int32.Parse(Console.ReadLine()) - 1;
                }
                catch
                {
                    wrongAmount = 1;
                    Console.WriteLine("Det där är inte ett giltigt val, skriv in en siffra som motsvarar ett av bankkonton");
                    ReadKeyMethod();
                    Console.Clear();
                }

                if (wrongAmount != 1)
                {
                    try
                    {
                        Console.WriteLine("Hur mycket pengar vill du ta ut?");
                        moneyAmount = Decimal.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        wrongAmount = 1;
                        Console.WriteLine("Skriv in ett heltal");
                        ReadKeyMethod();
                        Console.Clear();
                    }
                    
                    if (moneyAmount < 0)
                    {
                        Console.WriteLine("Du kan inte ta ut en negativ mängd pengar");
                        wrongAmount = 1;
                        ReadKeyMethod();
                        Console.Clear();
                    }
                    amountBefore = Convert.ToDecimal(bankAccounts[account, 1]);
                    amountAfter = amountBefore - moneyAmount;
                    if (amountAfter < 0)
                    {
                        Console.WriteLine("Du har inte tillräckligt med pengar för att ta ut så mycket");
                        wrongAmount = 1;
                        ReadKeyMethod();
                        Console.Clear();
                    }
                }

            } while (wrongAmount == 1);

            bankAccounts[account, 1] = Convert.ToString(amountAfter);

            Console.WriteLine();

            ViewAccounts(bankAccounts);
            ReadKeyMethod();
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

