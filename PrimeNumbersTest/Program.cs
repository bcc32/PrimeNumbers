using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrimeNumbers;
using Number = System.Numerics.BigInteger;

namespace PrimeNumbersTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Number total = 0;
            foreach (var num in Primes.Upto(2000000))
            {
                total += num;
            }
            Console.WriteLine(total);
        }
    }
}
