using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly float r_MaxAirPressure;
        private readonly string r_ManufacturerName;
        private float m_CurrentAirPressure;

        public Wheel(float i_MaxAirPressure, string i_ManufacturerName , float i_CurrentAirPressure)
        {
            if ((i_MaxAirPressure <= 0) || float.IsNaN(i_MaxAirPressure) || float.IsInfinity(i_MaxAirPressure))
            {
                throw new ArgumentException("invalid Max air pressure value");
            }
            if (i_CurrentAirPressure < 0 || i_CurrentAirPressure > i_MaxAirPressure || float.IsNaN(i_CurrentAirPressure))
            {
                throw new ValueRangeException(0, i_MaxAirPressure);
            }
            if(string.IsNullOrWhiteSpace(i_ManufacturerName))
            {
                throw new ArgumentException("Manufacturer Name cannot be empty");
            }
            r_MaxAirPressure = i_MaxAirPressure;
            r_ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = i_CurrentAirPressure;
        }
        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }    
        }
        public string ManufacturerName
        {
            get { return r_ManufacturerName; }
        }
        public float CurrentAirPressure
        {
            get {  return m_CurrentAirPressure;}
        }
        public void InflateToMax()
        {
            m_CurrentAirPressure = r_MaxAirPressure;
        }
        public void Inflate(float i_AirPressureToAdd)
        {
            if(i_AirPressureToAdd<= 0 || float.IsNaN(i_AirPressureToAdd) || i_AirPressureToAdd > r_MaxAirPressure - m_CurrentAirPressure)
            {
                throw new ValueRangeException(0, r_MaxAirPressure - m_CurrentAirPressure);
            }
            m_CurrentAirPressure+= i_AirPressureToAdd;
        }
    }
}
