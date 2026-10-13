using Ex03.GarageLogic.Energy;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Interfaces;

namespace Ex03.GarageLogic
{
    public class FuelCar : Car, IRefuelable
    {
        private const float k_MaxFuelAmount = 51f;
        private const eFuelType k_FuelType = eFuelType.Octan95;
        private const int k_NumberOfWheels = 5;
        private const float k_MaxAirPressure = 31f;

        private readonly FuelEnergySource r_FuelEnergySource;

        public FuelCar(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
            r_FuelEnergySource = new FuelEnergySource(0, k_MaxFuelAmount, k_FuelType);
        }

        public FuelEnergySource FuelEnergySource
        {
            get
            {
                return r_FuelEnergySource;
            }
        }

        public void Refuel(eFuelType i_FuelType, float i_AmountToAdd)
        {
            r_FuelEnergySource.Refuel(i_AmountToAdd, i_FuelType);
            EnergyPercentage = r_FuelEnergySource.CurrentAmount / r_FuelEnergySource.MaxAmount * 100;
        }

        public override void SetEnergyPercentage(float i_EnergyPercentage)
        {
            r_FuelEnergySource.SetFuelByPercentage(i_EnergyPercentage);
            EnergyPercentage = i_EnergyPercentage;
        }

        public override void CreateWheels(float i_CurrAirPressure, string i_ManufacturerName)
        {
            base.CreateWheels(k_NumberOfWheels, k_MaxAirPressure, i_CurrAirPressure, i_ManufacturerName);
        }

        public override string ShowFullDetails()
        {
            return string.Format(
                "{0}{1}Fuel type: {2}{1}Current fuel amount: {3}{1}Max fuel amount: {4}",
                base.ShowFullDetails(),
                System.Environment.NewLine,
                r_FuelEnergySource.FuelType,
                r_FuelEnergySource.CurrentAmount,
                r_FuelEnergySource.MaxAmount);
        }
    }
}
