using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Talent Show
    // Description: The finch does a couple talents
    // Application Type: C#.net framework
    // Author: Joshua Paul
    // Dated Created: 10/2/19
    // Last Modified: 10/7/19
    //
    // **************************************************
    //*********************************************************************
    // Finch Commands
    // finchRobot.setMotors(255, 255);
    // finchRobot.wait();
    // finchRobot.setLED(0 , 0 , 0 );
    // finchRobot.noteOn(800);

//\x00B0 F 
    class Program
    {
        public enum Command
        {
            NONE,
            MOVEFORWARD,
            MOVEBACKWARD,
            STOPMOTORS,
            WAIT,
            TURNRIGHT,
            TURNLEFT,
            LEDON,
            LEDOFF,
            DONE
        }
        static void Main(string[] args)
        {
            //DisplayLogin();
            SetTheme();
            DisplayWelcomeScreen();

            DisplayMainMenu();

            DisplayClosingScreen();
        }

       // private static void DisplayLogin()
       // {
           // bool valid = false;
            //DisplayScreenHeader("Please Login or Register");
            //Console.WriteLine();
            //Console.Write("1) Login");
            //Console.Write("2) Register");
            //Console.Write("3) Quit");

            
            
           // do
            //{
                //string input = Console.ReadLine();            
                //switch (input)
               // {
                    //case "1":
                      //  DisplayLoginScreen();
                       // valid = true;
                       // break;
                    //case "2":
                       // DislplayRegisterScreen();
                       // valid = true;
                       // break;
                   // default:
                       // Console.WriteLine("Please Enter a Valid Response");
                        //valid = false;
                       // break;
               // }



            //} while (!valid);
            
            
       // }

       // private static void DisplayLoginScreen()
       // {
           // string inputPassword;
            //string inputUserName;
           // DisplayScreenHeader("Login");
           // Console.WriteLine();
           // Console.WriteLine("UserName: ");
           // inputUserName = Console.ReadLine();
           // Console.WriteLine("Password: ");
           // inputPassword = Console.ReadLine();
           // Tuple<string, string> username = new Tuple<string, string>(inputUserName, inputPassword);
            
       // }

        static void SetTheme()
        {
            string dataPath = @"Data\Theme.txt";
            string foregroundColorString;
            ConsoleColor foregroundColor;

            foregroundColorString = File.ReadAllText(dataPath);

            Enum.TryParse(foregroundColorString, out foregroundColor);

            Console.ForegroundColor = foregroundColor;

        }

        static void DisplayMainMenu()
        {
            //instantiate a Finch object
            Finch finchRobot = new Finch();
            bool finchRobotConnected = false;
            bool quitApplication = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Main Menu");

                //get menu choice from user
                Console.WriteLine("A) Connect Finch Robot");
                Console.WriteLine("B) Talent Show");
                Console.WriteLine("C) Data Recorder");
                Console.WriteLine("D) Alarm System");
                Console.WriteLine("E) User Programming");
                Console.WriteLine("F) Disconnect Finch Robot");
                Console.WriteLine("Q) Quit");
                Console.Write("Enter Choice: ");
                menuChoice = Console.ReadLine().ToUpper();

                //process menu choice
                switch (menuChoice)
                {
                    case "A":
                            finchRobotConnected = DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "B":
                       
                            DisplayTalentShow(finchRobot);
                        break;

                    case "C":
                            DisplayDataRecorder(finchRobot);
                       
                        break;

                    case "D":
                            DisplayAlarmSystem(finchRobot);
                        break;

                    case "E":
                        DisplayUserProgramming(finchRobot);
                        finchRobotConnected = false;

                        break;                        

                    case "H":
                        DisplayDisconnectFinchRobot(finchRobot);
                        finchRobotConnected = false;

                        break;

                    case "Q":
                            quitApplication = true;
                        
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\t*********************************");
                        Console.WriteLine("\tPlease enter a valid menu choice");
                        Console.WriteLine("\t*********************************");
                        DisplayContinuePrompt();
                        break;
                        
                }

            } while (!quitApplication);
        }
        #region User Programming
        static List<Command> DisplayReadUserProgrammingData()
        {
            bool valid = false;
            string dataPath = @"Data\Data.txt";
            List<Command> commands = new List<Command>();
            string[] commandsString;

            DisplayScreenHeader("Read Commands from Data File");

            Console.WriteLine("Ready to read commands from the data file.");
            Console.WriteLine();
            

            commandsString = File.ReadAllLines(dataPath);

            Command command;
            foreach (string commandString in commandsString)
            {
                Enum.TryParse(commandString, out command);
                commands.Add(command);
                valid = true;
            }
            foreach (Command commando in commands)
            {
                Console.WriteLine(commando);
            }



            DisplayContinuePrompt();
            if (valid)
            {
                Console.WriteLine("Commands read from data file complete");
            }
            if (!valid)
            {
                Console.WriteLine("did not read");                
            }
            DisplayContinuePrompt();
            return commands;            
        }

        static void DisplayWriteuserProgrammingData(List<Command> commands)
        {
            string dataPath = @"Data\Data.txt";
            List<string> commandsString = new List<string>();
            DisplayScreenHeader("Write Commands to Data File");

            foreach  (Command command in commands)
            {
                commandsString.Add(command.ToString());
            }

            Console.WriteLine("ready to write to the data file.");
            DisplayContinuePrompt();

            File.WriteAllLines(dataPath, commandsString.ToArray());

            Console.WriteLine();
            Console.WriteLine("Commands written to the data file.");

            DisplayContinuePrompt();
        }
        
        static void DisplayUserProgramming(Finch finchRobot)
        {
            string menuChoice;
            bool quitApplication = false;

            (int motorSpeed, int ledBrightness, int waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            List<Command> commands = new List<Command>();

            do
            {
                DisplayScreenHeader("Main Menu");

                //get menu choice from user
                Console.WriteLine("A) Set Commmand Parameters");
                Console.WriteLine("B) Add Commands");
                Console.WriteLine("C) View Commands");
                Console.WriteLine("D) Execute Commands");
                Console.WriteLine("E) Save Commands to Text File");
                Console.WriteLine("F) Load Commands to Text File");
                Console.WriteLine("Q) Quit");
                Console.Write("Enter Choice: ");
                menuChoice = Console.ReadLine().ToUpper();

                //process menu choice
                switch (menuChoice)
                {
                    case "A":
                        commandParameters = DisplayGetCommandParameters();
                        break;

                    case "B":
                        DisplayGetFinchCommand(commands);
                        break;

                    case "C":
                        DisplayFinchCommands(commands);
                        break;

                    case "D":
                        DisplayExecuteCommands(finchRobot, commands, commandParameters);
                        break;

                    case "E":
                        DisplayWriteuserProgrammingData(commands);
                        break;

                    case "F":
                        commands = DisplayReadUserProgrammingData();
                        break;

                    case "Q":
                        quitApplication = true;

                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\t*********************************");
                        Console.WriteLine("\tPlease enter a valid menu choice");
                        Console.WriteLine("\t*********************************");
                        DisplayContinuePrompt();
                        break;

                }

            } while (!quitApplication);
        }

        static void DisplayExecuteCommands(
            Finch finchRobot, 
            List<Command> commands,
            (int motorSpeed, int ledBrightness, int waitSeconds) commandParameters)
        {
            int motorSpeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            int waitMilliSeconds = commandParameters.waitSeconds * 1000;

            DisplayScreenHeader("Execute Finch Commands");

            // info and pause

            foreach (Command command in commands)
            {
                switch (command)
                {
                    case Command.NONE:

                        break;
                    case Command.MOVEFORWARD:
                        finchRobot.setMotors(motorSpeed, motorSpeed);
                        break;
                    case Command.MOVEBACKWARD:
                        finchRobot.setMotors(-motorSpeed, -motorSpeed);
                        break;
                    case Command.STOPMOTORS:
                        finchRobot.setMotors(0, 0);
                        break;
                    case Command.WAIT:
                        finchRobot.wait(waitMilliSeconds);
                        break;
                    case Command.TURNRIGHT:
                        finchRobot.setMotors(motorSpeed, 0);
                        break;
                    case Command.TURNLEFT:
                        finchRobot.setMotors(0, motorSpeed);
                        break;
                    case Command.LEDON:
                        finchRobot.setLED(ledBrightness, 0, 0);
                        break;
                    case Command.LEDOFF:
                        finchRobot.setLED(0, 0, 0);
                        break;
                    case Command.DONE:
                        
                        break;
                    default:
                        break;
                }

            }

            DisplayContinuePrompt();
        }

        static void DisplayFinchCommands(List<Command> commands)
        {
            DisplayScreenHeader("Display Finch Commands");
            foreach (Command command in commands)
            {
                Console.WriteLine(command);
            }

            DisplayContinuePrompt();
        }

        static void DisplayGetFinchCommand(List<Command> commands)
        {
            DisplayScreenHeader("Finch Robot Commands");

            Command command =Command.NONE;
            string userResponse;
            Console.WriteLine("Comand list: MOVEFORWARD, MOVEBACKWARD, STOPMOTORS, WAIT, TURNRIGHT");
            Console.WriteLine("            TURNLEFT, LEDON, LEDOFF, DONE");
            while (command != Command.DONE)
            {
                Console.Write("Enter Command:");
                userResponse = Console.ReadLine().ToUpper();
                Enum.TryParse(userResponse, out command);

                commands.Add(command);
            }

            
            DisplayContinuePrompt();
        }

        static (int motorSpeed, int ledBrightness, int waitSeconds) DisplayGetCommandParameters()
        {
            (int motorSpeed, int ledBrightness, int waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            DisplayScreenHeader("Command Parameters");

            commandParameters.motorSpeed = DisplayGetMotorSpeed();
            commandParameters.ledBrightness = DisplayGetLedBrigthness();
            commandParameters.waitSeconds = DisplaySecondsToWait();  

            DisplayContinuePrompt();

            return commandParameters;
        }

        static int DisplaySecondsToWait()
        {        
            int wait;
            bool valid = false;
            // validate user input!!
            do
            {
                Console.Write("Enter Seconds to Wait [1 - 25]:");

                string input = Console.ReadLine();
                if (!Int32.TryParse(input, out wait))
                {
                    Console.WriteLine("Please choose a number");
                    valid = true;
                }
                if (wait > 25)
                {
                    wait = 25;
                    valid = true;
                }
                if (wait < 1)
                {
                    wait = 1;
                    valid = true;
                }
                else
                {
                    valid = false;
                }

            } while (valid);
            return wait;
        }

        static int DisplayGetMotorSpeed()
        {
            int Speed;

            bool valid = false;
            
            do
            {
                Console.Write("Enter Motor Speed [1 - 255]:");
                
                string input = Console.ReadLine();
                if (!Int32.TryParse(input, out Speed))
                {
                    Console.WriteLine("Please choose a number");
                    valid = true;
                }
                if (Speed > 255)
                {
                    Speed = 255;
                    valid = true;
                }
                if (Speed < 1)
                {
                    Speed = 1;
                    valid = true;
                }
                else
                {
                    valid = false;
                }

            } while (valid);
            return Speed;
        }

        static int DisplayGetLedBrigthness()
        {
            int brightness;
            bool valid = false;
            // validate user input!!
            do
            {
                Console.Write("Enter LED Brightness [1 - 255]:");

                string input = Console.ReadLine();
                if (!Int32.TryParse(input, out brightness))
                {
                    Console.WriteLine("Please choose a number");
                    valid = true;
                }
                if (brightness > 255)
                {
                    brightness = 255;
                    valid = true;
                }
                if (brightness < 1)
                {
                    brightness = 1;
                    valid = true;
                }
                else
                {
                    valid = false;
                }

            } while (valid);
            return brightness;
        }
        #endregion
        #region Alarm System
        private static void DisplayAlarmSystem(Finch finchRobot)
        {
            //validate user input
            string alarmType;
            int maxSeconds;
            double threshold;
            bool thresholdExceeded;

            DisplayScreenHeader("Alarm System");

            alarmType = DisplayGetAlarmType();
            maxSeconds = DisplayGetMaxSeconds();
            threshold = DisplayGetThreshold(finchRobot, alarmType);

            // warn user and pause
            if (alarmType == "light")
            {
                thresholdExceeded = MonitorCurrentLightLevel(finchRobot, threshold, maxSeconds);

                if (thresholdExceeded)
                {
                    if (alarmType == "light")
                    {
                        Console.WriteLine("Maximum Light Level Exceeded");
                        finchRobot.noteOn(1000);
                        finchRobot.wait(1000);
                        finchRobot.noteOff();
                    }
                    else
                    {
                        Console.WriteLine("Maximum Light Exceeded");
                    }
                }
                else
                {
                    Console.WriteLine("Maximum Monitoring Time Exceeded");
                }
            }
            if (alarmType == "temperature")
            {
                thresholdExceeded = MonitorCurrentTemperatureLevel(finchRobot, threshold, maxSeconds);

                if (thresholdExceeded)
                {
                    if (alarmType == "temperature")
                    {
                        Console.WriteLine("Maximum Temperature Level Exceeded");
                        finchRobot.noteOn(1000);
                        finchRobot.wait(1000);
                        finchRobot.noteOff();
                    }
                    else
                    {
                        Console.WriteLine("Maximum Temperature Exceeded");
                    }
                }
                else
                {
                    Console.WriteLine("Maximum Monitoring Time Exceeded");
                }
            }

            DisplayMainMenuPrompt();
        }

        static bool MonitorCurrentTemperatureLevel(Finch finchRobot, double threshold, int maxSeconds)
        {
            bool thresholdExceeded = false;
            double currentTemperatureLevel;
            double seconds = 0;

            while (!thresholdExceeded && seconds <= maxSeconds)
            {
                currentTemperatureLevel = finchRobot.getTemperature();

                DisplayScreenHeader("Monitoring Temp Levels");

                Console.WriteLine($"Maximum Temperature Level: {threshold}");
                Console.WriteLine($"Current Temperature Level: {currentTemperatureLevel}");

                if (currentTemperatureLevel > threshold)
                {
                    thresholdExceeded = true;
                }

                finchRobot.wait(500);
                seconds += 0.5;
            }


            return thresholdExceeded;
        }
        static bool MonitorCurrentLightLevel(Finch finchRobot, double threshold, int maxSeconds)
        {
            bool thresholdExceeded = false;
            int currentLightLevel;
            double seconds = 0;

            while(!thresholdExceeded && seconds <= maxSeconds)
            {
                currentLightLevel = finchRobot.getLeftLightSensor();

                DisplayScreenHeader("Monitoring Light Levels");

                Console.WriteLine($"Maximum Light Level: {threshold}");
                Console.WriteLine($"Current Light Level: {currentLightLevel}");

                if (currentLightLevel > threshold)
                {
                    thresholdExceeded = true;
                }

                finchRobot.wait(500);
                seconds += 0.5;
            }


            return thresholdExceeded;
        }

        static double DisplayGetThreshold(Finch finchRobot, string alarmType)
        {
            double threshold = 0;
            bool valid = false;
            string input;

            DisplayScreenHeader("Threshold Value");
            
            
            switch (alarmType)
            {
                case "light":

                    do
                    {                         
                        Console.WriteLine($"Current Light Level {finchRobot.getLeftLightSensor()}");
                        Console.WriteLine("Enter Maximum Light Level [0 - 255]:");
                        input = Console.ReadLine();                        
                        if (!double.TryParse(input, out threshold) || threshold > 255 || threshold < 1)
                        {
                            Console.WriteLine("Please choose a number above 0 and below 255");
                            valid = false;
                        }
                        else
                        {
                            valid = true;
                        }

                    } while (!valid);
                    break;

                case "temperature":
                    do {                     
                    Console.WriteLine($"Current Temperature {finchRobot.getTemperature()}");
                    Console.WriteLine("Enter Maximum Temperature [0 - 110]:");
                    input = Console.ReadLine();
                    if (!double.TryParse(input, out threshold) || threshold > 110 || threshold < 0)
                    {
                        Console.WriteLine("Please choose a temperature above 0 and below 255");
                        valid = false;
                    }
                    else
                    {
                        valid = true;
                    }

                    } while (!valid) ;
                    break;
            

                default:
                    throw new FormatException();
                    break;
            }

            

            DisplayContinuePrompt();
            return threshold;
        }

        static int DisplayGetMaxSeconds()
        {
            int maxSeconds;
            bool valid = false;
            // validate user input!!
            do
            {
                Console.WriteLine("Seconds To Monitor:");
                string input = Console.ReadLine();
                if (!Int32.TryParse(input, out maxSeconds))
                {
                        Console.WriteLine("Please choose a number");
                    valid = false;
                }
                else
                {
                    valid = true;
                }

            } while (!valid);
            return maxSeconds;
        }

        static string DisplayGetAlarmType()
        {
            bool valid = false;
            string alarmType;

            do
            {
                Console.Write("Alarm Type [light or temperature]");
                alarmType = Console.ReadLine().ToLower();
                switch (alarmType)
                {
                    case "light":
                        alarmType = "light";
                        valid = true;
                        break;
                    case "temperature":
                        alarmType = "temperature";
                        valid = true;
                        break;
                    default:
                        Console.WriteLine("please enter either light or temerature");
                        break;
                }
            } while (!valid);
                return alarmType;
        }
        #endregion
        #region Data Recorder
        /// <summary>
        /// DisplayDataRecorder
        /// </summary>
        /// <param name="finchRobot"></param>
        private static void DisplayDataRecorder(Finch finchRobot)
        {
            DisplayScreenHeader("Data Recorder");

            double dataPointFrequency;
            int numberOfDataPoints;
            bool fahrenheit;
            string dataType;

            // tell user what is going to happen

            dataPointFrequency = DisplayGetDataPointFrequency();
            numberOfDataPoints = DisplayGetNumberOfDatePoints();
            dataType = DisplayGetDataType();

            //
            // instantiate (create) the array
            //
            double[] temperatures = new double[numberOfDataPoints];

            DisplayGetData(numberOfDataPoints, dataPointFrequency, temperatures, finchRobot);

            fahrenheit = DisplayFahrenheitPrompt();

            if (fahrenheit == true)
            {
                DisplayDataFahrenheit(temperatures);
            }
            if (fahrenheit == false)
            {
                DisplayData(temperatures);
            }
            

            DisplayMainMenuPrompt();
        }

        private static string DisplayGetDataType()
        {
            string dataType;
            
                bool valid = false;

                do
                {
                Console.WriteLine("Monitor Temperature or Light?");
                dataType = Console.ReadLine().ToLower();                   
                    if (dataType == "temperature")
                    {
                        valid = false;
                    }
                    if (dataType == "light")
                    {                        
                        valid = false;
                    }
                    else
                    {
                    valid = true;
                    Console.WriteLine("Enter either light or temperature");                    
                    }
                } while (valid);
                return dataType;
            }

        /// <summary>
        /// Display Data
        /// </summary>
        /// <param name="temperatures"></param>
        static void DisplayData(double[] temperatures)
        {
            DisplayScreenHeader("Temperature Date");

            for (int index = 0; index < temperatures.Length; index++)
            {
                Console.WriteLine($"Temperature {index + 1}: {temperatures[index]}");
            }

            DisplayContinuePrompt();
        }
        /// <summary>
        /// Display Fahrenheit
        /// </summary>
        /// <param name="temperatures"></param>
        static void DisplayDataFahrenheit(double[] temperatures)
        {
            DisplayScreenHeader("Temperature Data");

            for (int index = 0; index < temperatures.Length; index++)
            {
                Console.WriteLine("The following Temperatures were taken:");
                Console.WriteLine($"Temperature #{index + 1}: {(temperatures[index] * 1.8) + 32}");
            }

            DisplayContinuePrompt();
        }
        /// <summary>
        /// DisplayFahrenheitPrompt
        /// </summary>
        /// <param name=""></param>
        static bool DisplayFahrenheitPrompt()
        {
            DisplayScreenHeader("Data in Fahrenheit");
            bool fahrenheit = false;
            string userResponse;
            Console.WriteLine("Would you like to convert data to Farhenheit? yes/no");

            userResponse = Console.ReadLine().ToLower();
            switch (userResponse)
            {
                case "yes":
                    fahrenheit = true;
                    break;
                case "no":
                    fahrenheit = false;
                    break;
                default:
                    fahrenheit = false;
                    break;
            }

            return fahrenheit;
            
        }
        /// <summary>
        /// DisplayGetData
        /// </summary>
        /// <param name="numberOfDataPoints"></param>
        /// <param name="dataPointFrequency"></param>
        /// <param name="temperatures"></param>
        /// <param name="finchRobot"></param>
        static void DisplayGetData(
            int numberOfDataPoints,
            double dataPointFrequency,
            double[] temperatures,
            Finch finchRobot)
        {
            DisplayScreenHeader("Get Temperature Data");

            // provide the user info and prompt

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                //
                // Record Data
                //

                temperatures[index] = finchRobot.getTemperature();
                int milliseconds = (int)(dataPointFrequency * 1000);
                finchRobot.wait(milliseconds);

                //
                // echo data
                //
                Console.WriteLine($"Temperature {index + 1}: {temperatures[index]}");
                

            }
            DisplayContinuePrompt();
        }
        
        /// <summary>
        /// Display Number of Data Points
        /// </summary>
        /// <returns></returns>
        static int DisplayGetNumberOfDatePoints()
        {
            int numberOfDataPoints;

            DisplayScreenHeader("Number of Data Points");
            Console.WriteLine("Enter Number of Data Points");
            int.TryParse(Console.ReadLine(), out numberOfDataPoints);

            DisplayContinuePrompt();
            return numberOfDataPoints;
        }
        /// <summary>
        /// Display Get Data Frequency
        /// </summary>
        /// <returns></returns>
        static double DisplayGetDataPointFrequency()
        {
            double dataPointFrequency;
            //string userResponse;

            DisplayScreenHeader("Data Point Frequency");

            Console.WriteLine("Enter Data Point Frequency:");
            //userResponse = Console.ReadLine();
            //double.TryParse(userResponse, out dataPointFrequency);
            double.TryParse(Console.ReadLine(), out dataPointFrequency);

            DisplayContinuePrompt();

            return dataPointFrequency;
        }
        #endregion
        #region Talent Show
        /// <summary>
        /// display finch talent show
        /// </summary>
        static void DisplayTalentShow(Finch finchRobot)
        {
            DisplayScreenHeader("Talent Show");
            talentShowMenu(finchRobot);


        }
        static void talentShowMenu(Finch finchRobot)
        {
       
            bool goBack;
            goBack = false;
            do 
            {

                DisplayScreenHeader("Talent Options");
                string userResponse;
                Console.WriteLine("Finch robot is ready to show its talent.");
                Console.WriteLine();
                Console.WriteLine("Please pick a talent for the finch");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("a) Do a Slolum");
                Console.WriteLine("b) Sing a Song");
                Console.WriteLine("c) Do a backflip while doing taxes");
                Console.WriteLine("d) Back to Main");
                userResponse = Console.ReadLine().ToLower();
                switch (userResponse)
                {
                    case "a":
                        slolumnTrick(finchRobot);
                        break;
                    case "b":
                        singSong(finchRobot);
                        break;
                    case "c":
                        backflipMethod(finchRobot);
                        break;
                    case "d":
                        goBack = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("\t*********************************");
                        Console.WriteLine("\tPlease enter a valid menu choice");
                        Console.WriteLine("\t*********************************");
                        DisplayContinuePrompt();
                        break;
            
                }
            } while (!goBack);

        }
        /// <summary>
        /// Future projects
        /// </summary>
        /// <param name="finchRobot"></param>
        private static void comingSoon(Finch finchRobot)
        {
            Console.Clear();
            Console.WriteLine("More options coming soon");
            DisplayContinuePrompt();
        }
        /// <summary>
        /// Back Flip Mode
        /// </summary>
        /// <param name="finchRobot"></param>
        private static void backflipMethod(Finch finchRobot)
        {
            Console.WriteLine();
            Console.WriteLine("Taxes and back flips are still in production, check back soon");
            DisplayContinuePrompt();
        }
        /// <summary>
        /// Finch Song
        /// </summary>
        /// <param name="finchRobot"></param>
        private static void singSong(Finch finchRobot)
        {
            // B 987
            // G 784
            // A 880
            // D 1174
            // 1200 whole note
            // 600 half note
            // 300 quarter
            // 150 eigth
            // B = finchRobot.setLED(0, 0, 255);
            // G = finchRobot.setLED(255, 0, 0);
            // A = finchRobot.setLED(0, 255, 0);
            // D = finchRobot.setLED(255, 255, 255);
            // off = finchRobot.setLED(0, 0, 0);

            finchRobot.noteOn(987);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(450);
            finchRobot.noteOn(880);
            finchRobot.setLED(0, 255, 0);
            finchRobot.wait(150);
            finchRobot.noteOn(784);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.noteOn(880);
            finchRobot.setLED(0, 255, 0);
            finchRobot.wait(300);
            //whole
            finchRobot.noteOn(987);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(150);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(150);
            finchRobot.noteOn(987);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(150);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(150);
            finchRobot.noteOn(987);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(150);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(450);
            //whole
            finchRobot.noteOn(880);
            finchRobot.setLED(0, 255, 0);
            finchRobot.wait(150);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(150);
            finchRobot.noteOn(880);
            finchRobot.setLED(0, 255, 0);
            finchRobot.wait(150);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(150);
            finchRobot.noteOn(880);
            finchRobot.setLED(0, 255, 0);
            finchRobot.wait(150);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(450);
            //whole
            finchRobot.noteOn(987);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(150);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(150);
            finchRobot.noteOn(1174);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(150);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(150);
            finchRobot.noteOn(1174);
            finchRobot.setLED(255, 255, 255);
            finchRobot.wait(150);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(450);
            //whole
            finchRobot.noteOn(987);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(450);
            finchRobot.noteOn(880);
            finchRobot.setLED(0, 255, 0);
            finchRobot.wait(150);
            finchRobot.noteOn(784);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(300);
            finchRobot.noteOn(880);
            finchRobot.setLED(0, 255, 0);
            finchRobot.wait(300);
            //whole
            finchRobot.noteOn(987);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(150);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(150);
            finchRobot.noteOn(987);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(150);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(150);
            finchRobot.noteOn(987);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(150);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(150);
            finchRobot.noteOn(987);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(150);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(150);
            //whole
            finchRobot.noteOn(880);
            finchRobot.setLED(0, 255, 0);
            finchRobot.wait(150);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(150);
            finchRobot.noteOn(880);
            finchRobot.setLED(0, 255, 0);
            finchRobot.wait(150);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(150);
            finchRobot.noteOn(987);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(150);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(150);
            finchRobot.noteOn(880);
            finchRobot.setLED(0, 255, 0);
            finchRobot.wait(150);
            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            finchRobot.wait(150);
            //whole
            finchRobot.noteOn(784);
            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(1200);
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();

            DisplayContinuePrompt();

        }
        /// <summary>
        /// Slolum
        /// </summary>
        /// <param name="finchRobot"></param>
        private static void slolumnTrick(Finch finchRobot)
        {
            DisplayScreenHeader("Slolum Time!");
            finchRobot.noteOn(500);
            finchRobot.setMotors(20, 75);
            finchRobot.wait(1000);
            finchRobot.noteOn(700);
            finchRobot.setMotors(100, 100);
            finchRobot.wait(2000);
            finchRobot.noteOn(500);
            finchRobot.setMotors(75, 20);
            finchRobot.wait(1000);
            finchRobot.noteOn(700);
            finchRobot.setMotors(100, 100);
            finchRobot.wait(2000);
            finchRobot.noteOn(500);
            finchRobot.setMotors(20, 75);
            finchRobot.wait(1000);
            finchRobot.noteOn(700);
            finchRobot.setMotors(100, 100);
            finchRobot.wait(2000);
            finchRobot.noteOn(500);
            finchRobot.setMotors(75, 20);
            finchRobot.wait(1000);
            finchRobot.noteOn(700);
            finchRobot.setMotors(100, 100);
            finchRobot.wait(2000);
            finchRobot.noteOff();
            finchRobot.setMotors(0, 0);

            DisplayContinuePrompt();
        }
        #endregion
        #region Menu Items
        /// <summary>
        /// disconnect finch robot from application
        /// </summary>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("Are you sure you want to disconnect Finch robot? Type 'yes' to confirm.");
            string userInput = Console.ReadLine().ToLower();
           

            if (userInput == "yes")
            {
                
                finchRobot.setLED(0, 0, 0);
                finchRobot.noteOn(200);
                finchRobot.wait(1000);
                finchRobot.noteOff();
                finchRobot.disConnect();
                Console.WriteLine();
                Console.WriteLine("Finch Robot is now disconnected.");
                
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Finch robot not disconnected properly. Returning to main menu.");
            }
            
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Connect finch robot to application
        /// </summary>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            bool finchRobotConnected;

            DisplayScreenHeader("Connect to Finch Robot");

            Console.WriteLine("Ready to connect to Finch robot. Be sure to connect the USB cable to the robot and the computer.");
            DisplayContinuePrompt();

            finchRobotConnected = finchRobot.connect();

            if (finchRobotConnected)
            {
                finchRobot.setLED(0, 255, 0);
                finchRobot.noteOn(15000);
                finchRobot.wait(1000);
                finchRobot.noteOff();
                Console.WriteLine();
                Console.WriteLine("Finch robot is now connected.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Unable to connect to the Finch robot.");
            }

            DisplayContinuePrompt();

            return finchRobotConnected;

        }

        /// <summary>
        /// display welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }
        #endregion 
        #region HELPER METHODS

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        static void DisplayMainMenuPrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to return to Main Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
