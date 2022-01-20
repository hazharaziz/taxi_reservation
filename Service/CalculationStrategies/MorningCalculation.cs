﻿using Domain.Entities;
using Service.Interfaces;

namespace Service.CalculationStrategies
{
    class MorningCalculation : ICalculateStrategy
    {
        public double CalculatePrice(Address source, Address destination)
        {
            return 2000;
        }
    }
}
