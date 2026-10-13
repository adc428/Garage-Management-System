using Ex03.GarageLogic;
using Ex03.GarageLogic.Exceptions;
using System;

namespace Ex03.GarageLogic
{
    public abstract class Truck : Vehicle
    {
        private bool m_IsCarryingRefrigeratedCargo;
        private float m_CargoVolume;
        private bool m_WereTruckDetailsSet;
    
        public Truck(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
            m_WereTruckDetailsSet = false;
        }
        public bool IsCarryingRefrigeratedCargo
        {
            get
            {
                return m_IsCarryingRefrigeratedCargo;
            }
        }
    
        public float CargoVolume
        {
            get
            {
                return m_CargoVolume;
            }
        }
        public override string ShowFullDetails()
        {
            if (!m_WereTruckDetailsSet)
            {
                throw new InvalidOperationException("Truck details were not initialized.");
            }
            return string.Format(
                "{0}{1}Is carrying refrigerated cargo: {2}{1}Cargo volume: {3}",
                base.ShowFullDetails(),
                System.Environment.NewLine,
                IsCarryingRefrigeratedCargo,
                CargoVolume);
        }
        public void SetTruckDetails(bool i_IsCarryingRefrigeratedCargo, float i_CargoVolume)
        {
            if (float.IsNaN(i_CargoVolume) || float.IsInfinity(i_CargoVolume) || i_CargoVolume < 0)
            {
                throw new ValueRangeException(0, float.MaxValue);
            }
    
            m_IsCarryingRefrigeratedCargo = i_IsCarryingRefrigeratedCargo;
            m_CargoVolume = i_CargoVolume;
            m_WereTruckDetailsSet = true;
        }
    }
}
