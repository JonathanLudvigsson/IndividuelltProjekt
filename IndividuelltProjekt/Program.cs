using System;
//SUT22 Jonathan Ludvigsson
namespace IndividuelltProjekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserLogin(out string userName, out string passWord); //Asks user to log in, if log in succeeds we send out their userName and passWord
            GetAllBankAccounts(out string[,] bankAccounts); //Sends out an array of all the bank accounts
            GetNumberOfMyBankAccounts(userName, bankAccounts, out int numberOfMyBankAccounts); //Sends out an integer of how many bank accounts the current user has
            while (true)
            {
                switch (FunctionChoice(userName)) //Returns a value 1-4, is used for user to select which function they want to use in the bank
                {
                    case 1:
                        Console.Clear();
                        ViewAccounts(userName, bankAccounts); //Prints all the current user's bank accounts
                        ReadKeyMethod(); //Asks user to press the enter button to continue, will only continue of enter is pressed
                        break;
                    case 2: 
                        TransferMoney(userName, bankAccounts, numberOfMyBankAccounts); //Method where user can transfer money inbetween their bank accounts
                        break;
                    case 3: 
                        WithdrawMoney(userName, passWord, bankAccounts, numberOfMyBankAccounts); //Method where user can withdraw money from one of their bank accounts
                        break;
                    case 4: 
                        UserLogin(out userName, out passWord); //User logs out and program asks for new log in, updates userName and passWord if succeeded
                        GetNumberOfMyBankAccounts(userName, bankAccounts, out numberOfMyBankAccounts); //Updates how many bank account the current user has
                        break;

                }
            }

        }

        static void UserLogin(out string userName, out string passWord) //Method for user login
        {
            string[,] usersArray = new string[,] //Array of usernames and passwords, connected with 2d array
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

            int logInTries = 0; //Counter for how many times the user has failed to log in
            while (logInTries < 3) //User can only try to log int three times
            {
                GetLogInFromUser(out userName, out passWord); //Asks user to input userName and passWord, then sends out what they put in as userName and passWord

                for (int userNameIndex = 0; userNameIndex < 5; userNameIndex++) //Array that goes through all rows in the usersArray and checks if the userName and passWord are correct
                {
                    if (userName == usersArray[userNameIndex, 0] && passWord == usersArray[userNameIndex, 1])
                    {
                        return; //If the username and password are correct, user is sent out of this method
                    }
                }

                logInTries++;
                if (logInTries != 3) //Tells user how many times they have failed to log in, not needed if logInTries == 3 because then another message will be printed
                {
                    Console.WriteLine($"Fel användarnamn eller lösenord. Du har {3 - logInTries} försök kvar.");
                }

            }

            Console.WriteLine("Du skrev in fel användarnamn eller lösenord för många gånger."); //Tells user they've failed to log in too many times and then closes program
            Console.WriteLine("Stänger programmet...");
            Environment.Exit(0);
        }
        static void GetLogInFromUser(out string userName, out string passWord) //Method for asking user to log in and then retrieving userName and passWord from user input
        {
            Console.WriteLine("Var vänlig skriv in ditt användarnamn");
            userName = Console.ReadLine();
            Console.WriteLine("Skriv nu in ditt lösenord");
            passWord = Console.ReadLine();
        }
        static void GetAllBankAccounts(out string[,] bankAccounts) //Method for storing all bank accounts, array is sent out to be used in other methods
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
        static void ViewAccounts(string userName, string[,] bankAccounts) //Method which prints all the current user's bank accounts
        {
            for (int i = 0; i <= bankAccounts.GetUpperBound(0); i++)
            {
                if (bankAccounts[i,0] == userName) //If the first column of the current row is the same as the user's username then the row will be printed
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
        static void GetNumberOfMyBankAccounts(string userName, string[,] bankAccounts, out int numberOfMyBankAccounts) //Stores how many bank accounts belongs to the current user
        {
            numberOfMyBankAccounts = -1; //Accounts for 0-index
            for (int i = 0; i <= bankAccounts.GetUpperBound(0); i++) //Goes through every first column of every row and checks if that index belongs to the current user
            {
                if (bankAccounts[i, 0] == userName)
                {
                    numberOfMyBankAccounts++; //Is used to make sure user cannot access bank accounts that don't belong to them in the other methods
                }
            }
        }
        static void TransferMoney(string userName, string[,] bankAccounts, int numberOfMyBankAccounts) //Method for transferring money inbetween user's own accounts
        {
            int account1 = 0; //Is used to ensure correct bank account is transferred from/to by adding account1 to the row index of the user's first bank account in bankAccounts array later
            int account2 = 0; //Same as above
            decimal moneyAmount = 0;
            decimal accountAmount1 = 0;
            decimal accountAmount2 = 0;
            bool noErrors; //Is used to ensure nothing goes wrong, such as a user inputting a letter as the amount of money they want to transfer
            Console.Clear();

            do
            {
                ViewAccounts(userName, bankAccounts); //Prints all user's bank accounts

                Console.WriteLine("Här överför du pengar mellan dina konton. Från vilket konto vill du föra över?");
                noErrors = Int32.TryParse(Console.ReadLine(), out account1); //User selects which bank account they want to transfer from, is chosen by number
                account1 -= 1; //Accounts for 0-index

                if (account1 > numberOfMyBankAccounts || account1 < 0 || noErrors == false) //Ensures user can only select their own bank accounts
                {
                    noErrors = false;
                    Console.WriteLine("Det där är inte ett giltigt val, skriv in en siffra som motsvarar ett av bankkonton");
                    ReadKeyMethod();
                    Console.Clear();
                }
                
                if (noErrors == true) //Checks if first account is successfully chosen
                {

                    Console.WriteLine("Hur mycket vill du föra över?");
                    noErrors = Decimal.TryParse(Console.ReadLine(), out moneyAmount); //User chooses how much money they want to transfer

                    if (moneyAmount <= 0 || noErrors == false) //Ensures user can't send nothing or a negative amount of money
                    {
                        noErrors = false;
                        Console.WriteLine("Du skrev inte in en mängd");
                        ReadKeyMethod();
                        Console.Clear();
                    }

                    for (int i = 0; i <= bankAccounts.GetUpperBound(0); i++) //Runs through the bankAccounts array
                    {
                        if (bankAccounts[i, 0] == userName) //Checks if current index belongs to the user
                        {
                            accountAmount1 = Decimal.Parse(bankAccounts[i + account1, 2]); //Store the amount of money in the chosen account they want to transfer from
                            accountAmount1 -= moneyAmount; //Amount of money the chosen first account should have after transfer is stored
                            break; //Money in first account has been stored, no need to keep looping
                        }
                    }

                    if (accountAmount1 < 0) //Ensures user cannot transfer more money than the first account has
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
                    noErrors = Int32.TryParse(Console.ReadLine(), out account2); //User selects the account they want to transfer the money to
                    account2 -= 1; //Accounts for 0-index

                    if (account2 > numberOfMyBankAccounts || account2 < 0 || noErrors == false) //Ensures user can only select their own bank accounts
                    {
                        noErrors = false;
                        Console.WriteLine("Det där är inte ett giltigt val, skriv in en siffra som motsvarar ett av bankkonton");
                        ReadKeyMethod();
                        Console.Clear();
                    }

                }

            } while (noErrors == false); //If any errors are found, program loops and user inputs all the variables again


            for (int i = 0; i <= bankAccounts.GetUpperBound(0); i++) //Loops through all bank accounts until it finds one which belongs to user
            {
                if (bankAccounts[i, 0] == userName) //bankAccounts array is updated to reflect the transferral of money in the correct bank accounts
                {
                    bankAccounts[i + account1, 2] = Convert.ToString(accountAmount1);
                    accountAmount2 = Decimal.Parse(bankAccounts[i + account2, 2]);
                    accountAmount2 += moneyAmount;
                    bankAccounts[i + account2, 2] = Convert.ToString(accountAmount2);
                    break;
                }
            }

            Console.WriteLine();

            ViewAccounts(userName, bankAccounts); //Bank accounts are shown again so user can see the changes
            ReadKeyMethod(); //Waits for user to press enter

        }
        static void WithdrawMoney(string userName, string passWord, string[,] bankAccounts, int numberOfMyBankAccounts) //Method for withdrawing money from bank account
        {
            int account = 0; //Same as the accounts above, is added to row index of the user's first bank account to ensure we update the correct account later
            decimal moneyAmount = 0;
            decimal accountAmount = 0;
            bool noErrors;
            Console.Clear();

            do
            {
                ViewAccounts(userName, bankAccounts);

                Console.WriteLine("Här kan du ta pengar ur dina konton. Vilket konto vill du ta ut ur?");
                noErrors = Int32.TryParse(Console.ReadLine(), out account); //User picks which account they want to transfer from
                account -= 1; //Accounts for 0-index

                if (account > numberOfMyBankAccounts || account < 0 || noErrors == false) //Same as in TransferMoney, ensure they only access their own bank accounts
                {
                    noErrors = false;
                    Console.WriteLine("Det där är inte ett giltigt val, skriv in en siffra som motsvarar ett av bankkonton");
                    ReadKeyMethod();
                    Console.Clear();
                }

                if (noErrors == true)
                {
                    Console.WriteLine("Hur mycket pengar vill du ta ut?");
                    noErrors = Decimal.TryParse(Console.ReadLine(), out moneyAmount); //Amount of money user wants to withdraw is gotten

                    if (moneyAmount <= 0 || noErrors == false) //User can't withdraw 0 or negative amount of money
                    {
                        Console.WriteLine("Du skrev inte in en mängd");
                        noErrors = false;
                        ReadKeyMethod();
                        Console.Clear();
                    }

                    for (int i = 0; i <= bankAccounts.GetUpperBound(0); i++) //Runs through bank account array
                    {
                        if (bankAccounts[i, 0] == userName)
                        {
                            accountAmount = Decimal.Parse(bankAccounts[i + account, 2]); //Amount of money chosen bank account has is stored
                            accountAmount -= moneyAmount; //Amount of money the chosen bank account should have after withdrawal is stored
                            break;
                        }
                    }
                    if (accountAmount < 0) //Ensuers user can't take out more money than their bank account has
                    {
                        Console.WriteLine("Du har inte tillräckligt med pengar för att ta ut så mycket");
                        noErrors = false;
                        ReadKeyMethod();
                        Console.Clear();
                    }

                    if (noErrors == true)
                    {
                        Console.WriteLine("Var vänlig skriv in ditt lösenord för att bekräfta att du vill ta ut pengar");
                        if (Console.ReadLine() != passWord) //Asks user for password, if password does not match current user's password noErrors is set to false
                        {
                            Console.WriteLine("Du skrev in fel lösenord");
                            noErrors = false;
                            ReadKeyMethod();
                            Console.Clear();
                        }
                    }

                }

            } while (noErrors == false); //Loops if any errors were found

            for (int i = 0; i <= bankAccounts.GetUpperBound(0); i++) //Runs through all bank accounts
            {
                if (bankAccounts[i, 0] == userName)
                {
                    bankAccounts[i + account, 2] = Convert.ToString(accountAmount); //Amount of money in the chosen bank account is updated
                    break;
                }
            }

            Console.WriteLine();

            ViewAccounts(userName, bankAccounts); //User's bank accounts are printed again so user can see the changes in money
            ReadKeyMethod();
        }
        static int FunctionChoice(string userName) //Method for user to select which program function they want to access
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

                noErrors = Int32.TryParse(Console.ReadLine(), out functionChoice); //Gets user input of 1-4 for what they want to do

                switch (functionChoice) //Returns value of user's input of 1-4 to main then exits method
                {
                    case 1:
                        return 1;
                    case 2:
                        return 2;
                    case 3:
                        return 3;
                    case 4:
                        return 4;
                    default: //If user input is not 1-4, set noErrors to false and loop
                        Console.WriteLine("Ogiltigt val. Skriv in ett värde mellan 1 och 4");
                        noErrors = false;
                        ReadKeyMethod();
                        break;
                }
            } while (noErrors == false); //Loops if user input is not 1-4
            return 0;
        }
        static void ReadKeyMethod() //Method which tells user to press enter and only progresses if user presses enter
        {
            Console.WriteLine("Klicka på Enter för att fortsätta...");
            while (true)
            {
                ConsoleKeyInfo enter = Console.ReadKey(true); //User input is hidden due to the (true)
                if (enter.Key == ConsoleKey.Enter) //Checks if user presses enter
                {
                    return;
                }
            }

        }
    }
}

