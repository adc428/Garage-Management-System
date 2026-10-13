using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        private eCarColor m_Color;
        private int m_DoorsNumber;
        private bool m_WereCarDetailsSet;

        public Car(string i_LicenseNumber, string i_ModelName)
        : base(i_LicenseNumber, i_ModelName)
        {
            m_WereCarDetailsSet = false;
        }

        public eCarColor Color
        {
            get { return m_Color; }
        }
        public int DoorsNumber
        {
            get { return m_DoorsNumber; }
        }
        public void SetCarDetails(eCarColor i_CarColor ,int i_DoorsNumber)
        {
            if (i_DoorsNumber < 2 || i_DoorsNumber > 5)
            {
                throw new ValueRangeException(2, 5);
            }
            if (!Enum.IsDefined(typeof(eCarColor), i_CarColor))
            {
                throw new ArgumentException("Invalid car color.");
            }
            m_Color = i_CarColor;
            m_DoorsNumber = i_DoorsNumber;
            m_WereCarDetailsSet = true;

        }
        public override string ShowFullDetails()
        {
            if (!m_WereCarDetailsSet)
            {
                throw new InvalidOperationException("Car details were not initialized.");
            }

            return string.Format(
                "{0}{1}Car color: {2}{1}Number of doors: {3}",
                base.ShowFullDetails(),
                System.Environment.NewLine,
                Color,
                DoorsNumber);
        }

    }
}
