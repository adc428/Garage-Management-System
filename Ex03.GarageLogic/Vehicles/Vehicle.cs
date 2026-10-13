using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle 
    {
        private readonly string r_ModelName;
        private readonly string r_LicenseNumber;
        private float m_EnergyPercentage;
        private List<Wheel> m_Wheels;

        public Vehicle(string i_LicenseNumber,string i_ModelName)
        {
            if (string.IsNullOrWhiteSpace(i_LicenseNumber) || string.IsNullOrWhiteSpace(i_ModelName))
            {
                throw new ArgumentException("License number and model name cannot be empty.");
            }
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            m_EnergyPercentage = 0;
            m_Wheels = new List<Wheel>();
        }
        public string ModelName
        {
            get { return r_ModelName; }
        }
        public string LicenseNumber
        {
            get { return r_LicenseNumber; } 
        }
        public float EnergyPercentage
        {
            get { return m_EnergyPercentage; }
            protected set { m_EnergyPercentage = value; }   
        }
        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
        }
        protected void CreateWheels(int i_NumberOfWheels , float i_MaxAirPressure , float i_CurrAirPressure , string i_ManufacturerName)
        {
            List<Wheel> newWheels = new List<Wheel>();
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                Wheel NewWheel = new Wheel(i_MaxAirPressure, i_ManufacturerName, i_CurrAirPressure);
                newWheels.Add(NewWheel);
            }
            m_Wheels.Clear();
            m_Wheels.AddRange(newWheels);

        }
        public abstract void CreateWheels(float i_CurrAirPressure, string i_ManufacturerName);
        public abstract void SetEnergyPercentage(float i_EnergyPercentage);


        public virtual string ShowFullDetails()
        {
            if (m_Wheels.Count == 0)
            {
                throw new InvalidOperationException("Vehicle wheels details were not initialized.");
            }

            StringBuilder details = new StringBuilder(string.Format(
                "The model name is: {0}{1}The license number is: {2}{1}Energy percentage is: {3}{1}Number of wheels is: {4}",
                r_ModelName,
                System.Environment.NewLine,
                r_LicenseNumber,
                m_EnergyPercentage,
                m_Wheels.Count));
            for (int i = 0; i < m_Wheels.Count; i++)
            {
                Wheel wheel = m_Wheels[i];
                details.AppendFormat(
                    "{0}Wheel {1}: manufacturer {2}, current air pressure {3}, max air pressure {4}",
                    Environment.NewLine, i + 1, wheel.ManufacturerName,
                    wheel.CurrentAirPressure, wheel.MaxAirPressure);
            }
            return details.ToString();
        }


    }
}
