using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaver;

namespace TestWeaver
{
    class Program
    {
        static void Main(string[] args)
        {
            Shaft s1 = new Shaft(8);

            Shaft s2 = !s1;

            Shaft s3 = new Shaft(s1);

            s3[1] = true;

            System.Console.WriteLine("Shaft 1: {0}", s1);
            System.Console.WriteLine("Shaft 2: {0}", s2);
            System.Console.WriteLine("Shaft 3: {0}", s3);



        }
    }
}
