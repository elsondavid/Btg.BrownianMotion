namespace Btg.BrownianMotion.Core.Services;

public interface IBrownianMotionService
{
    double[] GenerateBrownianMotion(double sigma, double mean, double initialPrice, int numDays);
}
public class BrownianMotionService : IBrownianMotionService
{
    public double[] GenerateBrownianMotion(double sigma, double mean, double initialPrice, int numDays)
    {
        var rand = new Random();
        var prices = new double[numDays];
        prices[0] = initialPrice;

        for (var i = 1; i < numDays; i++)
        {
            var u1 = 1.0 - rand.NextDouble();
            var u2 = 1.0 - rand.NextDouble();
            var z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

            var dailyResult = mean + sigma * z;
            prices[i] = prices[i - 1] * Math.Exp(dailyResult);
        }

        return prices;
    }
}