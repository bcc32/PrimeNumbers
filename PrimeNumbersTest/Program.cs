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
            Primes.Cache(2000000);
            foreach (var num in new Primes())
            {
                total += num;
            }
            Console.WriteLine(total);
        }
    }
}
