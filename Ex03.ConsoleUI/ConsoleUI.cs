using Ex03.GarageLogic;
using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Ex03.GarageLogic.Enums;
using Ex03.GarageLogic.Interfaces;
using System.Collections.Specialized;
using System.Globalization;

namespace Ex03.ConsoleUI
{
    public class ConsoleUI
    {
        private readonly Garage m_Garage;

        public ConsoleUI()
        {
            m_Garage = new Garage();
        }

        public void Run()
        {
            eMenuOption selectedOption = 0;

            Console.WriteLine("Welcome to the garage system.");

            while (selectedOption != eMenuOption.Exit)
            {
                try
                {
                    showMenu();
                    selectedOption = getMenuOptionFromUser();
                    handleMenuOption(selectedOption);
                }
                catch (EndOfStreamException)
                {
                    Console.WriteLine("Input ended. Goodbye.");
                    break;
                }
                catch (ValueRangeException ex)
                {
                    Console.WriteLine(
                        string.Format(
                            "The value is out of range. Valid range is between {0} and {1}.",
                            ex.MinValue,
                            ex.MaxValue));
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void showMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Load vehicles from file");
            Console.WriteLine("2. Add vehicle to garage");
            Console.WriteLine("3. Display license numbers");
            Console.WriteLine("4. Change vehicle status");
            Console.WriteLine("5. Inflate wheels to max");
            Console.WriteLine("6. Refuel vehicle");
            Console.WriteLine("7. Charge vehicle");
            Console.WriteLine("8. Display vehicle details");
            Console.WriteLine("9. Exit");
        }

        private eMenuOption getMenuOptionFromUser()
        {
            string userInput = null;
            int selectedOption = 0;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.Write("Your choice: ");
                userInput = readRequiredLine();

                if (int.TryParse(userInput, out selectedOption)
                   && selectedOption >= (int)eMenuOption.LoadVehiclesFromFile
                   && selectedOption <= (int)eMenuOption.Exit)
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 9.");
                }
            }

            return (eMenuOption)selectedOption;
        }

        private void handleMenuOption(eMenuOption i_SelectedOption)
        {
            switch (i_SelectedOption)
            {
                case eMenuOption.LoadVehiclesFromFile:
                    loadVehiclesFromFile();
                    break;
                case eMenuOption.AddVehicle:
                    addVehicleToGarage();
                    break;
                case eMenuOption.DisplayLicenseNumbers:
                    displayLicenseNumbers();
                    break;
                case eMenuOption.ChangeVehicleStatus:
                    changeVehicleStatus();
                    break;
                case eMenuOption.InflateWheelsToMax:
                    inflateWheelsToMax();
                    break;
                case eMenuOption.RefuelVehicle:
                    refuelVehicle();
                    break;
                case eMenuOption.ChargeVehicle:
                    chargeVehicle();
                    break;
                case eMenuOption.DisplayVehicleDetails:
                    displayVehicleDetails();
                    break;
                case eMenuOption.Exit:
                    Console.WriteLine("Goodbye.");
                    break;
            }
        }
        private void chargeVehicle()
        {
            string licenseNumber = getNotEmptyStringFromUser("Enter the license number of the vehicle:");
            float minutesToAdd = getFloatFromUser("Enter charging time in minutes:");
            m_Garage.ChargeVehicle(licenseNumber, minutesToAdd);
            Console.WriteLine("Vehicle was charged successfully.");
        }
        private void refuelVehicle()
        {
            string licenseNumber = getNotEmptyStringFromUser("Enter the license number of the vehicle:");
            float amountToAdd = getFloatFromUser("Enter fuel amount to add in liters:");
            eFuelType fuelType = getFuelTypeFromUser();
            m_Garage.RefuelVehicle(licenseNumber, amountToAdd, fuelType);
            Console.WriteLine("Vehicle was refueled successfully.");

        }
        private eFuelType getFuelTypeFromUser()
        {
            string userInput = null;
            int selectedOption = 0;
            bool isValidInput = false;
            eFuelType fuelType = 0;

            Console.WriteLine("Choose fuel type:");
            Console.WriteLine("1. Octan95");
            Console.WriteLine("2. Octan96");
            Console.WriteLine("3. Octan98");
            Console.WriteLine("4. Soler");

            while (!isValidInput)
            {
                Console.Write("Your choice: ");
                userInput = readRequiredLine();

                if (int.TryParse(userInput, out selectedOption) && selectedOption >= 1 && selectedOption <= 4)
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                }
            }

