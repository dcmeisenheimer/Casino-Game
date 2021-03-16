using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace CasinoGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int gamesWon = 0, gamesLost = 0, gamesPlayed = 0,  gil = 50; //variables used to store leaderboard and game data
            GameRules();
            
            string userInput = GetMenuChoice(); //calls the method to store what menu option the user selects
        
            while (userInput != "7") //continually loops as long as user doesnt enter 7
            {
                Route(userInput, ref gamesWon, ref gamesLost, ref gamesPlayed, ref gil); //calls the route method to go to menu option selected with referenced variables
                
                if (UserBetGil(ref gil) <= 0 || UserBetGil(ref gil) >= 300) //an if statement that ends the game if gil balance exceeds or drops below game rule
                {
                    userInput = "7"; //ends the loop if they win or lose
                }
                else
                {
                    userInput = GetMenuChoice(); //if value doesnt exceed game rule they keep playing
                }
            }
        
            GoodBye(gamesWon, gamesLost, gamesPlayed, gil); //final method to show users score and leaderboard before exiting the program
        }
        public static void GameRules() //rules for the casino
        {
            System.Console.WriteLine("Welcome to Jolly Jackpot Land!");
            System.Console.WriteLine("You have been awared 50 gils to start");
            System.Console.WriteLine("In order to win you must gamble your way to 300 gils");
            System.Console.WriteLine("If your gils drop to 0, You Lose!");
            System.Console.WriteLine("Goodluck!");
            System.Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        public static string GetMenuChoice()
        {
            DisplayMenu(); //calls the method to show menu
            string userInput = Console.ReadLine(); //stores users selection
            
            while (!ValidMenuChoice(userInput)) //loop that starts if a valid number isnt selected
            {
                System.Console.WriteLine("Invalid menu choice.  Please Enter a Valid Menu Choice"); 
                System.Console.WriteLine("Press any key to continue....");
                Console.ReadKey();

                DisplayMenu(); //returns to menu to make another selection
                userInput = Console.ReadLine();
            }
            return userInput; //returns users input to be passed to route
        }
        public static void DisplayMenu()
        {
            Console.Clear(); //menu selection display
            System.Console.WriteLine("1: Slot Machine");
            System.Console.WriteLine("2: Dice");
            System.Console.WriteLine("3: Roulette Wheel");
            System.Console.WriteLine("4: Black Jack");
            System.Console.WriteLine("5: Leaderboard");
            System.Console.WriteLine("6: Gil Balance");
            System.Console.WriteLine("7: Exit");
        }
        public static bool ValidMenuChoice(string userInput)
        {
            switch (userInput)
            {
                case "1":
                return true; //true means it will keep the application running

                case "2":
                return true;

                case "3":
                return true;
                
                case "4":
                return true;

                case "5":
                return true;

                case "6":
                return true;

                case "7":
                return true;

                default:
                return false; // natural state is false
            }
            
        }
        public static void Route(string userInput, ref int gamesWon, ref int gamesLost, ref int gamesPlayed, ref int gil)
        {
            switch (userInput)
            {
                case "1":
                SlotMachine(ref gamesWon, ref gamesLost, ref gamesPlayed, ref gil, ref userInput); // values being updated throughout games
                break;

                case "2":
                GilChecker(ref gamesWon, ref gamesLost, ref gamesPlayed, ref gil, ref userInput);
                break;

                case "3":
                RouletteWheel(ref gamesWon, ref gamesLost, ref gamesPlayed, ref gil, ref userInput);
                break;

                case "4":
                BlackJack(ref gamesWon, ref gamesLost, ref gamesPlayed, ref gil, ref userInput);
                break;

                case "5":
                ScoreBoard(gamesWon, gamesLost, gamesPlayed, gil);
                break;

                case "6":
                UserBetGil(ref gil); //shows user gil amount
                System.Console.WriteLine("Your current balance is " + gil + " gils");
                System.Console.WriteLine("Press any key to continue....");
                Console.ReadKey();
                break;

                case "7":
                GoodBye(gamesWon, gamesLost, gamesPlayed, gil);
                break;
                
            }
        }
        public static void SlotMachineRules() //slot machine rules
        {
            System.Console.WriteLine("The game will randomly select 3 words from the following list" +
             "\n1: Elephant" + "\t2: Computer" + "\t3: Football" + "\t4: Resume" + "\t5: Capstone" + "\t6: Crimson" + 
             "\nIf none of the words match you lose your bet" + "\nIf 2 words match you doubled your bet" + 
             "\nIf 3 words matched you tripled your bet");
            System.Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        public static void SlotMachine(ref int gamesWon, ref int gamesLost, ref int gamesPlayed, ref int gil, ref string userInput)
        {
            
            SlotMachineRules();
            gamesPlayed++; //updates games played for leaderboard
            System.Console.WriteLine("You currently have " + gil + " gils"); 
            System.Console.WriteLine("How many gills would you like to bet?");
            int userBet = int.Parse(Console.ReadLine());

            if (userBet > gil) //checks is user bet over current gil 
            {
                System.Console.WriteLine("Sorry bet exceeds gil amount currently stored..." + "\nReturning to Menu");
                System.Console.WriteLine("Press any key to return to menu");
                Console.ReadKey();
            }
            else
            {
                List<string> words = new List<string>{"Elephant", "Computer", "Football", "Resume", "Capstone", "Crimson"}; //a list containing the selected words
                var random = new Random();

                int word1 = random.Next(words.Count); //3 random variables that choose a word from the list
                int word2 = random.Next(words.Count);
                int word3 = random.Next(words.Count);

                System.Console.Write(words[word1] + "   ");
                Thread.Sleep(1000); //i used threading to make a delay between each word like a real slot machine.
                System.Console.Write(words[word2] + "   ");
                Thread.Sleep(1000);
                System.Console.WriteLine(words[word3]);
                Thread.Sleep(1000);
                SlotPoints(word1, word2, word3, ref gil, ref gamesPlayed, ref gamesLost, ref gamesWon, userBet); 
                PlayAgainDecision(ref gamesWon, ref gamesLost, ref gamesPlayed, ref gil, ref userInput);
            }
        }
        public static void SlotPoints(int word1, int word2, int word3, ref int gil, ref int gamesPlayed, ref int gamesLost, ref int gamesWon, int userBet) //method that calculates how many words match
        {
            if (word1 == word2 && word1 == word3) //all 3 match if statement
            {
                gamesWon++;
                gil = gil + (userBet *2);
                System.Console.WriteLine("You have 3 matching words!" + "\nYou won " + userBet * 3 + " gil");
                System.Console.WriteLine("You have a total gil of " + gil);
                System.Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                
            }
            else if(word1 == word2 && word1 != word3 || word2 == word3 && word2 != word1 || word1 == word3 && word3 != word2) //2 match else if statement
            {
                gamesWon++;
                gil = gil + (userBet *1);
                System.Console.WriteLine("You have 2 matching words!" + "\nYou won " + userBet * 2 + " gil");
                System.Console.WriteLine("You have a total gil of " + gil);
                System.Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else //no matches
            {
                if (userBet == gil) //if user bets max and loses the game ends.
                {
                    gil = 0;                 
                }
                else
                {
                    gamesLost++;
                    gil = gil - userBet;
                    System.Console.WriteLine("You have 0 matching words!" + "\nYou lost " + userBet + " gil");
                    System.Console.WriteLine("You have a total gil of " + gil);
                    System.Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                } 
            }            
        }
        public static void DiceRules() //rules of dice disolay
        {
            System.Console.WriteLine("The rules of dice" + 
            "\n1: The game will roll five 6-sided dice " + 
            "\n2: The range of the total die is 5-30" + 
            "\n3: You have a total of 4 guesses before you lose" + 
            "\n4: Each guess cost 3 gil" + 
            "\n5: If you guess the right total before 4 guess you win 50 gil" + 
            "\n6: If you cannot correctly guess within 4 turns you forfeit the gil paid to guess");
            System.Console.WriteLine("Press any key to continue.....");
            Console.ReadKey();
        }
        public static void GilChecker(ref int gamesWon, ref int gamesLost, ref int gamesPlayed, ref int gil, ref string userInput) //method to check if bet exceeds gil balance
        {
            int sum = 0;
            DiceRules();
            System.Console.WriteLine("Your Current Gil Balance is " + gil + " gils");

            if (gil < 12) //12 is the limit because any less and the user cannot make 4 guesses
            {
                System.Console.WriteLine("Sorry bet exceeds gil amount currently stored");
                System.Console.WriteLine("You needed a minimum of 12 gil to play");
                System.Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                gamesPlayed++;
                sum = RandomDice(); //calling random dice generator
                UserGuess(ref gil, sum, ref gamesWon, ref gamesLost); //method for guessing
                PlayAgainDecision(ref gamesWon, ref gamesLost, ref gamesPlayed, ref gil, ref userInput);
            }

        }
        public static int RandomDice() //method for 5 random dice and the return of the sum
        {
            Random rnd = new Random();

            int [] dice = new int [5]; //dice array to store randon numbers
            dice[0] = rnd.Next(1,7);
            dice[1] = rnd.Next(1,7);
            dice[2] = rnd.Next(1,7);
            dice[3] = rnd.Next(1,7);
            dice[4] = rnd.Next(1,7);

            int sum = dice.Sum(); //sum will equal the sum of the array
            return sum;
        }
        public static void UserGuess(ref int gil, int sum, ref int gamesWons, ref int gamesLost) //method to guess what number it is.
        {
            int userDieGuess = 0;
            int count = 1;
            System.Console.WriteLine("The dice have been rolled what is your guess for the total amount?");
            userDieGuess = int.Parse(Console.ReadLine());
            while(userDieGuess!= sum && count != 4) //while loop that keeps going as long 4 guesses have not been used and they didnt already guess correctly
            {
                count++;
                DieSumChecker(userDieGuess, sum); //checks if they match method
                System.Console.WriteLine("The dice have been rolled what is your guess for the total amount?");
                userDieGuess = int.Parse(Console.ReadLine()); //keeps the variable updated
            }
            if (userDieGuess == sum) //if user guesses correctly they get 50 gil
            {
                gamesWons++;
                gil = gil + 50;
                System.Console.WriteLine("You guessed correctly good job!");
                System.Console.WriteLine("You won 50 gil!");
                System.Console.WriteLine("You have a total gil of " + gil);
                System.Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                
            }
            else //if user doesnt guess correctly they lose 12 gil
            {
                gil = gil - 12;
                gamesLost++;
                System.Console.WriteLine("Sorry you lost!");
                System.Console.WriteLine("The correct number was " + sum);
                System.Console.WriteLine("You lost 12 gil!");
                System.Console.WriteLine("You have a total gil of " + gil);
                System.Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                
            }
            
        }
        public static void DieSumChecker(int userDieGuess, int sum) //checks to see if teh guess matches the sum
        {
            if(userDieGuess > sum) 
            {
                System.Console.WriteLine("Too high go lower!");
            }
            else if(userDieGuess < sum)
            {
                System.Console.WriteLine("Too low go higher!");
            }
        }
        public static int UserBetGil(ref int gil) //returns the balance of the gil when requested
        {
            return gil;
            
        }
        public static void RouletteRules() //rules of roulette display
        {
            System.Console.WriteLine("The game will choose a random number from 1-36" + 
            "\n1-10 even are black, 1-10 odd are red" + 
            "\n11-18 evem are red, 11-18 odd are black" + 
            "\n19-28 even are black, 19-28 odd are red" + 
            "\n29-36 even are red, 29-36 odd are black" +
            "\nYou have to guess a color red or black" + 
            "\nIf the correct color is guessed you double your bet" + 
            "\nIf the wrong color is guessed you lose your bet");
            System.Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        public static void RouletteWheel(ref int gamesWon, ref int gamesLost, ref int gamesPlayed, ref int gil, ref string userInput)
        {
            RouletteRules();
            gamesPlayed++;
            int [] red = new int []{1,3,5,7,9,12,14,16,18,19,21,23,25,27,30,32,34,36}; //arrays to store the numbers of each red number
            int [] black = new int []{2,4,6,8,10,11,13,15,17,20,22,24,26,28,29,31,33,35};//arrays to store the numbers of each black number

            Random rnd = new Random();

            int selection = rnd.Next(1,37); //random number between 1 and 36

            int gilBet = GilBet(ref gil); //method to check if bet exceeds gil balance
            
            if (gilBet > gil)
            {
                System.Console.WriteLine("Sorry bet exceeds gil amount currently stored..." + "\nReturning to Menu");
                System.Console.WriteLine("Press any key to return to menu");
                Console.ReadKey();
            }
            else
            {
                string userBet = RouletteBet(); //varaible to store what color user selects
                string finalDecision = RouletteDecision(red, black, selection); //tells user the color of random number
                BetMath(userBet, finalDecision, gilBet, ref gil, ref gamesWon, ref gamesLost); //how much user won
                PlayAgainDecision(ref gamesWon, ref gamesLost, ref gamesPlayed, ref gil, ref userInput);
            }

        }
        static int GilBet(ref int gil) //checks if bet exceeds gill balance
        {
            System.Console.WriteLine("You currently have " + gil + " gils");
            System.Console.WriteLine("How many gils would you like to bet");
            int gilBet = int.Parse(Console.ReadLine());

            return gilBet;
        }
        static string RouletteBet() //ask user what color it was
        {
            System.Console.WriteLine("The wheel has been spun what color did it land on?: red or black");
            string userBet = Console.ReadLine();
            return userBet;
        }
        static string RouletteDecision(int [] red, int [] black, int selection)
        {
            string finalDecision = " "; 
            System.Console.WriteLine("The random number is " + selection);
            foreach (int reds in red) //goes through every number in red array
            {
                if (selection == reds) //if the random number matches the red array
                {
                    System.Console.WriteLine("It landed on red");
                    finalDecision = selection.ToString("red");
                }
            }
            foreach (int blacks in black) //goes through every number in black array
            {
                if (selection == blacks) //checks if random number is in black array
                {
                    System.Console.WriteLine("It landed on black");
                    finalDecision = selection.ToString("black");

                }
            }
            return finalDecision; //returns what color it landed on

        }
        static void BetMath(string userBet, string finalDecision, int gilBet, ref int gil, ref int gamesWon, ref int gamesLost)
        {
            if (finalDecision == userBet.ToLower()) //checks if they won and win 2 times the amount 
            {
                gamesWon++;
                gil = gil + (gilBet);
                System.Console.WriteLine("You win! You made " + gilBet * 2 + " gils");
                System.Console.WriteLine("You have a total gil of " + gil);
                System.Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else if(finalDecision != userBet.ToLower()) //checks if they lost and tells them how much they lost
            {
                gamesLost++;
                gil = gil - gilBet;
                System.Console.WriteLine("You lose! You lost " + gilBet + " gils");
                System.Console.WriteLine("You have a total gil of " + gil);
                System.Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
        public static void BlackJack(ref int gamesWon, ref int gamesLost, ref int gamesPlayed, ref int gil, ref string userInput)
        {
            BlackJackRules(); 
            gamesPlayed++;
            int gilBet = GilBet(ref gil); //checks if user amount is bigger than gil balance
            if (gilBet > gil)
            {
                System.Console.WriteLine("Sorry bet exceeds gil amount currently stored..." + "\nReturning to Menu");
                System.Console.WriteLine("Press any key to return to menu");
                Console.ReadKey();
            }
            else
            {
                int userSum = UserTurn(); //variable to store total amount of user cards
                int cpuSum = CpuTurn();
                userSum = HitMe(ref userSum);
                WhoWins(userSum, cpuSum, ref gamesLost, ref gamesWon, ref gil, gilBet);
                PlayAgainDecision(ref gamesWon, ref gamesLost, ref gamesPlayed, ref gil, ref userInput);
            }
        }
        public static void BlackJackRules() //rules of blackjack display
        {
            System.Console.WriteLine("Welcome to BlackJack");
            System.Console.WriteLine("1: The objective is to get as close to 21 without going over");
            System.Console.WriteLine("2: You and the cpu will get two cards");
            System.Console.WriteLine("3: A random number from 1-11 will be generated for both cards");
            System.Console.WriteLine("4: You can see the CPU's second card");
            System.Console.WriteLine("5: You have the option to flip aces");
            System.Console.WriteLine("6: If you win, you double your bet");
            System.Console.WriteLine("7: If you lose, your bet is gone");
            System.Console.WriteLine("8: If you hit Black Jack you triple your bet");
            System.Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }
        public static int UserTurn()
        {
            Random rnd = new Random(); //random number generator for two cards
            int cardOne = rnd.Next(1, 11);
            int cardTwo = rnd.Next(1,11);
            System.Console.WriteLine("Your first card is " + cardOne);
            System.Console.WriteLine("Your second card is " + cardTwo);
            AceFlip(ref cardOne,ref cardTwo); //method to allow user to flip aces
            int sum = cardOne + cardTwo;
            System.Console.WriteLine("Your total number is " + sum);
            Thread.Sleep(1000);
            return sum;
        }
        public static void AceFlip(ref int cardOne,ref int cardTwo) //method to allow user to flip his ace
        {
            string userChoice = "";
            if (cardOne == 1) //if its a 1 they can change to 11
            {
                System.Console.WriteLine("Would you like to change your ace to 11? Y or N");
                userChoice = Console.ReadLine();
                if (userChoice.ToUpper() == "Y") //if user wants to change it changes the value to 11
                {
                    cardOne = 11;
                }
            }
            else if (cardOne == 11)
            {
                System.Console.WriteLine("Would you like to change your ace to 1? Y or N"); //allows user to change to 1 if its an 11
                userChoice = Console.ReadLine();
                if (userChoice.ToUpper() == "Y")
                {
                    cardOne = 1;
                }
            }
            if (cardTwo == 1) //card two 1 - 11
            {
                System.Console.WriteLine("Would you like to change your ace to 11? Y or N");
                userChoice = Console.ReadLine();
                if (userChoice.ToUpper() == "Y")
                {
                    cardTwo = 11;
                }
            }
            else if (cardTwo == 11) //card twp 11 -1
            {
                System.Console.WriteLine("Would you like to change your ace to 1? Y or N");
                userChoice = Console.ReadLine();
                if (userChoice.ToUpper() == "Y")
                {
                    cardTwo = 1;
                }
            }
        }
        public static int CpuTurn() //dealers random number generator method
        {
            Random rnd = new Random();
            int cardOne = rnd.Next(1, 11); //storing two of cpus random cards
            int cardTwo = rnd.Next(1,11);
            int sum = cardOne + cardTwo; 
            if (cardOne == 1 && sum <= 17) //tells cpu to flip ace if he is below 17
            {
                sum = sum - cardOne + 11;
            }
            else if(cardTwo == 1 && sum <=  17) //tells cpu to flip ace on second card if he is below 17
            {
                sum = sum - cardTwo + 11;
            }
            while (sum < 17) //cpu keeps taking a card till he hits 17 
            {
                CpuHitMe(ref sum);
            }
            System.Console.WriteLine("The cpu's second card is " + cardTwo);
            return sum;
        }
        public static void CpuHitMe(ref int sum)
        {
            Random rnd = new Random(); //random number generator to keep hitting cards until 17 is met
            int cpuHit = rnd.Next(1,11);
            if (cpuHit == 11 && sum > 21)
            {
                sum = sum - cpuHit + 1;
            }
            else if(cpuHit == 1 && sum + 11 > 21 )
            {
                sum = sum - cpuHit + 11;
            }
            sum = sum + cpuHit;
        }
        public static int HitMe(ref int userSum) //allows user to keep taking hits until he wants to stop or bust method
        {
            bool keepHitting = true; 
            System.Console.WriteLine("Would you like a hit or Stay?");
            string userChoice = Console.ReadLine();
            if (userChoice.ToLower() == "hit")
            {
                while(keepHitting == true) //keeps going until he bust or stop 
                {   
                    keepHitting = BustCheck(ref userSum, ref userChoice);
                }
            }
            return userSum;
        }
        public static bool BustCheck(ref int userSum, ref string userChoice)
        {
            bool keepHitting = true;
            while(keepHitting == true)
            {
                if(userSum <= 21 && keepHitting == true && userChoice.ToLower() == "hit")
                {
                    Random rnd = new Random(); //random number generator for cards after 2
                    int hit = rnd.Next(1,11);
                    AceFlip(ref hit); //checks if user has an ace and wants to flip it
                    userSum = userSum + hit; //update user sum 
                    System.Console.WriteLine("Card number is " + hit); 
                    System.Console.WriteLine("Your new total is " + userSum);
                    if (userSum <= 21) //allows user to hit again if he chooses as long as he doesnt bust
                    {
                        System.Console.WriteLine("Would you like a hit or Stay?");
                        userChoice = Console.ReadLine();
                    }
                    else
                    {
                        return keepHitting = false; //ends the while loop
                    }
                }
                else if(userSum > 21) //if he busst it ends the loop
                {
                    return keepHitting = false;
                }
                else //user doesnt have to hit
                {
                    return keepHitting = false; 
                }
            }
            return keepHitting; //return the variable
        }
        public static void AceFlip(ref int hit) //flips user ace if he decides too
        {
            string userChoice = ""; 
            if (hit == 1)
            {
                System.Console.WriteLine("Would you like to change your ace to 11? Y or N");
                userChoice = Console.ReadLine();
                if (userChoice.ToUpper() == "Y")
                {
                    hit = 11;
                }
            }
            else if(hit == 11)
            {
                System.Console.WriteLine("Would you like to change your ace to 1? Y or N");
                userChoice = Console.ReadLine();
                if (userChoice.ToUpper() == "Y")
                {
                    hit = 1;
                }
            }
        }
        public static void WhoWins(int userSum, int cpuSum, ref int gamesLost, ref int gamesWon, ref int gil, int gilBet) 
        {
            System.Console.WriteLine("Your total is " + userSum); 
            System.Console.WriteLine("The cpu total is " + cpuSum);
            Thread.Sleep(1000);

            if(userSum == 21 && cpuSum != 21) //user hits 21 and cpu does not
            {
                gamesWon++;
                gil = gil + (gilBet * 3);
                System.Console.WriteLine("You got BlackJack! You made " + gilBet * 3 + " gils"); //3 times the amount of bet is given and gil is updated
                System.Console.WriteLine("You have a total gil of " + gil);
                System.Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else if (userSum > cpuSum && userSum < 22) //they beat cpu and doubled their bet
            {
                gamesWon++;
                gil = gil + (gilBet * 2);
                System.Console.WriteLine("You win! You made " + gilBet * 2 + " gils"); 
                System.Console.WriteLine("You have a total gil of " + gil);
                System.Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else if(userSum == cpuSum || userSum > 21 && cpuSum > 21 ) //they both got the same card
            {
                System.Console.WriteLine("You tied!");
                System.Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            } 
            else if(userSum < cpuSum && cpuSum > 21)//double their bet if user card is bigger
            {
                gamesWon++;
                gil = gil + (gilBet * 2);
                System.Console.WriteLine("You win! You made " + gilBet * 2 + " gils");
                System.Console.WriteLine("You have a total gil of " + gil);
                System.Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else //user lost statement
            {
                gamesLost++;
                gil = gil - gilBet;
                System.Console.WriteLine("You lose! You lost " + gilBet + " gils");
                System.Console.WriteLine("You have a total gil of " + gil);
                System.Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
        public static void PlayAgainDecision(ref int gamesWon, ref int gamesLost, ref int gamesPlayed, ref int gil, ref string userInput)
        {
            if (UserBetGil(ref gil) <= 0 || UserBetGil(ref gil) >= 300) //ends the game if gil amount is 0
            {
                GameOver(ref gil);
            }
            else
            {
                System.Console.WriteLine("Would you like to play again? Y or N"); //allows user to play any game again based on userInput
                string userChoice = Console.ReadLine();
                
                if (userChoice.ToUpper() == "Y")
                {
                    if (userInput == "4")
                    {
                        BlackJack(ref gamesWon, ref gamesLost, ref gamesPlayed, ref gil, ref userInput); //passing all the reference variable to update leaderboard and balance
                    }
                    if (userInput == "3")
                    {
                        RouletteWheel(ref gamesWon, ref gamesLost, ref gamesPlayed, ref gil, ref userInput);
                    }
                    if(userInput == "2")
                    {
                        GilChecker(ref gamesWon, ref gamesLost, ref gamesPlayed, ref gil, ref userInput);
                    }
                    if (userInput == "1")
                    {
                        SlotMachine(ref gamesWon, ref gamesLost, ref gamesPlayed, ref gil, ref userInput);
                    }
                }
                else
                {
                    System.Console.WriteLine("Thanks for playing!");
                    System.Console.WriteLine("Press any key to return to menu");
                    Console.ReadKey();
                }
            }
        }
        public static void ScoreBoard(int gamesWon, int gamesLost, int gamesPlayed, int gil) //scoreboard to display user stats
        {
            Console.Clear();
            Console.WriteLine("You have won " + gamesWon + " games");
            Console.WriteLine("You have lost " + gamesLost + " games");
            Console.WriteLine("You have played " + gamesPlayed + " games");
            System.Console.WriteLine("You have " + gil + " gil's remaining");
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
        }
        public static void GameOver(ref int gil) //gameover method that tells user if he won or not
        {
            if (gil < 1)
            {
                System.Console.WriteLine("You lost all your gil. Game is now ending!");
                System.Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else if(gil >= 300)
            {
                System.Console.WriteLine("You won! Congratulations! Game is now ending");
                System.Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
        public static void GoodBye(int gamesWon, int gamesLost, int gamesPlayed, int gil) //final message when user decided to quit
        {
            Console.Clear();
            System.Console.WriteLine("Thank you for playing... \n" +
                "Press any key for one final look at the scoreboard" + 
                " before you go");
            Console.ReadKey();
            ScoreBoard(gamesWon, gamesLost, gamesPlayed, gil); //allows them to see scoreboard 1 more time
        }
       
        
    }
}

