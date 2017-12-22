using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace IISLogParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;            // The file path for the log to read in
            string writeText;       // Option to write a text file
            bool textFile;

            Console.WriteLine("Enter a full path and filename for the log to parse: ");
            path = Console.ReadLine();   //@"h:\u_ex171219.log"; 
            Console.WriteLine("Path entered is: " + path);

            if (File.Exists(path))
            {
                string targetIP;
                string line;
                var newlines = new List<string>();
                int count = 0;
                bool exitApp = false;

                Console.WriteLine("File exists.");

                while (!exitApp)
                {
                    Console.WriteLine("Enter an IP address to search for, or X to exit: ");
                    targetIP = Console.ReadLine();
                    
                    while ((!Regex.IsMatch(targetIP, @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$")))
                    {
                        if (targetIP.ToUpper() == "X")
                        {
                            exitApp = true;
                            return;
                        }
                        Console.WriteLine("Please enter a valid IP address");
                        targetIP = Console.ReadLine();
                    }

                    Console.WriteLine("Do you want to save results to a text file? Enter Y or N:");
                    writeText = Console.ReadLine();
                    if (writeText.ToUpper().Contains("Y"))
                    {
                        textFile = true;
                        Console.WriteLine("File will be written to c:\\iis_log_output.txt");
                    }
                    else
                    {
                        textFile = false;
                    }

                    System.IO.StreamReader file = new System.IO.StreamReader(path);
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Contains(targetIP))
                        {
                            count++;
                            newlines.Add(line);
                            int uriStemStart = line.IndexOf("/");
                            int uriStemEnd = line.LastIndexOf("- 443 -");

                            if (uriStemEnd < uriStemStart)
                            {
                                uriStemEnd = uriStemStart + 1;
                            }

                            Console.WriteLine(count + " " +
                                "Time: " + line.Substring(0, 19) + " | " +
                                "Method: " + line.Substring(50, 5) + " | " +
                                "URI Stem: " + line.Substring(uriStemStart, uriStemEnd - uriStemStart)
                            );
                        }

                        // To write results to a file. Recommended for large result sets that exceed the screen buffer.
                        if (textFile)
                        {
                            using (System.IO.StreamWriter dest = new System.IO.StreamWriter(@"c:\iis_log_output.txt"))
                            {
                                foreach (var newline in newlines)
                                {
                                    dest.WriteLine(newline);
                                }
                            }
                        }
                    }

                    Console.WriteLine("\n\rFound " + count + " instances of " + targetIP + "\n\r");
                    count = 0;

                }
            }

            else
            {
                Console.WriteLine("File does not exist.");
                return;
            }
        }
    }
}
