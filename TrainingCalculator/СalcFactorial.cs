using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace TrainingCalculator
{
    public class СalcFactorial
    {
        public TimeSpan TimeFactorial { get; set; }
        BigInteger FactNaive(int n)
        {
            BigInteger r = 1;
            for (int i = 2; i <= n; ++i)
                r *= i;           
            return r;
        }
        BigInteger ProdTree(int l, int r)
        {
            if (l > r)
                return 1;
            if (l == r)
                return l;
            if (r - l == 1)
                return (BigInteger)l * r;
            int m = (l + r) / 2;
            return ProdTree(l, m) * ProdTree(m + 1, r);
        }
        BigInteger FactTree(int n)
        {
            int t = Environment.TickCount;
            if (n < 0)
                return 0;
            if (n == 0)
                return 1;
            if (n == 1 || n == 2)
                return n;
            return ProdTree(2, n);
        }
        BigInteger FactFactor(int n)
        {
            if (n < 0)
                return 0;
            if (n == 0)
                return 1;
            if (n == 1 || n == 2)
                return n;
            bool[] u = new bool[n + 1];
            List<Tuple<int, int>> p = new List<Tuple<int, int>>();
            for (int i = 2; i <= n; ++i)
                if (!u[i])
                {
                    int k = n / i;
                    int c = 0;
                    while (k > 0)
                    {
                        c += k;
                        k /= i;
                    }
                    p.Add(new Tuple<int, int>(i, c));
                    int j = 2;
                    while (i * j <= n)
                    {
                        u[i * j] = true;
                        ++j;
                    }
                }
            BigInteger r = 1;
            for (int i = p.Count() - 1; i >= 0; --i)
                r *= BigInteger.Pow(p[i].Item1, p[i].Item2);
            return r;
        }
        public BigInteger Calculate(int n)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            BigInteger bi = FactNaive(n);
            stopWatch.Stop();
            TimeFactorial = stopWatch.Elapsed;
            return bi;
        }
    }
}
