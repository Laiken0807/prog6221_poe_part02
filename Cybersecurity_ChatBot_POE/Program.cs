using System;
using System.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cybersecurity
{
    internal class Program
    {
        static void Main()
        {
            // play audio greeting
            PlayGreetingAudio("cyber_bot.wav");

            Console.Title = "Cybersecurity Awareness Assistant Chatbox";
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(@" 


_________        ___.               __________        __   
\_   ___ \___.__.\_ |__   __________\______   \ _____/  |_ 
/    \  \<   |  | | __ \_/ __ \_  __ \    |  _//  _ \   __\
\     \___\___  | | \_\ \  ___/|  | \/    |   (  <_> )  |  
 \______  / ____| |___  /\___  >__|  |______  /\____/|__|  
        \/\/          \/     \/             \/             

");

            Console.Write("What is your name?");
            Console.ForegroundColor = ConsoleColor.White;

            string userName = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine($"Hello,{userName}! I'm your Cybersecurity Assistant.");
            Console.WriteLine("You can ask me about password safety, phishing, malware, safe browsing or you can type 'exit' to quit.\n");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write($"{userName}: ");

                string userInput = Console.ReadLine()?.ToLower().Trim();

                if (string.IsNullOrEmpty(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("Chatbox: Please can you enter a valid question.");
                    Console.WriteLine();

                    continue;
                }
                if (userInput == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("Chatbox: Thank you for using CyberBot! Stay safe online, bye!");
                    Console.WriteLine();

                    break;
                }

                
            }
        }
        static void PlayGreetingAudio(string filePath)
        {
            try
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                if (File.Exists(fullPath))
                {
                    SoundPlayer player = new SoundPlayer(fullPath);
                    player.PlaySync(); // Play audio synchronously
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {filePath} was not found at the specified location");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error playing audio: {ex.Message}");
            }
        }



    }
}


