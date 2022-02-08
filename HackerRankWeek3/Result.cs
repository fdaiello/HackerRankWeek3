using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System;

namespace HackerRankWeek3
{
    class Result
    {
        public static List<int> icecreamParlor(int m, List<int> arr)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();

            for ( int i = 0; i<arr.Count; i++)
            {
                int needed = m - arr[i];
                if (map.ContainsKey(needed))
                {
                    return new List<int>() { i+1, map[needed]+1 };
                }
                else
                {
                    map.Add(arr[i], i);
                }
            }

            return new List<int>();
        }

        public static List<int> icecreamParlor1(int m, List<int> arr)
        {
            for ( int n1 =0; n1<arr.Count-1; n1++)
            {
                for ( int n2 = n1+1; n2 < arr.Count; n2++)
                {
                    if ( arr[n1] + arr[n2] == m)
                    {
                        return new List<int>() { n1+1, n2+1 };
                    }
                }
            }

            return new List<int>();
        }

        public static List<int> icecreamParlor2(int m, List<int> arr)
        {

            // Start a StopWatch to count the time
            Stopwatch watch = new Stopwatch();
            watch.Start();

            // Creates a new List that we will sort
            List<int> sList = new List<int>(arr);

            // Sort copy list
            sList.Sort();

            // Search Two numbers that sum m; First loop goes to last but one element
            for ( int n1=0; n1<arr.Count-1; n1++)
            {
                // Second loop - only to following numbers
                for ( int n2=n1+1; n2<arr.Count; n2++)
                {
                    // Check if two number sum equals searched m
                    if ( sList[n1] + sList[n2] == m)
                    {
                        // We found the values; Now we need to check the original position;
                        int p1 = arr.IndexOf(sList[n1]);
                        int p2 = arr.IndexOf(sList[n2]);

                        // If position is the same, values are the same; we have duplicated values; lets get next one
                        if (p1 == p2)
                            p2++;

                        // Return array with positions
                        List<int> ret = new List<int>() { p1 + 1, p2 + 1 };
                        ret.Sort();

                        watch.Stop();
                        Console.WriteLine($"Tempo em ms : {watch.ElapsedMilliseconds}");

                        return ret;
                    }
                    // If sum exceeds m, exit inner loop
                    else if (sList[n1] + sList[n2] > m)
                    {
                        break;
                    }
                }
            }

            // Output elasped time
            watch.Stop();
            Console.WriteLine($"Tempo em ms : {watch.ElapsedMilliseconds}");

            // If we got here, no two numbers were found - return empty list
            return new List<int>();
        }
        public static List<string> bomberMan(int n, List<string> grid)
        {
            if (n < 2)
                return grid;

            // At every even cicle grid is full of bombs
            else if (n % 2 == 0)
            {
                for (int x = 0; x < grid.Count; x++)
                    grid[x] = String.Empty.PadLeft(grid[x].Length, 'O');
                return grid;
            }

            int[,] aGrid = new int[grid.Count, grid[0].Length];
            InitBombs(grid, aGrid);
            WriteGrid(aGrid);       // debug

            int s = 2;

            // At every 4 cicles, pattern gets repeated
            /*
             *   3 -  Pattern 1
             *   5 -  Pattern 2
             *   7 -  Pattern 1
             *   9 -  Pattern 2
             *   11 - Pattern 1
             *   13 - Pattern 2
             *   15 - Pattern 1
             *   17 - Pattern 2
             */

            while (s <= n)
            {
                PlantBombs(aGrid, s);

                Console.WriteLine(s);   // debug
                WriteGrid(aGrid);       // debug

                s++;

                if (s > n) break;

                ExplodeBombs(aGrid, s);

                Console.WriteLine(s);   // debug
                WriteGrid(aGrid);       // debug

                s++;
            }

            return aGridToList(aGrid);
        }
        static void InitBombs(List<string> grid, int[,] aGrid)
        {
            for (int x = 0; x < grid.Count; x++)
            {
                for (int y = 0; y < grid[x].Length; y++)
                {
                    if (grid[x][y] == '.')
                    {
                        aGrid[x, y] = -1;
                    }
                    else
                    {
                        aGrid[x, y] = 0;
                    }
                }
            }
        }
        static void PlantBombs(int[,] aGrid, int s)
        {
            for (int x = 0; x < aGrid.GetLength(0); x++)
            {
                for (int y = 0; y < aGrid.GetLength(1); y++)
                {
                    if (aGrid[x, y] == -1)
                        aGrid[x, y] = s;
                }
            }
        }
        static void ExplodeBombs(int[,] aGrid, int s)
        {
            // loop rows
            for (int x = 0; x < aGrid.GetLength(0); x++)
            {
                // loop cols
                for (int y = 0; y < aGrid.GetLength(1); y++)
                {
                    if (s - Convert.ToInt32(aGrid[x, y]) == 3)
                    {
                        aGrid[x, y] = -1;
                        if (x > 0 && aGrid[x - 1, y] > s - 3)
                            aGrid[x - 1, y] = -1;
                        if (x < aGrid.GetLength(0) - 1 && aGrid[x + 1, y] > s - 3)
                            aGrid[x + 1, y] = -1;
                        if (y > 0 && aGrid[x, y - 1] > s - 3)
                            aGrid[x, y - 1] = -1;
                        if (y < aGrid.GetLength(1) - 1 && aGrid[x, y + 1] > s - 3)
                            aGrid[x, y + 1] = -1;
                    }
                }
            }
        }
        static List<string> aGridToList(int[,] aGrid)
        {
            List<string> list = new List<string>();
            char[] row = new char[aGrid.GetLength(1)];

            for (int x = 0; x < aGrid.GetLength(0); x++)
            {
                for (int y = 0; y < aGrid.GetLength(1); y++)
                {
                    if (aGrid[x, y] == -1)
                        row[y] = '.';
                    else
                        row[y] = aGrid[x, y].ToString()[0]; ;
                    // row[y] = 'O';
                }
                list.Add(new string(row));
            }

            return list;
        }

