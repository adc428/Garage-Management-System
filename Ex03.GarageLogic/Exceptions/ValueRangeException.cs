using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Exceptions
{
    public class ValueRangeException : Exception
    {
        private readonly float r_MinValue;
        private readonly float r_MaxValue;
        public ValueRangeException(float i_MinValue, float i_MaxValue)
            : base(string.Format("Value must be between {0} and {1}.", i_MinValue, i_MaxValue))
        {
            r_MinValue = i_MinValue;
            r_MaxValue = i_MaxValue;
        }
        public float MinValue
        {
            get { return r_MinValue; }
        }
        public float MaxValue
        {
            get { return r_MaxValue; }
        }

    }
    
}