            switch (selectedOption)
            {
                case 1:
                    fuelType = eFuelType.Octan95;
                    break;
                case 2:
                    fuelType = eFuelType.Octan96;
                    break;
                case 3:
                    fuelType = eFuelType.Octan98;
                    break;
                case 4:
                    fuelType = eFuelType.Soler;
                    break;
            }

            return fuelType;
        }
        private void displayVehicleDetails()
        {
            string licenseNumber = null;
            string details;
            Console.WriteLine("Enter the license number of the vehicle:");
            licenseNumber = getNotEmptyStringFromUser("License number:");
            details = m_Garage.GetVehicleFullDetails(licenseNumber);
            Console.WriteLine(details);
        }
        private void inflateWheelsToMax()
        {
            string licenseNumber = null;

            Console.WriteLine("Enter the license number of the vehicle:");
            licenseNumber = getNotEmptyStringFromUser("License number:");

            m_Garage.InflatWheelsToMax(licenseNumber);

            Console.WriteLine("Vehicle wheels were inflated to max successfully.");
        }
        private void changeVehicleStatus()
        {
            string licenseNumber;
            eVehicleStatus? vehicleStatus = null;
            Console.WriteLine("Enter the license number of the vehicle you want to change:");
            licenseNumber = getNotEmptyStringFromUser("License number:");
            vehicleStatus = getVehicleStatusFromUser(false);
            m_Garage.ChangeVehicleStatus(vehicleStatus.Value, licenseNumber);
            Console.WriteLine("Vehicle status was changed successfully.");
        }
        private void displayLicenseNumbers()
        {
            List<string> LicenseNumbersList;
            eVehicleStatus? vehicleStatus = null;
            vehicleStatus = getVehicleStatusFromUser(true);
            LicenseNumbersList=m_Garage.GetLicenseNumbers(vehicleStatus);
            if(LicenseNumbersList.Count==0)
            {
                Console.WriteLine("There are not license numbers to display");
            }
            else
            {
                Console.WriteLine("License numbers:");
                foreach (string licenseNumber in LicenseNumbersList)
                {
                    Console.WriteLine(licenseNumber);
                }
            }
        }
        private eVehicleStatus? getVehicleStatusFromUser(bool i_AllowNoFilter)
        {
            eVehicleStatus? vehicleStatus = null;
            int selectedOption=0;
            bool IsValid = false;
            string userInput = null;
            int minOption = 1;
            int maxOption = 3;
            Console.WriteLine("Choose vehicle status:");
            
            if (i_AllowNoFilter)
            {
                Console.WriteLine("1. No filter");
                Console.WriteLine("2. In repair");
                Console.WriteLine("3. Repaired");
                Console.WriteLine("4. Paid");
                maxOption = 4;
            }
            else
            {
                Console.WriteLine("1. In repair");
                Console.WriteLine("2. Repaired");
                Console.WriteLine("3. Paid");
            }
            while (!IsValid)
            {
                Console.Write("Your choice: ");
                userInput = readRequiredLine();
                if (int.TryParse(userInput, out selectedOption) && selectedOption >= minOption && selectedOption <= maxOption)
                {
                    IsValid = true;
                }
                else
                {
                    Console.WriteLine(string.Format("Invalid choice. Please enter a number between {0} and {1}.", minOption, maxOption));
                }

            }
            if (i_AllowNoFilter)
            {
                switch (selectedOption)
                {
                    case 2:
                        vehicleStatus = eVehicleStatus.InRepair;
                        break;
                    case 3:
                        vehicleStatus = eVehicleStatus.Repaired;
                        break;
                    case 4:
                        vehicleStatus = eVehicleStatus.Paid;
                        break;
                }

            }
            else
            {
                switch (selectedOption)
                {
                    case 1:
                        vehicleStatus = eVehicleStatus.InRepair;
                        break;
                    case 2:
                        vehicleStatus = eVehicleStatus.Repaired;
                        break;
                    case 3:
                        vehicleStatus = eVehicleStatus.Paid;
                        break;
                }
            }
            return vehicleStatus;

        }
            
