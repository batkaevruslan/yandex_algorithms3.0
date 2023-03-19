using System;
using System.Collections.Generic;
using System.Linq;

namespace _23.Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            (int result, List<int> numbers) = GetOperationsCount(n);
            Console.WriteLine(result);
            Console.WriteLine(string.Join(" ", numbers.OrderBy(x => x)));
        }

        private static (int operationsCount, List<int> numberSequense) GetOperationsCount(int n)
        {
            (int operationCount, int previousNumber)[] operationsPerNumber = new (int, int)[n + 1];
            operationsPerNumber[0] = new(-1, 1);
            for (int number = 1; number <= n; number++)
            {
                int numberLessByOne = number - 1;
                (int previousOperationsCount, int previousNumber) = (operationsPerNumber[numberLessByOne].operationCount, numberLessByOne);
                if (number % 2 == 0)
                {
                    int numberTwiceLess = number / 2;
                    int numberTwiceLessOperationsCount = operationsPerNumber[numberTwiceLess].operationCount;
                    if (numberTwiceLessOperationsCount < previousOperationsCount)
                    {
                        (previousOperationsCount, previousNumber) = (numberTwiceLessOperationsCount, numberTwiceLess);
                    }
                }
                if (number % 3 == 0)
                {
                    int numberThreeTimesLess = number / 3;
                    int numberThreeTimesLessOperationCount = operationsPerNumber[numberThreeTimesLess].operationCount;
                    if (numberThreeTimesLessOperationCount < previousOperationsCount)
                    {
                        (previousOperationsCount, previousNumber) = (numberThreeTimesLessOperationCount, numberThreeTimesLess);
                    }
                }
                operationsPerNumber[number] = (previousOperationsCount + 1, previousNumber);
            }

            return BuildResult(n, operationsPerNumber);
        }

        private static (int operationsCount, List<int> numberSequense) BuildResult(int n, (int operationCount, int previousNumber)[] operationsPerNumber)
        {
            List<int> numberSequense = new List<int>
            {
                n
            };
            (int operationsCount, int previousNumber) = operationsPerNumber[n];
            int resultOperationCount = operationsCount;
            while (operationsCount > 0)
            {
                numberSequense.Add(previousNumber);
                (operationsCount, previousNumber) = operationsPerNumber[previousNumber];
            }
            
            return (resultOperationCount, numberSequense);
        }
    }
}