using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Energy
{
    public abstract class EnergySource
    {
        private float m_CurrentAmount;
        private readonly float r_MaxAmount;

        public EnergySource(float i_currentAmount, float i_MaxAmount)
        {
            if (float.IsNaN(i_MaxAmount) || float.IsInfinity(i_MaxAmount) || i_MaxAmount <= 0)
            {
                throw new ArgumentException("Max Amount has to be a positive number");
            }
            if (float.IsNaN(i_currentAmount) || i_currentAmount < 0 || i_currentAmount > i_MaxAmount)
            {
                throw new ValueRangeException(0, i_MaxAmount);
            }
            m_CurrentAmount = i_currentAmount;
            r_MaxAmount= i_MaxAmount;
        }
        public float CurrentAmount

        { get { return m_CurrentAmount; } }
        public float MaxAmount

        { get { return r_MaxAmount; } }

        protected void SetEnergyByPercentage(float i_Percentage)
        {
            if (float.IsNaN(i_Percentage) || i_Percentage < 0 || i_Percentage > 100)
            {
                throw new ValueRangeException(0, 100);
            }
            m_CurrentAmount = r_MaxAmount * i_Percentage / 100;

        }

        protected void AddEnergy(float i_AmountToAdd)
        {
            float remainingAmount = r_MaxAmount - m_CurrentAmount;
            float roundingTolerance = r_MaxAmount * 0.000001f;
            if (float.IsNaN(i_AmountToAdd) || float.IsInfinity(i_AmountToAdd) || i_AmountToAdd <= 0
                || (i_AmountToAdd > remainingAmount && i_AmountToAdd - remainingAmount > roundingTolerance))
            {
                throw new ValueRangeException(0, r_MaxAmount - m_CurrentAmount);
            }

            m_CurrentAmount = Math.Min(r_MaxAmount, m_CurrentAmount + i_AmountToAdd);
        }

        
        
    }
}
