using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmApp.Services
{
    public interface IConsolePrintingService
    {
        void PrintWindow(string title, List<String> content);
    }
    internal class ConsolePrintingService : IConsolePrintingService
    {
        public void PrintWindow(string title, List<String> content)
        {
            var longestLineLength = content.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur).Length;

            //16 - length of 2 tabs
            int headerLineCount = (int)Math.Ceiling((double)(longestLineLength - title.Length - 4 + 16) /2);
            string header = "┌" + new string('-', headerLineCount - 1) + "┤" + title + "├" + new string('-', headerLineCount - 1) + "┐";

            int footerLineCount = longestLineLength - 4 + 16;
            string footer = "└" + new string('-', footerLineCount) + "┘";
            
            Console.WriteLine(header);
            foreach (var str in content)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine(footer);

        }
    }
}
