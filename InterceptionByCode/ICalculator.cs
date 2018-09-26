namespace InterceptionByCode
{
    public interface ICalculator
    {
        //[CachingHandler(1, 0, 0)]
        [Measure(Order = 1)]
        [Measuring(Order = 0)]
        int Add(int first, int second);

        [Measure(Order = 2)]
        int Multiply(int first, int second);
    }
}