        private void addVehicleToGarage()
        {
            string licenseNumber = null;

            licenseNumber = getNotEmptyStringFromUser("Enter the license number of the vehicle:");
            if (m_Garage.IsInGarage(licenseNumber))
            {
                m_Garage.ChangeVehicleStatus(eVehicleStatus.InRepair, licenseNumber);
                Console.WriteLine("Vehicle already exists in the garage. Its status was changed to InRepair.");
            }
            else
            {
                addNewVehicleToGarage(licenseNumber);
            }
        }
        private void addNewVehicleToGarage(string i_LicenseNumber)
        {
            string vehicleType = null;
            string modelName = null;
            float energyPercentage = 0;
            string wheelManufacturer = null;
            float currentAirPressure = 0;
            string ownerName = null;
            string ownerPhone = null;
            Vehicle vehicle = null;

            Console.WriteLine("Enter vehicle type:");
            Console.WriteLine("Supported types: " + string.Join(", ", VehicleCreator.SupportedTypes));
            vehicleType = readRequiredLine().Trim();

            modelName = getNotEmptyStringFromUser("Enter model name:");

            vehicle = VehicleCreator.CreateVehicle(vehicleType, i_LicenseNumber, modelName);

            if (vehicle == null)
            {
                throw new ArgumentException("Unsupported vehicle type.");
            }

            energyPercentage = getFloatFromUser("Enter energy percentage:");

            wheelManufacturer = getNotEmptyStringFromUser("Enter wheel manufacturer name for all wheels:");

            currentAirPressure = getFloatFromUser("Enter current air pressure for all wheels:");

            ownerName = getNotEmptyStringFromUser("Enter owner name:");

            ownerPhone = getNotEmptyStringFromUser("Enter Phone Number");

            vehicle.SetEnergyPercentage(energyPercentage);
            vehicle.CreateWheels(currentAirPressure, wheelManufacturer);
            setSpecificVehicleDetailsFromUser(vehicle);

            m_Garage.AddVehicleToGarage(vehicle, ownerName, ownerPhone);

            Console.WriteLine("Vehicle was added to the garage successfully.");
        }
        private float getFloatFromUser(string i_Message)
        {
            string userInput = null;
            float number = 0;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.WriteLine(i_Message);
                userInput = readRequiredLine();

                if (float.TryParse(userInput, out number) && !float.IsNaN(number) && !float.IsInfinity(number))
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }

