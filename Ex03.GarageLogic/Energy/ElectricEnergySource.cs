using Ex03.GarageLogic.Energy;

namespace Ex03.GarageLogic
{
    public class ElectricEnergySource : EnergySource
    {
        public ElectricEnergySource(float i_CurrentBatteryTime, float i_MaxBatteryTime)
            : base(i_CurrentBatteryTime, i_MaxBatteryTime)
        {
        }
        public void SetBatteryByPercentage(float i_FuelPercentage)
        {
            SetEnergyByPercentage(i_FuelPercentage);
        }
        public void ChargeBattery(float i_HoursToAdd)
        {
            AddEnergy(i_HoursToAdd);
        }
    }
}
