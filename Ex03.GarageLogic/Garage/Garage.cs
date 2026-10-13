using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string,GarageVehicle> m_GarageVehicle;
         
        public Garage()
        {
            m_GarageVehicle = new Dictionary<string,GarageVehicle>();
        }
        public Dictionary<string, GarageVehicle> GarageVehicle
        {
            get { return m_GarageVehicle; }
        }
        private GarageVehicle getGarageVehicleByLicenseNumber(string i_LicenseNumber)
        {
            if (!IsInGarage(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle is not in the garage.");
            }

            return m_GarageVehicle[i_LicenseNumber];
        }
        public bool IsInGarage(string i_LicenseNumber)
        {
            if (string.IsNullOrWhiteSpace(i_LicenseNumber))
            {
                throw new ArgumentException("License number is invalid.");
            }
            return (m_GarageVehicle.ContainsKey(i_LicenseNumber));
        }
        public bool AddVehicleToGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhone)
        {
            if (i_Vehicle == null)
            {
                throw new ArgumentNullException("i_Vehicle");
            }
            string VehicLicenseNumber = i_Vehicle.LicenseNumber;
            bool WasAdded = false;
            if(IsInGarage(VehicLicenseNumber))
            {
                m_GarageVehicle[VehicLicenseNumber].SetVehicleStatus(eVehicleStatus.InRepair);
                WasAdded = false;
            }
            else
            {
                GarageVehicle garageVehicle = new GarageVehicle(i_OwnerName, i_OwnerPhone, i_Vehicle);
                m_GarageVehicle.Add(VehicLicenseNumber, garageVehicle);
                WasAdded = true;
            }
            return WasAdded;
        }
        public void ChargeVehicle(string i_LicenseNumber,float i_MinToCharge)
        {
            if(float.IsNaN(i_MinToCharge) || float.IsInfinity(i_MinToCharge) || i_MinToCharge <= 0)
            {
                throw new ArgumentException("minutes must be a positive number");
            }
            Vehicle currVehicle = getGarageVehicleByLicenseNumber(i_LicenseNumber).Vehicle;
            IChargeable chargeableVehicle = currVehicle as IChargeable;

            if (chargeableVehicle == null)
            {
                throw new ArgumentException("Vehicle is not electric.");
            }

            chargeableVehicle.ChargeBattery(i_MinToCharge);

        }
        public void RefuelVehicle(string i_LicenseNumber, float i_FuelAmountToAdd,eFuelType i_FuelType)
        {
            if (float.IsNaN(i_FuelAmountToAdd) || float.IsInfinity(i_FuelAmountToAdd) || i_FuelAmountToAdd <= 0)
            {
                throw new ArgumentException("Fuel Amount must be a positive number");
            }
            Vehicle currVehicle = getGarageVehicleByLicenseNumber(i_LicenseNumber).Vehicle;
            IRefuelable refuelableVehicle = currVehicle as IRefuelable;

            if (refuelableVehicle == null)
            {
                throw new ArgumentException("Vehicle is not fuel based.");
            }

            refuelableVehicle.Refuel(i_FuelType, i_FuelAmountToAdd);

        }
        public void InflatWheelsToMax(string i_LicenseNumber)
        {
            Vehicle currVehicle = getGarageVehicleByLicenseNumber(i_LicenseNumber).Vehicle;
            foreach (Wheel wheel in currVehicle.Wheels)
            {
                wheel.InflateToMax();
            }
        }
        public void ChangeVehicleStatus(eVehicleStatus i_VehicleStatus , string i_LicenseNumber)
        {
            GarageVehicle garageVehicle = getGarageVehicleByLicenseNumber(i_LicenseNumber);
            garageVehicle.SetVehicleStatus(i_VehicleStatus);
        }
        public List<string> GetLicenseNumbers(eVehicleStatus? i_eVehicleStatus)
        {
            if (i_eVehicleStatus.HasValue && !Enum.IsDefined(typeof(eVehicleStatus), i_eVehicleStatus.Value))
            {
                throw new ArgumentException("Invalid vehicle status filter.");
            }
            List<string> LicenseNumbers = new List<string>();
                foreach (GarageVehicle garageVehicle in m_GarageVehicle.Values)
                {
                    if(i_eVehicleStatus ==null ||garageVehicle.VehicleStatus == i_eVehicleStatus)
                    {
                        LicenseNumbers.Add(garageVehicle.Vehicle.LicenseNumber);
                    }
                }
            return LicenseNumbers;
        }
        public string GetVehicleFullDetails(string i_LicenseNumber)
        {
            if (!IsInGarage(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle is not in the garage.");
            }

            GarageVehicle garageVehicle = m_GarageVehicle[i_LicenseNumber];
            return string.Format(
                "Owner name: {0}{1}Owner phone: {2}{1}Vehicle status: {3}{1}{4}",
                garageVehicle.OwnerName,
                Environment.NewLine,
                garageVehicle.OwnerPhone,
                garageVehicle.VehicleStatus,
                garageVehicle.Vehicle.ShowFullDetails());
        }

    }
}
