using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;


namespace AI
{
    public class Program
    {
        public string[] variables;
        public string[] domain;
        public string[] result;
        // here I couldn't find eval in c# so i used if else statement instead, in python we will do it with eval
        public string[] constraints;

        public Program(string[] variables, string[] domain, string[] result)
        {
            this.variables = variables;
            this.domain = domain;
            this.result = result;
        }

        public static bool ToPlaceOrNotToPlace(string[] result, int Line, string variable, int problem)
        {
            if (problem == 1)
            {
                if (Line == 0)
                    return true;
                for (int i = 0; i < Line; i++)
                {
                    if ((Math.Abs((Convert.ToInt32(variable) - Convert.ToInt32(result[i]))) == Math.Abs(Line - i)) && ((Convert.ToInt32(result[i])) != 0))
                        return false;
                    if (Convert.ToInt32(variable) == Convert.ToInt32(result[i]))
                        return false;
                }

                return true;
            }
            else
            {
                if (Line == 0)
                    return true;

                if ((result[0] == result[5]) && (!result[0].Equals("0")))
                    return false;
                if ((result[0] == result[1]) && (!result[0].Equals("0")))
                    return false;
                if ((result[1] == result[5]) && (!result[1].Equals("0")))
                    return false;
                if ((result[1] == result[2]) && (!result[1].Equals("0")))
                    return false;
                if ((result[2] == result[5]) && (!result[2].Equals("0")))
                    return false;
                if ((result[3] == result[5]) && (!result[3].Equals("0")))
                    return false;
                if ((result[2] == result[3]) && (!result[2].Equals("0")))
                    return false;
                if ((result[4] == result[5]) && (!result[4].Equals("0")))
                    return false;
                if ((result[3] == result[4]) && (!result[3].Equals("0")))
                    return false;

                return true;
            }

        }

        public bool BacktrackSolve(string[] result, int Line, int problem)
        {
            if (Line >= this.variables.Length)
                return true;
            else
            {
                foreach (string d in domain)
                {
                    if (ToPlaceOrNotToPlace(result, Line, d, problem))
                    {
                        result[Line] = d;
                        // if you want to print the trace of the process just call printSolution here
                        if (BacktrackSolve(result, Line + 1, problem))
                            return true;
                        result[Line] = "0";
                    }
                }
            }

            return false;
        }

        public static List<string>[] remove(List<string>[] tmp, string[] result, int Line, string variable, int problem)
        {
            int index;
            List<string>[] FD = new List<string>[tmp.Length];
            Array.Copy(tmp, FD, tmp.Length);
            for (int i = Line + 1; i < FD.Length; i++)
            {
                foreach (string v in FD[i].ToList())
                {
                    result[i] = v;
                    if (!ToPlaceOrNotToPlace(result, i, v, problem))
                    {
                        index = FD[i].IndexOf(v);
                        if (index >= 0)
                            FD[i].RemoveAt(index);
                    }
                    result[i] = "0";
                }

            }
            return FD;
        }

        public bool ForwardCheckSolve(List<string>[] forwardDomain, string[] result, int Line, int problem)
        {
            if (Line >= this.variables.Length)
                return true;
            else
            {
                for (int i = Line; i < forwardDomain.Length; i++)
                {
                    if (forwardDomain[i].Count == 0)
                        return false;
                    foreach (string d in forwardDomain[i].ToList())
                    {
                        if (ToPlaceOrNotToPlace(result, Line, d, problem))
                        {
                            result[Line] = d;
                            List<string>[] original = new List<string>[forwardDomain.Length];
                            for (int k = 0; k < forwardDomain.Length; k++)
                            {
                                original[k] = new List<string>(forwardDomain[k].Count);
                                forwardDomain[k].ForEach((item) =>
                                {
                                    original[k].Add((string)item.Clone());
                                });
                            }
                            forwardDomain = remove(forwardDomain, result, Line, d, problem);
                            // if you want to see the trace
                            //ForwardTrace(forwardDomain, result);
                            if (ForwardCheckSolve(forwardDomain, result, Line + 1, problem))
                                return true;
                            forwardDomain = original;
                            result[Line] = "0";

                        }
                    }
                }
            }
            return false;
        }

