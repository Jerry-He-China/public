using System;

namespace InterceptionByCode
{
    public class SimpleCalculator : ICalculator
    {
        public int Add(int first, int second)
        {
            Console.WriteLine(@"In method Add.");
            return first+second;
        }

        public int Multiply(int first, int second)
        {
            throw new NotImplementedException();
        }
    }
}
