using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day2
{
    public class Day2
    {
        List<int[]> inputData;

        public Day2()
        {
            inputData = new List<int[]>();
            ParseFile();
            int safeEntriesCount = 0;
            int safeEntriesProblemDampenerCount = 0;

            foreach (var inputData in inputData)
            {
                var isSafe = IsEntrySafe(inputData);
                //Console.WriteLine($"{ isSafe } - {string.Join(' ', inputData)}");

                if (isSafe)
                    ++safeEntriesCount;
            }

            Console.WriteLine($"Total safe entries: {safeEntriesCount}");

            foreach (var inputData in inputData)
            {
                var isSafe = ProblemDampener(inputData);
                //Console.WriteLine($"{ isSafe } - {string.Join(' ', inputData)}");

                if (isSafe)
                    ++safeEntriesProblemDampenerCount;
            }

            Console.WriteLine($"Total Problem Dampener safe entries: {safeEntriesProblemDampenerCount}");
        }

        private bool ProblemDampener(int[] entry)
        {
            var isEntrySafe = IsEntrySafe(entry);

            if (isEntrySafe) return true;

            for (var i = 0; i < entry.Length; i++) 
            {
                if (IsEntrySafe(RemoveAt(entry, i)))
                    return true;
            }

            return false;
        }

        private int[] RemoveAt(int[] array, int position)
        {
            if (position < 0 || position >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(position), "Position is out of range.");
            }

            int[] result = new int[array.Length - 1];
            int index = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (i != position)
                {
                    result[index++] = array[i];
                }
            }

            return result;
        }

        private bool IsEntrySafe(int[] entry)
        {
            int[] differences = entry
                .Zip(entry.Skip(1), (a, b) => b - a)
                .ToArray();

            bool isIncremental = entry
                .Zip(entry.Skip(1), (a, b) => b >= a)
                .All(x => x); 

            bool isDecreasing = entry
                .Zip(entry.Skip(1), (a, b) => b <= a)
                .All(x => x);

            if (isIncremental && isDecreasing) return false;

            if (!isIncremental && !isDecreasing) return false;

            if (differences.Any(x => Math.Abs(x) > 3)) return false;

            if (differences.Any(x => Math.Abs(x) == 0)) return false;

            return true;
        }

        private void ParseFile()
        {
            var strArr = System.IO.File.ReadAllLines("Day2\\input.txt");

            foreach (var line in strArr)
            {
                var splittedLine = line.Split(' ').Select(int.Parse).ToArray();

                inputData.Add(splittedLine);
            }
        }
    }
}