            return number;
        }
        private void loadVehiclesFromFile()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "VehiclesDB.txt");
            string[] vehicleLines = File.ReadAllLines(filePath);
            List<GarageVehicle> parsedVehicles = new List<GarageVehicle>();
            for (int i = 0; i < vehicleLines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(vehicleLines[i]))
                {
                    continue;
                }
                try
                {
                    parsedVehicles.Add(createVehicleFromFileLine(vehicleLines[i]));
                }
                catch (Exception ex) when (ex is FormatException || ex is ArgumentException || ex is ValueRangeException || ex is OverflowException)
                {
                    throw new FormatException(string.Format("Invalid vehicle data on line {0}: {1}", i + 1, ex.Message), ex);
                }
            }
            foreach (GarageVehicle garageVehicle in parsedVehicles)
            {
                m_Garage.AddVehicleToGarage(garageVehicle.Vehicle, garageVehicle.OwnerName, garageVehicle.OwnerPhone);
            }
            Console.WriteLine("Vehicles were loaded from file ({0} records).", parsedVehicles.Count);
        }

        private GarageVehicle createVehicleFromFileLine(string i_Line)
        {
            const int k_MinimumNumberOfDetails = 10;
            const int k_VehicleTypeIndex = 0;
            const int k_LicenseNumberIndex = 1;
            const int k_ModelNameIndex = 2;
            const int k_EnergyPercentageIndex = 3;
            const int k_WheelManufacturerIndex = 4;
            const int k_CurrentAirPressureIndex = 5;
            const int k_OwnerNameIndex = 6;
            const int k_OwnerPhoneIndex = 7;

            string[] vehicleDetails = i_Line.Split(',').Select(detail => detail.Trim()).ToArray();

            if (vehicleDetails.Length != k_MinimumNumberOfDetails)
            {
                throw new FormatException("Invalid vehicle line format.");
            }

            string vehicleType = vehicleDetails[k_VehicleTypeIndex];
            string licenseNumber = vehicleDetails[k_LicenseNumberIndex];
            string modelName = vehicleDetails[k_ModelNameIndex];
            float energyPercentage = float.Parse(vehicleDetails[k_EnergyPercentageIndex], CultureInfo.InvariantCulture);
            string wheelManufacturer = vehicleDetails[k_WheelManufacturerIndex];
            float currentAirPressure = float.Parse(vehicleDetails[k_CurrentAirPressureIndex], CultureInfo.InvariantCulture);
            string ownerName = vehicleDetails[k_OwnerNameIndex];
            string ownerPhone = vehicleDetails[k_OwnerPhoneIndex];

            Vehicle vehicle = VehicleCreator.CreateVehicle(vehicleType, licenseNumber, modelName);

            if (vehicle == null)
            {
                throw new ArgumentException("Unsupported vehicle type.");
            }

            vehicle.SetEnergyPercentage(energyPercentage);
            vehicle.CreateWheels(currentAirPressure, wheelManufacturer);
            setSpecificVehicleDetails(vehicle, vehicleDetails[8], vehicleDetails[9]);
            return new GarageVehicle(ownerName, ownerPhone, vehicle);
        }
        private void setSpecificVehicleDetails(Vehicle i_CurrVehicle, string FirstSpecificDetailIndex , string SecondSpecificDetailIndex)
        {
            
            Car car = i_CurrVehicle as Car;
            Truck truck = i_CurrVehicle as Truck;
            Motorcycle motorcycle = i_CurrVehicle as Motorcycle;

            if (car != null)
            {
                eCarColor color = (eCarColor)Enum.Parse(typeof(eCarColor), FirstSpecificDetailIndex, true);
                int doorsNumber = int.Parse(SecondSpecificDetailIndex, CultureInfo.InvariantCulture);

                car.SetCarDetails(color, doorsNumber);
            }
            else if (truck != null)
            {
                bool isCarryingRefrigeratedCargo = bool.Parse(FirstSpecificDetailIndex);
                float cargoVolume = float.Parse(SecondSpecificDetailIndex, CultureInfo.InvariantCulture);

                truck.SetTruckDetails(isCarryingRefrigeratedCargo, cargoVolume);
            }
            else if (motorcycle != null)
            {
                eLicenseType licenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), FirstSpecificDetailIndex, true);
                int engineCapacity = int.Parse(SecondSpecificDetailIndex, CultureInfo.InvariantCulture);

                motorcycle.SetMotorcycleDetails(licenseType, engineCapacity);
            }
            else
            {
                throw new ArgumentException("Unsupported vehicle type.");
            }
        }
        private string getNotEmptyStringFromUser(string i_Message)
        {
            string userInput = null;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.WriteLine(i_Message);
                userInput = readRequiredLine();

                if (!string.IsNullOrWhiteSpace(userInput))
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Input cannot be empty.");
                }
            }

            return userInput.Trim();
        }

        private void setSpecificVehicleDetailsFromUser(Vehicle i_Vehicle)
        {
            Car car = i_Vehicle as Car;
            Motorcycle motorcycle = i_Vehicle as Motorcycle;
            Truck truck = i_Vehicle as Truck;
            if (car != null)
            {
                eCarColor color = getEnumFromUser<eCarColor>("Enter car color (Red, Yellow, Black, Silver):");
                int doors = getIntFromUser("Enter number of doors (2-5):");
                car.SetCarDetails(color, doors);
            }
            else if (motorcycle != null)
            {
                eLicenseType license = getEnumFromUser<eLicenseType>("Enter license type (A, A2, AB, B1):");
                int capacity = getIntFromUser("Enter engine capacity in cc (0 for an electric motorcycle):");
                motorcycle.SetMotorcycleDetails(license, capacity);
            }
            else if (truck != null)
            {
                bool refrigerated;
                while (!bool.TryParse(getNotEmptyStringFromUser("Does it carry refrigerated cargo? (true/false):"), out refrigerated))
                {
                    Console.WriteLine("Invalid input. Please enter true or false.");
                }
                float volume = getFloatFromUser("Enter cargo volume:");
                truck.SetTruckDetails(refrigerated, volume);
            }
            else
            {
                throw new ArgumentException("Unsupported vehicle type.");
            }
        }

        private T getEnumFromUser<T>(string i_Message) where T : struct
        {
            T result;
            while (!Enum.TryParse(getNotEmptyStringFromUser(i_Message), true, out result)
                || !Enum.IsDefined(typeof(T), result))
            {
                Console.WriteLine("Invalid choice. Please enter one of the listed names.");
            }
            return result;
        }

        private int getIntFromUser(string i_Message)
        {
            int result;
            while (!int.TryParse(getNotEmptyStringFromUser(i_Message), out result))
            {
                Console.WriteLine("Invalid input. Please enter a whole number.");
            }
            return result;
        }

        private string readRequiredLine()
        {
            string line = Console.ReadLine();
            if (line == null)
            {
                throw new EndOfStreamException();
            }
            return line;
        }
    }
}
