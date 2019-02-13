using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace csvCompare
{
    class Program
    {
        static void Main(string[] args)
        {
 
            ConfirmFilePathsCorrect(args);

            string[] filePathOne = System.IO.File.ReadAllLines(args[0]);
            string[] filePathTwo = System.IO.File.ReadAllLines(args[1]);
            string resultsPath = args[2];

            IEnumerable<string> differenceQuery = filePathOne.Except(filePathTwo);

            ShowResultsOnConsole(differenceQuery, args);
            ExportResultsToCSV(differenceQuery, resultsPath);

            Console.WriteLine("All done.");
            Console.ReadLine();

        }

        private static void ConfirmFilePathsCorrect(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Please include the path to the Original CSV, the Current CSV, and the Output\n" +
                    "Eg: C:\\tech\\original.csv c:\\tech\\current.csv c:\\tech\\output.csv");
                Console.ReadLine();
                Environment.Exit(0);
            }

            /* Check for Original and Current CSVs to compare */
            for (int i = 0; i < 2; i++)
            {
                if (!File.Exists(args[i]))
                {
                    Console.WriteLine("Unable to open {0}\nCheck path is correct and file is closed.", args[i]);
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }

            /* Check if we can write to the output */
            try
            {
                File.WriteAllText(args[2], "Placeholder");
            }
            catch
            {
                Console.WriteLine("Unable to write to {0}\nCheck you have permissions to that folder", args[2]);
                Console.ReadLine();
                Environment.Exit(0);
            }

        }

        private static void ExportResultsToCSV(IEnumerable<string> differenceQuery, string resultsPath)
        {
            StringBuilder results = new StringBuilder();

            foreach (string s in differenceQuery)
                results.AppendFormat("{0}\n", s.ToString());

            File.WriteAllText(resultsPath, results.ToString());
        }

        private static void ShowResultsOnConsole(IEnumerable<string> differenceQuery, string[] args)
        {
            Console.WriteLine("The following files are in {0},\nbut not in {1} :_ \n", args[0], args[1]);

            foreach (string s in differenceQuery)
                Console.WriteLine(s);
        }


    }
}
