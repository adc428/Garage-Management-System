using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Interfaces
{
    public interface IRefuelable
    {
        void Refuel(eFuelType i_FuelType,float i_FuelAmountToAdd);
    }
}
