using Domain.Entities;
using Service.Interfaces;

namespace Service.CalculationStrategies
{
    class AfternoonCalculation : ICalculateStrategy
    {
        public double CalculatePrice(Address source, Address destination)
        {
            return 10000;
        }
    }
}
