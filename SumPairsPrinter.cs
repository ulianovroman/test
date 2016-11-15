namespace ConsoleApplication1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SumPairsPrinter
    {
        public static void PrintPairs(IEnumerable<int> col, int sum)
        {
            var arr = col.ToArray();
            Array.Sort(arr);

            int i = 0;
            int j = arr.Length - 1;

            while (i <= j)
            {
                var s = arr[i] + arr[j];

                if (s == sum)
                {
                    Console.WriteLine($"{sum} = {arr[i]} + {arr[j]}");
                    ++i;
                    --j;
                }
                else
                {
                    if (s < sum)
                        ++i;
                    else
                        --j;
                }
            }
        }
    }
}
