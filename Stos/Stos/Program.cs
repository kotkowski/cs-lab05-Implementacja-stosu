using System;

namespace Stos
{
    class Program
    {
        static void Main(string[] args)
        {
            StosWTablicy<string> s = new StosWTablicy<string>();
            s.Push("km");
            s.Push("aa");
            s.Push("ab");
           

            foreach (var x in s.ToArray())
                Console.WriteLine(x);

            
            foreach (var x in ((StosWTablicy<string>)s).Revert)
                Console.WriteLine(x);

            Console.WriteLine($"Długość: {s.Length()}");
            s.TrimExcess();
            Console.WriteLine($"Przycięty: {s.Length()}");
            Console.WriteLine($"Pierwszy element: {s[0]}");
            Console.WriteLine($"Ostatni element: {s[2]}");


        }
    }
}