        static void WriteGrid(int[,] aGrid)
        {
            var grid2 = aGridToList(aGrid);

            foreach (string s in grid2)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();
        }
        /*
         *  New Year Caos
         */
        public static void minimumBribes0(List<int> q)
        {
            int b = 0;
            bool t = false;


            for (int x = 0; x < q.Count - 1; x++)
            {
                int b1 = 0;
                for (int y = x + 1; y < q.Count; y++)
                {

                    if (q[y] < q[x])
                        b1++;
                    if (b1 > 2)
                    {
                        Console.WriteLine("Too chaotic");
                        t = true;
                        break;
                    }
                }
                b += b1;
                if (t)
                    break;
            }

            if (!t)

                Console.WriteLine(b);

        }
        public static void minimumBribes(List<int> q)
        {

            int shifts = 0;
            int count = 0;

            for (int i = 0; i < q.Count; i++)
            {
                // sticker number
                int num = q[i];

                // position in line
                int pos = i + 1;

                // number of positions shifted
                int shift = pos - num;

                if (shift < 0)
                {
                    if (shift < -2)
                    {
                        Console.WriteLine("Too chaotic");
                        return;
                    }

                    count += Math.Abs(shift);

                    shifts++;

                }
                else
                {
                    if (shifts > shift)
                    {
                        count++;
                    }

                    shifts = 0;
                }

            }

            Console.WriteLine(count);

        }

