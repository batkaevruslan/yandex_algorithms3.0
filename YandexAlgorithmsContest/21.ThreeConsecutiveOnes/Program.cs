using System;
using System.IO;

namespace _21.ThreeConsecutiveOnes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            Console.WriteLine(CalculateNumberOfConsecutiveOnes(n));
            //Console.WriteLine(CalculateNumberOfConsecutiveOnes(4));
            //Console.WriteLine(CalculateNumberOfConsecutiveOnes(5));
           // Console.WriteLine(CalculateNumberOfConsecutiveOnes(6));
           /* for (int i = 0; i < Math.Pow(2, 7); i++)
            {
                Console.WriteLine(Convert.ToString(i, 2).PadLeft(7, '0'));
            }*/
        }

        private static double CalculateNumberOfConsecutiveOnes(int n)
        {
            if (n == 1)
            {
                return 2;
            }
            if (n == 2)
            {
                return 4;
            }
            double _000Count = 1;
            double _001Count = 1;
            double _010Count = 1;
            double _100Count = 1;
            double _101Count = 1;
            double _110Count = 1;
            double _011Count = 1;
            for (int i = 4; i <= n ; i++)
            {
                double _000NewCount = _100Count + _000Count;
                double _001NewCount = _000Count + _100Count;
                double _010NewCount = _001Count + _101Count;
                double _100NewCount = _110Count + _010Count;
                double _101NewCount = _110Count + _010Count;
                double _110NewCount = _011Count;
                double _011NewCount = _001Count + _101Count;

                _000Count = _000NewCount;
                _001Count = _001NewCount;
                _010Count = _010NewCount;
                _100Count = _100NewCount;
                _101Count = _101NewCount;
                _011Count = _011NewCount;
                _110Count = _110NewCount;
            }
            return _000Count + _001Count + _010Count + _100Count + _101Count + _110Count + _011Count;
        }
    }
}