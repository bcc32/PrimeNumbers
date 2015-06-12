using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Number = System.Numerics.BigInteger;

namespace PrimeNumbers
{
    public class Primes : IEnumerable<Number>
    {
        private static Number REASONABLE_LIMIT = new Number(10000000);
        private static List<Number> _cache = new List<Number>() { 2, 3 };

        // caches at least all of the primes <= limit
        public static void Cache(Number limit)
        {
            while (AddOneMore(limit)) // loops until candidate > limit
                ;
        }

        // TODO make enumerator keep generating primes

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Primes._cache.GetEnumerator();
        }

        IEnumerator<Number> IEnumerable<Number>.GetEnumerator()
        {
            return Primes._cache.GetEnumerator();
        }

        public static Number GetNthPrime(Number n)
        {
            while ((Number)_cache.Count < n)
                if (!AddOneMore(REASONABLE_LIMIT))
                    return 0;
            return _cache[(int)(n - 1)];
        }

        public static bool IsPrime(Number number)
        {
            if (number <= _cache.Last())
            {
                // BinarySearch returns negative if not found
                return _cache.BinarySearch(number) >= 0;
            }
            else if (number <= REASONABLE_LIMIT)
            {
                Cache(number);
                return _cache.Last() == number;
            }
            else
            {
                for (Number i = 2; i * i <= number; i++)
                    if (number % i == 0)
                        return false;
                return true;
            }
        }

        public static IEnumerable<Number> Upto(Number limit)
        {
            Cache(limit);
            return new Primes().TakeWhile(a => a <= limit);
        }

        private static bool AddOneMore(Number limit)
        {
            if (limit > REASONABLE_LIMIT) // don't let the user fuck this up.
                limit = REASONABLE_LIMIT;

            Number candidate = _cache.Last();

            try
            {
                bool isPrime = false;
                while (!isPrime)
                {
                    candidate += 2;
                    if (candidate > limit)
                        return false;
                    isPrime = true;
                    foreach (Number factor in _cache)
                    {
                        if (factor * factor > candidate)
                            break;
                        else if (candidate % factor == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                }
            }
            catch (System.OverflowException)
            {
                return false;
            }

            _cache.Add(candidate);
            return true;
        }
    }
}
