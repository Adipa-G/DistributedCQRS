namespace DistCqrs.Sample.Performance.PerformanceTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new ProductTest("http://localhost:5000").Execute();
        }
    }
}