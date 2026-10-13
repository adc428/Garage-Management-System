using Ex03.GarageLogic.Energy;
using Ex03.GarageLogic.Interfaces;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle, IChargeable
    {
        private const float k_MaxBatteryTime = 3.0f;
        private const int k_NumberOfWheels = 2;
        private const float k_MaxAirPressure = 30f;

        private readonly ElectricEnergySource r_ElectricEnergySource;

        public ElectricMotorcycle(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
            r_ElectricEnergySource = new ElectricEnergySource(0, k_MaxBatteryTime);
        }

        public ElectricEnergySource ElectricEnergySource
        {
            get
            {
                return r_ElectricEnergySource;
            }
        }

        public void ChargeBattery(float i_MinToCharge)
        {
            r_ElectricEnergySource.ChargeBattery(i_MinToCharge / 60);
            EnergyPercentage = r_ElectricEnergySource.CurrentAmount / r_ElectricEnergySource.MaxAmount * 100;
        }

        public override void SetEnergyPercentage(float i_EnergyPercentage)
        {
            r_ElectricEnergySource.SetBatteryByPercentage(i_EnergyPercentage);
            EnergyPercentage = i_EnergyPercentage;
        }

        public override void CreateWheels(float i_CurrAirPressure, string i_ManufacturerName)
        {
            base.CreateWheels(k_NumberOfWheels, k_MaxAirPressure, i_CurrAirPressure, i_ManufacturerName);
        }

        public override string ShowFullDetails()
        {
            return string.Format(
                "{0}{1}Current battery time: {2}{1}Max battery time: {3}",
                base.ShowFullDetails(),
                System.Environment.NewLine,
                r_ElectricEnergySource.CurrentAmount,
                r_ElectricEnergySource.MaxAmount);
        }
    }
}
