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
            string path;    // The file path for the log to read in

            Console.WriteLine("Enter a full path and filename for the log to parse: ");
            path = Console.ReadLine();   //@"h:\u_ex171219.log"; 
            Console.WriteLine("Path entered is: " + path);

            if (File.Exists(path))
            {
                string targetIP;
                string line;
                var newlines = new List<string>();
                int count = 0;

                Console.WriteLine("File exists.");
                Console.WriteLine("Enter an IP address to search for: ");
                targetIP = Console.ReadLine();
                while ((!Regex.IsMatch(targetIP, @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$")))    
                {
                    Console.WriteLine("Please enter a valid IP address");
                    targetIP = Console.ReadLine();
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
                    //using (System.IO.StreamWriter dest = new System.IO.StreamWriter(@"c:\iis_log_output.txt"))
                    //{
                    //    foreach (var newline in newlines)
                    //    {
                    //        dest.WriteLine(newline);
                    //    }
                    //}
                }

                Console.WriteLine("\n\rFound " + count + " instances of " + targetIP + "\n\r");

            }
            else
            {
                Console.WriteLine("File does not exist.");
                return;
            }
        }
    }
}
