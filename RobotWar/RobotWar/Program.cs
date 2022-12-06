using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotWar
{
    class Program
    {
        //Direction are stored in array based on their left and right 
        private static char[] directions = { 'N', 'E', 'S', 'W' };
        //X coordinate for Arena
        private static int upperRightX;
        //Y coordinate for Arena
        private static int upperRightY;
        static void Main(string[] args)
        {
            Console.WriteLine("Robot War");
            Console.WriteLine("=====================================================================================================");
            Console.WriteLine("Enter upper-right coordinates of the arena (Format: x_value<<Space>>y_value):");
            string upperRightCO = Console.ReadLine();
            //Spliting the Input string into array for X and Y Coordinates
            var upperRightXY = upperRightCO.Split(' ');
            //Converting String to Int
            Int32.TryParse(upperRightXY[0], out upperRightX);
            Int32.TryParse(upperRightXY[1], out upperRightY);
            //List Robots in Arena
            List<Robot> allRobots = new List<Robot>();
            char response = 'N';
            //Initilising Maximum no. of Moves with minimum Integer Value
            int maxMoves = Int32.MinValue;
            do
            {
                Console.WriteLine("=====================================================================================================");
                Console.WriteLine("Directions: (N, E, W, S)");
                Console.WriteLine("Enter Robot coordinates (Format: x_value<<Space>>y_value<<Space>>Direction):");
                string botCO= Console.ReadLine();
                //Coordinates and Direction of the Robots
                var botValue = botCO.Split(' ');
                Console.WriteLine("Please enter the robots moves as a collection of L M R:");
                string moves = Console.ReadLine();
                //Instruction for Robots

                //Updating the maximum no. of Moves
                if (maxMoves < moves.Length)
                    maxMoves = moves.Length;                
                Int32.TryParse(botValue[0], out int x);
                Int32.TryParse(botValue[1], out int y);
                Char.TryParse(botValue[2], out char c);

                //Validating the Robot Inputs
                bool flag = validateBotInput(x, y, c, moves);
                if (flag)
                {
                    //Creating object for Robot based on the Input
                    Robot bot = new Robot(x, y, c, moves);
                    allRobots.Add(bot);
                }
                else
                {
                    Console.WriteLine("Invalid Robot Inputs!!!");
                }
                Console.WriteLine("=====================================================================================================");
                Console.WriteLine("Do you want to add another Robot?(Y/N):");
                response = Console.ReadLine()[0]; 
                //Reponse to add another Robot to the Arena
            } while (response == 'Y' || response=='y');
            Console.WriteLine("=====================================================================================================");
            //Execute Series of Instruction
            StartGame(allRobots, maxMoves);
            Console.WriteLine("Output:");
            //Display Final Robots Location
            DisplayBotLocation(allRobots);
        }

        #region Data Helper

        private static bool validateBotInput(int x, int y, char c, string moves)
        {
            if (x > upperRightX || y > upperRightY)
                return false;
            if (!directions.Contains(c))
                return false;
            if (!moves.All(c => "LMR".Contains(c)))
                return false;
            return true;
        }

        private static void DisplayBotLocation(List<Robot> allRobots)
        {
            foreach (var bot in allRobots)
            {
                Console.WriteLine(bot.x + " " + bot.y + " " + bot.direction);
            }
        }

        #endregion
        private static void StartGame(List<Robot> allRobots, int maxMoves)
        {
            int move = 0;
            while (maxMoves > 0)
            {
                foreach(Robot bot in allRobots)
                {
                    if (bot.moves.Length>move) {
                        switch (bot.moves[move])
                        {
                            case 'L':
                                TurnLeft(bot);
                                break;
                            case 'R':
                                TurnRight(bot);
                                break;
                            case 'M':
                                MoveForward(bot);
                                break;
                        }
                    }                   
                }
                maxMoves--;
                move++;
                Console.WriteLine("Position of Robots after Step " + move);
                DisplayBotLocation(allRobots);
            }
            Console.WriteLine("=====================================================================================================");
        }

        #region Robot Navigator Helper
        private static void MoveForward(Robot bot)
        {
            if (bot.direction == 'N' && bot.y + 1 <= upperRightY)
                bot.y++;
            else if (bot.direction == 'E' && bot.x + 1 <= upperRightX)
                bot.x++;
            else if (bot.direction == 'W' && bot.x - 1 >= 0)
                bot.x--;
            else if (bot.direction == 'S' && bot.y - 1 >= 0)
                bot.y--;
        }

        private static void TurnRight(Robot bot)
        {
            int index = Array.IndexOf(directions, bot.direction);
            if (index + 1 > 3)
                index = 0;
            else
                index++;

            bot.direction = directions[index];
        }

        private static void TurnLeft(Robot bot)
        {
            int index = Array.IndexOf(directions, bot.direction);
            if (index - 1 < 0)
                index = 3;
            else
                index--;
            bot.direction = directions[index];
        } 
        #endregion
    }
}
