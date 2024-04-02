using System;

namespace Course_Console
{
    internal class MathClass
    {
        public static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static void Run(string line)
        {
            // TODO
            int value;
            if (!Int32.TryParse(line, out value) || 0 > value || value > 180)
            {
                Console.WriteLine("Check the input!");
                return;
            }

            Console.WriteLine("Cos = {0}", Math.Cos(ConvertToRadians(value)));
            Console.WriteLine("Sin = {0}", Math.Sin(ConvertToRadians(value)));
            Console.WriteLine("Tg = {0}", Math.Tan(ConvertToRadians(value)));
        }
    }
}
