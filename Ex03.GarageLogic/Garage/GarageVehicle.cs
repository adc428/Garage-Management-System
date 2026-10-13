using Ex03.GarageLogic.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageVehicle
    {
        private Vehicle m_Vehicle;
        private readonly string r_OwnerName;
        private readonly string r_OwnerPhone;
        private eVehicleStatus m_VehicleStatus;

        public GarageVehicle(string i_OwnerName , string i_OwnerPhone , Vehicle i_Vehicle )
        {
            if (i_Vehicle == null)
            {
                throw new ArgumentNullException("i_Vehicle");
            }
            string error;
            if(!IsValid(i_OwnerName, i_OwnerPhone, out error))
            {
                throw new ArgumentException(error);
            }
            m_Vehicle = i_Vehicle;
            r_OwnerName = i_OwnerName.Trim();
            r_OwnerPhone = i_OwnerPhone.Trim();
            m_VehicleStatus = eVehicleStatus.InRepair;
        }
        public Vehicle Vehicle
        {
            get 
            {
                return m_Vehicle;
            }
        }
        public string OwnerName
        {
            get
            { 
                return r_OwnerName;
            }
        }
        public string OwnerPhone
        {
            get
            {
                return r_OwnerPhone;
            }
        }
        public eVehicleStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }
        }
        public void SetVehicleStatus(eVehicleStatus i_VehicleStatus)
        {
            if (!Enum.IsDefined(typeof(eVehicleStatus), i_VehicleStatus))
            {
                throw new ArgumentException("Invalid vehicle status.");
            }
            m_VehicleStatus = i_VehicleStatus;
        }

        public bool IsValid(string i_OwnerName, string i_OwnerPhone, out string error)
        {
            bool check = true;
            error = "";
            if (string.IsNullOrWhiteSpace(i_OwnerPhone) || string.IsNullOrWhiteSpace(i_OwnerName))
            {
                error = "input cannot be empty";
                check = false;
            }
            else if (!i_OwnerPhone.Any(char.IsDigit) || !i_OwnerPhone.All(c => char.IsDigit(c) || c == '+' || c == '-' || c == ' ' || c == '(' || c == ')'))
            {
                error = "Phone must contain digits and optional phone separators only";
                check = false;
            }
            else if (!i_OwnerName.Any(char.IsLetter) || i_OwnerName.Any(char.IsDigit))
            {
                error = "Name must contain chars only";
                check = false;
            }
            return check;

        }
    }
}
