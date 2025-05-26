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
        static Random rnd = new Random();

        // Memory variables
        static string userName = "";
        static string favoriteTopic = "";
        static string lastTopic = "";

        static List<string> passwordResponses = new List<string>
        {
            "You can create a strong password by using a mix of letters (capital and lowercase), numbers, and symbols.",
            "Avoid using personal information in passwords and make them at least 12 characters long.",
            "Consider using a password manager to generate and store strong passwords securely."
        };

        static List<string> phishingResponses = new List<string>
        {
            "Phishing attacks trick you into giving personal info by pretending to be trusted sources.",
            "Never click suspicious links or share your credentials to avoid phishing scams.",
            "Be cautious of unexpected emails asking for sensitive information."
        };

        static List<string> malwareResponses = new List<string>
        {
            "Malware is malicious software like viruses, spyware, and ransomware.",
            "Keep your systems updated and use antivirus software to protect yourself.",
            "Avoid downloading suspicious files or clicking unknown links to prevent malware infections."
        };

        static List<string> browsingResponses = new List<string>
        {
            "Practice safe browsing by using secure websites with HTTPS.",
            "Avoid clicking on suspicious links and keep your browser updated.",
            "Don’t download anything from untrusted websites as it could contain malware."
        };

        static List<string> purposeResponses = new List<string>
        {
            "My purpose is to provide you with information and tips on cybersecurity.",
            "I'm here to help you stay safe online and answer your cybersecurity questions."
        };

        static List<string> askResponses = new List<string>
        {
            "You can ask me about password safety, phishing, malware, and safe browsing.",
            "Feel free to ask any questions related to cybersecurity topics."
        };

        static List<string> feelingResponses = new List<string>
        {
            "I'm just a program, but I'm always ready to help!",
            "Thanks for asking! Let's keep you safe online."
        };

        static void Main()
        {
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

            Console.Write("What is your name? ");
            Console.ForegroundColor = ConsoleColor.White;
            userName = Console.ReadLine()?.Trim() ?? "User";
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine($"Hello, {userName}! I'm your Cybersecurity Assistant.");
            Console.WriteLine("You can ask me about password safety, phishing, malware, safe browsing or type 'exit' to quit.\n");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{userName}: ");
                string userInput = Console.ReadLine()?.ToLower().Trim();

                if (string.IsNullOrEmpty(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Chatbox: Please enter a valid question.");
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

                HandleUserQuery(userInput);
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
                    Console.WriteLine($"Error: {filePath} was not found at the specified location.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error playing audio: {ex.Message}");
            }
        }

        static void HandleUserQuery(string input)
        {
            // Simple sentiment detection (can be expanded)
            string sentiment = DetectSentiment(input);

            // Check for favorite topic mention
            if (input.Contains("favorite") && input.Contains("topic"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Chatbox: What's your favorite cybersecurity topic?");
                string fav = Console.ReadLine()?.Trim().ToLower();
                if (!string.IsNullOrEmpty(fav))
                {
                    favoriteTopic = fav;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Chatbox: Got it, I'll remember that your favorite topic is {favoriteTopic}.");
                }
                return;
            }

            // Keyword-based responses
            if (KeywordMatch(input, new[] { "password", "pass" }))
            {
                lastTopic = "password";
                RespondRandomly(passwordResponses, sentiment);
            }
            else if (KeywordMatch(input, new[] { "phishing", "scam", "link" }))
            {
                lastTopic = "phishing";
                RespondRandomly(phishingResponses, sentiment);
            }
            else if (KeywordMatch(input, new[] { "malware", "virus", "spyware", "ransomware" }))
            {
                lastTopic = "malware";
                RespondRandomly(malwareResponses, sentiment);
            }
            else if (KeywordMatch(input, new[] { "browse", "browsing", "web", "website", "https" }))
            {
                lastTopic = "browsing";
                RespondRandomly(browsingResponses, sentiment);
            }
            else if (KeywordMatch(input, new[] { "purpose", "function", "why" }))
            {
                lastTopic = "purpose";
                RespondRandomly(purposeResponses, sentiment);
            }
            else if (KeywordMatch(input, new[] { "what can i ask", "topics", "ask you" }))
            {
                lastTopic = "ask";
                RespondRandomly(askResponses, sentiment);
            }
            else if (KeywordMatch(input, new[] { "how are you", "how do you feel" }))
            {
                RespondRandomly(feelingResponses, sentiment);
            }
            else if (input.Contains("more details") || input.Contains("explain") || input.Contains("tell me more") || input.Contains("confused"))
            {
                // Continue the last topic for follow-up requests
                if (!string.IsNullOrEmpty(lastTopic))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Chatbox: Sure, here’s some more information on {lastTopic}:");
                    CyberSecurityTipsForTopic(lastTopic);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Chatbox: Could you please specify the topic you'd like more details on?");
                }
            }
            else
            {
                // Default unknown input response
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Chatbox: I'm not sure I understand, {userName}. Can you try rephrasing?");
                Console.WriteLine("Chatbox: Would you like some essential cybersecurity tips? (yes/no)");

                string reply = Console.ReadLine()?.ToLower().Trim();
                if (reply == "yes")
                {
                    CyberSecurityTips();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Chatbox: No worries! Let me know if there is anything else I can help with.");
                }
            }
        }

        static bool KeywordMatch(string input, string[] keywords)
        {
            foreach (string keyword in keywords)
            {
                if (input.Contains(keyword))
                    return true;
            }
            return false;
        }

        static void RespondRandomly(List<string> responses, string sentiment)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            int index = rnd.Next(responses.Count);
            string response = responses[index];

            // Adjust response based on sentiment - simple example
            if (sentiment == "worried" || sentiment == "frustrated")
            {
                response += " Don't worry, take your time to understand it.";
            }
            else if (sentiment == "curious")
            {
                response += " Feel free to ask me more!";
            }

            Console.WriteLine($"Chatbox: {response}");
        }

        static string DetectSentiment(string input)
        {
            input = input.ToLower();
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("frustrated") || input.Contains("confused"))
                return "worried";
            if (input.Contains("curious") || input.Contains("interesting") || input.Contains("learn") || input.Contains("help"))
                return "curious";

            return "neutral";
        }

        static void CyberSecurityTips()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Chatbox: Here are some essential cybersecurity tips:");
            Console.WriteLine("1. Use strong and unique passwords for all your accounts.");
            Console.WriteLine("2. Enable two-factor authentication for extra safety.");
            Console.WriteLine("3. Keep your software and devices updated.");
            Console.WriteLine("4. Beware of suspicious emails or links that might be phishing attempts.");
            Console.WriteLine("5. Use a VPN on public networks when accessing sensitive info.");
            Console.WriteLine("6. Regularly backup important data.");
            Console.WriteLine("7. Use antivirus and firewalls to block threats.");
        }

        static void CyberSecurityTipsForTopic(string topic)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            switch (topic)
            {
                case "password":
                    Console.WriteLine("Make sure your passwords are at least 12 characters long and include a mix of letters, numbers, and symbols.");
                    Console.WriteLine("Avoid using the same password across multiple sites.");
                    Console.WriteLine("Use a reputable password manager to keep track of your passwords.");
                    break;
                case "phishing":
                    Console.WriteLine("Always verify the sender’s email address before clicking on links.");
                    Console.WriteLine("Look out for poor spelling and grammar which can be a sign of phishing.");
                    Console.WriteLine("Never provide sensitive information unless you are sure of the recipient.");
                    break;
                case "malware":
                    Console.WriteLine("Install antivirus software and keep it up to date.");
                    Console.WriteLine("Avoid downloading attachments or software from unknown sources.");
                    Console.WriteLine("Regularly scan your device for malware.");
                    break;
                case "browsing":
                    Console.WriteLine("Only visit secure websites (look for https:// in the URL).");
                    Console.WriteLine("Avoid clicking pop-up ads or suspicious links.");
                    Console.WriteLine("Keep your web browser and plugins updated to protect against vulnerabilities.");
                    break;
                case "purpose":
                    Console.WriteLine("I’m here to guide you and provide important information about staying safe in the digital world.");
                    break;
                case "ask":
                    Console.WriteLine("You can ask me about passwords, phishing, malware, safe browsing, or general cybersecurity questions.");
                    break;
                default:
                    Console.WriteLine("I can help with a variety of cybersecurity topics. What would you like to know more about?");
                    break;
            }
        }
    }
}


