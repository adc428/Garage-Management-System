using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;
using System;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;
        private bool m_WereMotorcycleDetailsSet;

        public Motorcycle(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
            m_WereMotorcycleDetailsSet = false;
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
        }

        public int EngineCapacity
        {
            get
            {
                return m_EngineCapacity;
            }
        }

        public void SetMotorcycleDetails(eLicenseType i_LicenseType, int i_EngineCapacity)
        {
            if (!Enum.IsDefined(typeof(eLicenseType), i_LicenseType))
            {
                throw new ArgumentException("Invalid license type.");
            }

            if (i_EngineCapacity < 0)
            {
                throw new ValueRangeException(0, int.MaxValue);
            }

            m_LicenseType = i_LicenseType;
            m_EngineCapacity = i_EngineCapacity;
            m_WereMotorcycleDetailsSet = true;
        }

        public override string ShowFullDetails()
        {
            if (!m_WereMotorcycleDetailsSet)
            {
                throw new InvalidOperationException("Motorcycle details were not initialized.");
            }

            return string.Format(
                "{0}{1}License type: {2}{1}Engine capacity: {3}",
                base.ShowFullDetails(),
                System.Environment.NewLine,
                LicenseType,
                EngineCapacity);
        }
    }
}
