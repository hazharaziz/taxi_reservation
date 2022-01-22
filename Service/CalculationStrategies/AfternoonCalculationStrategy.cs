using Domain.Entities;
using Service.Interfaces;

namespace Service.CalculationStrategies
{
    class AfternoonCalculationStrategy : ICalculateStrategy
    {
        public double CalculatePrice(Address source, Address destination)
        {
            return 10000;
        }
    }
}
