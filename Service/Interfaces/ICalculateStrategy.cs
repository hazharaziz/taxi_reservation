using Domain.Entities;

namespace Service.Interfaces
{
    public interface ICalculateStrategy
    {
        public double CalculatePrice(Address source, Address destination);
    }
}
