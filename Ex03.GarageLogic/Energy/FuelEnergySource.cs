using Ex03.GarageLogic.Exceptions;
using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Energy
{
    public class FuelEnergySource : EnergySource
    {
        private readonly eFuelType r_FuelType;
        public FuelEnergySource(float i_CurrentAmount, float i_MaxAmount, eFuelType i_FuelType)
            : base(i_CurrentAmount, i_MaxAmount)
        {
            r_FuelType = i_FuelType;
        }

        public eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            } 
        }
        public void SetFuelByPercentage(float i_FuelPercentage)
        {
            SetEnergyByPercentage(i_FuelPercentage);
        }


        public void Refuel(float i_FuelAmountToAdd, eFuelType i_FuelType)
        {
            if (i_FuelType !=r_FuelType)
            {
                throw new ArgumentException("Invalid fuel type.");
            }
            AddEnergy(i_FuelAmountToAdd);
           
            
        }

    }

}
