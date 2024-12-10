using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day3
{
    public class Day3
    {
        string rawInputData;
        List<Tuple<int, int>> mulPair;
        List<Tuple<int, int>> enabledMulPair;

        public Day3()
        {
            mulPair = new List<Tuple<int, int>>();
            ParseFile();
            Part1();
            Part2();
        }

        private void Part1()
        {
            var sum = 0;

            foreach (var pair in mulPair)
            {
                sum += pair.Item1 * pair.Item2;
            }

            Console.WriteLine($"Total result of multiplications: {sum}");
        }

        private void Part2()
        {
            var str = CleanString(rawInputData);
            enabledMulPair = ExtractNumbersFromMul(str);
            var sum = 0;

            foreach (var pair in enabledMulPair)
            {
                sum += pair.Item1 * pair.Item2;
            }

            Console.WriteLine($"Total result of enabled multiplications: {sum}");
        }

        private void ParseFile()
        {
            rawInputData = File.ReadAllText("Day3\\input.txt");
            mulPair = ExtractNumbersFromMul(rawInputData);
        }

        private string CleanString(string inputString)
        {
            string pattern = @"don't\(\)[\s\S]*?do\(\)";

            // Replace the matched substring with an empty string
            string result = Regex.Replace(inputString, pattern, "");

            return result;
        }

        static List<Tuple<int, int>> ExtractNumbersFromMul(string input)
        {
            // Define the regex pattern with two capturing groups for the numbers
            string pattern = @"mul\((\d+),(\d+)\)";

            // Find matches
            MatchCollection matches = Regex.Matches(input, pattern);

            // List to store the extracted number pairs
            List<Tuple<int, int>> numberPairs = new List<Tuple<int, int>>();

            foreach (Match match in matches)
            {
                // Parse the numbers and store as a tuple
                int firstNumber = int.Parse(match.Groups[1].Value);
                int secondNumber = int.Parse(match.Groups[2].Value);
                numberPairs.Add(new Tuple<int, int>(firstNumber, secondNumber));
            }

            return numberPairs;
        }
    }
}
