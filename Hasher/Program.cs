using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hasher
{
    /*
    This is a simple console program that prints a md5, sha1, or sha265 hash based on a specific file chosen in their current directory. 

    Usage: Hasher.exe <md5>/<sha1> example_file.txt

    Output:

    File: C:\Path\to\user\file\example_file.xt
    md5 Hash: 55AA9082F1B961F14CB4B63D0DEED184
    MachineName: Test-System
    04/01/2021 07:59 PM

    Based off (MSFT's) Robert McMurray Blog: https://blogs.iis.net/robert_mcmurray/simple-utility-to-calculate-file-hashes

    Program created by Samuel Huron
    */

    using System;
    using System.IO;
    using System.Net.Security;
    using System.Security.Cryptography;
    using System.Text;

    class Hash_generator
    {
        static void Main(string[] args)
        {
            byte[] hashCalc = null;

            //Check for two command line arguments

            if (args.Length != 2)
            {
                // Show the help message if an incorrect number of arguments was specified.

                helpMenu();
                return;
            }

            //If command args are correct, proceed

            else
            {

                // Create a fileStream for the file.

                FileStream userFile = File.OpenRead(args[1]);

                // Be sure it's positioned to the beginning of the stream.

                userFile.Position = 0;

                // Match the switch case with the specified command line argument.

                switch (args[0])
                {
                    case "md5":
                        // Computes the MD5 hash of the fileStream.
                        hashCalc = MD5.Create().ComputeHash(userFile);
                        break;

                    case "sha1":

                        // Computes the SHA1 hash of the fileStream.
                        hashCalc = SHA1.Create().ComputeHash(userFile);
                        break;

                    case "sha256":
                        // Computes the SHA256 hash of the fileStream.
                        hashCalc = SHA256.Create().ComputeHash(userFile);
                        break;

                    default:

                        // Displays the help message if an unrecognized hash algorithm was specified.
                        helpMenu();
                        return;
                }

                //As long as the hashValue is not empty, continue
                if (hashCalc != null)
                {
                    // Print all methods to the Console.

                    PrintHashInfo(args[0], userFile.Name, hashCalc);
                    netBios();
                    dateTime();
                }
                // Close the file.
                userFile.Close();
            }

        }

        //Creates 3 variables, string finalHash, string fileName, byte hashConvert
        private static void PrintHashInfo(string hashName, string fileName, byte[] hashConvert)
        {
            //Prints file location and hash to console

            Console.Write("File: {0}\r\n{1}: ", fileName, hashName);

            //Creates for loop that stores stream data into byte array

            for (int x = 0; x < hashConvert.Length; x++)
            {
                //String.Format("{0:X2}" specifies hexadecimal format, 2 characters

                Console.Write(String.Format("{0:X2}", hashConvert[x]));

            }

        }

        // Help Menu

        private static void helpMenu()
        {
            Console.WriteLine("Invalid syntax detected!");
            Console.WriteLine("");
            Console.WriteLine("Please use the following syntax to run the executable: hash_gen.exe <md5>/<sha1>/<sha256> <filename>");
            Console.WriteLine("Example: Hasher.exe md5 test_file.txt");
            Console.WriteLine("");
            Console.WriteLine("Please ensure the file you've specified is in your current directory!");

        }

        // Date:Time Method
        private static void dateTime()
        {
            //Prints current date and time to console
            DateTime timeStamp = DateTime.Now;
            Console.WriteLine(timeStamp.ToString("MM/dd/yyyy hh:mm tt"));
        }

        //Prints the NetBIOS name of the system
        public static void netBios()
        {
            Console.WriteLine();
            Console.WriteLine("MachineName: {0}", Environment.MachineName);
        }

    }
}
