using System;

namespace CrClient
{
    public class BruteForcer
    {
        private static string password = "p123";
        private static string result;
        private static bool isMatched = false;

        private static int charactersToTestLength = 0;
        private static long computedKeys = 0;

        private static char[] charactersToTest =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z','A','B','C','D','E',
            'F','G','H','I','J','K','L','M','N','O','P','Q','R',
            'S','T','U','V','W','X','Y','Z','1','2','3','4','5',
            '6','7','8','9','0','!','$','#','@','-','%','^','&',
            '*','(',')','<','>','?',';',':','"','\'','/','\\',
            '{','}','[',']','+','='
        };

        public static void BruteForceTouney()
        {
            DateTime timeStarted = DateTime.Now;
            Console.WriteLine($"Brute forcing started at {timeStarted.ToString("dd/mm/yyyy  hh:mm:ss")}");
        }
    }
}