        /*
         *  Return YES if characters in string appears the same number of times
         *  One character - one instance - is allowed to be removed;
         */
        public static string isValid(string s)
        {
            Dictionary<char, int> dic = new Dictionary<char, int>();

            for (int x = 0; x < s.Length; x++)
            {
                if (dic.ContainsKey(s[x]))
                {
                    dic[s[x]]++;
                }
                else
                {
                    dic.Add(s[x], 1);
                }
            }

            int t1 = 0, t2 = 0, ct1 = 0, ct2 = 0;

            foreach (KeyValuePair<char, int> kp in dic)
            {
                if (t1 == 0)
                {
                    t1 = kp.Value;
                    ct1 = 1;
                }
                else if (kp.Value == t1)
                {
                    ct1++;
                }
                else if (t2 == 0)
                {
                    t2 = kp.Value;
                    ct2 = 1;
                }
                else if (kp.Value == t2)
                {
                    ct2++;
                }
                else
                {
                    return "NO";
                }
            }

            if (t2 == 0 || (t1 == 1 && ct1 == 1) || (t2 == 1 && ct2 == 1) || (Math.Abs(t1 - t2) == 1 && (ct1 == 1 || ct2 == 1)))
            {
                return "YES";
            }
            else
            {
                return "NO";
            }
        }
        /*
         * Return YES or NO if the string contains balanced brackets
         * 
         *  {[()]}      YES
         *  {[(})]}     NO
         */
        public static string isBalanced(string s)
        {
            // Shortcut: Balanced bracket string must have even number of characters
            if (s.Length % 2 != 0)
                return "NO";
            else if (s.Length == 0)
                return "YES";

            // Stack
            Stack<char> stack = new Stack<char>();

            // Run the string
            for (int i = 0; i < s.Length; i++)
            {
                // Check if its an opening symbol
                if (s[i] == '[' || s[i] == '{' || s[i] == '(')
                {
                    // Pushes opening bracket at the stack
                    stack.Push(s[i]);
                }
                else if (s[i] == ']')
                {
                    // Checks if last pushed bracked matches
                    if (stack.Count == 0 || stack.Pop() != '[')
                        return "NO";

                }
                else if (s[i] == ')')
                {
                    // Checks if last pushed bracked matches
                    if (stack.Count == 0 || stack.Pop() != '(')
                        return "NO";
                }
                else if (s[i] == '}')
                {
                    // Checks if last pushed bracked matches
                    if (stack.Count == 0 || stack.Pop() != '{')
                        return "NO";
                }
            }

            if (stack.Count == 0)
                return "YES";
            else
                return "NO";

        }

        public static List<int> waiter(List<int> number, int q)
        {

            // PrimeList
            List<int> primeList = GeneratePrimeList(q);

            // answers
            List<int> answers = new List<int>();

            // Stack
            Stack<int> stack = new Stack<int>(number);

            // Interations
            int nextPlate;
            for (int i = 0; i < q; i++)
            {
                //  A e B
                Stack<int> A1 = new Stack<int>();
                Stack<int> B1 = new Stack<int>();

                // Loop for all plates in Stack
                while (stack.Count > 0)
                {
                    // Remove next plate from stack
                    nextPlate = stack.Pop();

                    // Determine if the number on the plate is evenly divisble by the iTh prime number;
                    if (nextPlate % primeList[i] == 0)
                    {
                        // yes, stack on B
                        B1.Push(nextPlate);
                    }
                    else
                    {
                        // no, push it on A
                        A1.Push(nextPlate);
                    }
                }

                // Move B elements to Answer from top to down
                while (B1.Count > 0)
                {
                    answers.Add(B1.Pop());
                }

                // Lets use A1 on the next interation
                stack = A1;
            }

            //  Once the required number of iterations is complete, store the remaining values in stack , again from top to bottom.
            while (stack.Count > 0)
            {
                answers.Add(stack.Pop());
            }

            // Return answers
            return answers;
        }
        public static List<int> GeneratePrimeList(int n)
        {
            List<int> primeList = new List<int>() { 2, 3, 5, 7, 11, 13 };
            int last = 13;

            while (primeList.Count < n)
            {
                last++;
                if (IsPrime(last))
                {
                    primeList.Add(last);
                }
            }

            return primeList;
        }
        public static bool IsPrime(int n)
        {
            if (n < 2)
                return false;

            for (int i = 2; i < n; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
