using System;
using System.Collections.Generic;


namespace Course_Console
{
    internal class DelegetTest
    {
        // TODO
        public delegate float OperationDelegate(float val1, float val2);

        public static float Add(float val1, float val2)
        {
            return val1 + val2;
        }

        public static float Subtract(float val1, float val2)
        {
            return val1 - val2;
        }

        public static float Multiply(float val1, float val2)
        {
            return val1 * val2;
        }

        public static float Divide(float val1, float val2)
        {
            return val1 / val2;
        }

        Func<int, int> PlusOne = (a) => a + 1;

        public static float ApplyOperation(float val1, float val2, OperationDelegate operation)
        {
            return operation(val1, val2);
        }
    }

    public static class LambdaExpresionsTest
    {
        // TODO
        static public Dictionary<string, Func<float, float, float>> dictionary1 = new() { { "+", (val1, val2) => val1 + val2 },
                                                                                  { "-", (val1, val2) => val1 - val2 },
                                                                                  { "*", (val1, val2) => val1 * val2 },
                                                                                  { "/", (val1, val2) => val1 / val2 } };
        static Func<float, float, float> Plus = (val1, val2) => val1 + val2;
        static Func<float, float, float> Minus = (val1, val2) => val1 - val2;
        static Func<float, float, float> Divide = (val1, val2) => val1 / val2;
        static Func<float, float, float> Multiply = (val1, val2) => val1 * val2;

        static public Dictionary<string, Func<float, float, float>> dictionary2 = new Dictionary<string, Func<float, float, float>> { 
                                                                                    { "+", Plus },
                                                                                    { "-", Minus },
                                                                                    { "/", Divide },
                                                                                    { "*", Multiply } };

        static public Func<float, float, float>? OperationGet(string key)
        {
            Func<float, float, float>? result = null;
            dictionary2.TryGetValue(key, out result);
            return result;
        }
    }
}


