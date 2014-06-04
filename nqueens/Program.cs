using System;
using System.Collections.Generic;
using System.Linq;

namespace nqueens
{
    internal class Program
    {
        private static void Main()
        {
            DisplaySolutions(Queens(8));
        }

        private static void DisplaySolutions(IEnumerable<IList<int>> solutions)
        {
            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            solutions.Select((solution, index) =>
                {
                    DisplaySolution(solution, index);
                    Console.WriteLine();
                    return 0;
                }).ToList();
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed
        }

        private static void DisplaySolution(IList<int> solution, int solutionIndex)
        {
            Console.WriteLine("Solution {0}", solutionIndex + 1);
            PrintBoard(solution, solution.Count);
        }

        public static void PrintBoard(IList<int> solution, int n)
        {
            if (n == 0) return;
            if (solution.Count == 0) return;

            var rowDivider = MakeRowDivider(n);

            for (var row = 0; row < n && row < solution.Count; row++)
            {
                Console.WriteLine(rowDivider);
                var line = string.Empty;
                for (var col = 0; col < n; col++)
                {
                    line += "|";
                    line += solution[row] == col ? " Q " : new string(' ', 3);
                }
                line += "|";
                Console.WriteLine(line);
            }

            Console.WriteLine(rowDivider);
        }

        private const char BoxCorner = '+';
        private const char BoxSide = '-';

        private static string MakeRowDivider(int n)
        {
            return string.Concat(Enumerable.Repeat(BoxCorner + new string(BoxSide, 3), n)) + BoxCorner;
        }

        private static IEnumerable<IList<int>> Queens(int n)
        {
            return PlaceQueens(n, n);
        }

        private static IEnumerable<IList<int>> PlaceQueens(int k, int n)
        {
            if (k == 0) {
                yield return new List<int>();
                yield break;
            }

            var solutionsToSmallerProblem = PlaceQueens(k - 1, n).ToList();

            foreach (var solutionToSmallerProblem in solutionsToSmallerProblem)
            {
                foreach (var col in Enumerable.Range(0, n))
                {
                    if (IsSafe(col, solutionToSmallerProblem))
                    {
                        var smallerSolution = new List<int> {col};
                        smallerSolution.AddRange(solutionToSmallerProblem);
                        yield return smallerSolution;
                    }
                }
            }
        }

        private static bool IsSafe(int col, IList<int> queens)
        {
            var rowIndexes = Enumerable.Range(0, queens.Count).Reverse().ToList();
            var existingCoordSums = rowIndexes.Zip(queens, (r, c) => r + c);
            var existingCoordDifferences = rowIndexes.Zip(queens, (r, c) => r - c);
            var row = queens.Count;
            var newCoordsSum = row + col;
            var newCoordsDifference = row - col;
            return !queens.Contains(col)
                   && !existingCoordSums.Contains(newCoordsSum)
                   && !existingCoordDifferences.Contains(newCoordsDifference);
        }
    }
}