        public void printSolution(string[] result, int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(this.variables[i] + " is set to " + result[i]);
            }
        }

        public void ForwardTrace(List<string>[] forwardDomain, string[] result)
        {
            Console.WriteLine("------------------------------------");
            for (int i = 0; i < forwardDomain.Length; i++)
            {
                Console.WriteLine("Line " + i + " is set to " + result[i]);
                for (int j = 0; j < forwardDomain[i].Count; j++)
                {
                    Console.Write(forwardDomain[i][j]);
                }
                Console.Write("\n");
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Choose the problem that you want to solve");
                Console.WriteLine("1 - N Queens \n2 - Map Filling");
                int Pchoice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Choose the algorithme you want to work with");
                Console.WriteLine("1 - BackTrack\n2 - Forward check");
                int Schoice = Convert.ToInt32(Console.ReadLine());
                switch (Pchoice)
                {
                    case 1:
                        {
                            switch (Schoice)
                            { //Gmeter
                                case 1:
                                    {
                                        // N queens with backtrack
                                        string[] array1 = new string[] { "Q1", "Q2", "Q3", "Q4", "Q5", "Q6", "Q7", "Q8" };
                                        string[] array2 = new string[] { "1", "2", "3", "4", "5", "6", "7", "8" };
                                        string[] array3 = new string[] { "0", "0", "0", "0", "0", "0", "0", "0" };
                                        Program p = new Program(array1, array2, array3);
                                        if (!p.BacktrackSolve(p.result, 0, 1))
                                        { Console.WriteLine("There is no solution"); }
                                        p.printSolution(p.result, 8);
                                        break;
                                    }
                                case 2:
                                    {
                                        //N queens with forward check
                                        string[] array1 = new string[] { "Q1", "Q2", "Q3", "Q4", "Q5", "Q6", "Q7", "Q8" };
                                        string[] array2 = new string[] { "1", "2", "3", "4", "5", "6", "7", "8" };
                                        string[] array3 = new string[] { "0", "0", "0", "0", "0", "0", "0", "0" };
                                        Program p = new Program(array1, array2, array3);
                                        List<string>[] forwardDomain = new List<string>[8];
                                        for (int i = 0; i < 8; i++)
                                        { forwardDomain[i] = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8" }; }
                                        if (!p.ForwardCheckSolve(forwardDomain, p.result, 0, 1))
                                        { Console.WriteLine("There is no solution"); }
                                        p.printSolution(p.result, 8); 
                                        break;
                                    }
                            }
                            break;
                        }
                    case 2:
                        {
                            switch (Schoice)
                            {
                                case 1:
                                    {
                                        //Map Filling with backtrack
                                        string[] array1 = new string[] { "WA", "NT", "Q", "NSW", "V", "SA", "T" };
                                        string[] array2 = new string[] { "R", "G", "B" };
                                        string[] array3 = new string[] { "0", "0", "0", "0", "0", "0", "0" };
                                        Program p = new Program(array1, array2, array3);
                                        if (!p.BacktrackSolve(p.result, 0, 2))
                                        { Console.WriteLine("There is no Solution"); }
                                        p.printSolution(p.result, 7);
                                        break;
                                    }
                                case 2:
                                    {
                                        //Map filling with forwardcheck
                                        string[] array1 = new string[] { "WA", "NT", "Q", "NSW", "V", "SA", "T" };
                                        string[] array2 = new string[] { "R", "G", "B" };
                                        string[] array3 = new string[] { "0", "0", "0", "0", "0", "0", "0" };
                                        Program p = new Program(array1, array2, array3);
                                        List<string>[] forwardDomain = new List<string>[7];
                                        for (int i = 0; i < 7; i++)
                                        { forwardDomain[i] = new List<string>() { "R", "G", "B" }; }
                                        if (!p.ForwardCheckSolve(forwardDomain, p.result, 0, 2))
                                        { Console.WriteLine("There is no solution"); }
                                        p.printSolution(p.result, 7);
                                        break;
                                    }
                            }
                            break;
                        }
                }
                Console.WriteLine("1 - Continue \n0 - Exit");
                int exit = Convert.ToInt32(Console.ReadLine());
                if (exit == 0)
                    System.Environment.Exit(1);
                Console.WriteLine("------------------------------------------");
            }
        }
    }
}
