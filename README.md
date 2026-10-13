# Project: Garage Management System in C# / .NET

# Description

This project implements a console-based garage management system in C#.  
The system manages five vehicle types, owner details, repair status, wheels, and fuel or battery capacity.  
The solution separates the console interface from the garage logic so the vehicle model can be reused with another interface.

# Components

Garage Manager (`Garage`, `GarageVehicle`):
Stores vehicles by license number, keeps owner information and repair status, and coordinates garage operations.

Vehicle Model (`Vehicle`, `Car`, `Motorcycle`, `Truck`):
Represents shared vehicle properties and the additional details required for each vehicle category.

Vehicle Factory (`VehicleCreator`):
Creates supported fuel and electric vehicles from their type names.

Energy Sources (`EnergySource`, `FuelEnergySource`, `ElectricEnergySource`):
Manage capacity, remaining energy, refueling, charging, and range validation.

Vehicle Capabilities (`IRefuelable`, `IChargeable`):
Define the operations available for fuel-powered and electric vehicles.

Wheel Model (`Wheel`):
Stores manufacturer and air-pressure values and supports inflation within the allowed range.

Console Interface (`ConsoleUI`):
Handles the main menu, manual entry, file loading, input validation, and error messages.

Range Validation (`ValueRangeException`):
Reports the allowed minimum and maximum when numeric input exceeds a valid range.

# Features

Support for fuel cars, electric cars, fuel motorcycles, electric motorcycles, and fuel trucks.

Vehicle loading from an included sample data file.

Manual vehicle entry with type-specific details.

License-number listing with optional repair-status filtering.

Repair-status updates and duplicate-vehicle handling.

Wheel inflation to the maximum allowed pressure.

Refueling with fuel-type and capacity validation.

Electric-vehicle charging using minutes entered by the user.

Full vehicle details, including owner, phone, repair status, and individual wheels.

# Files

## Solution and Projects
- `GarageManagementSystem.sln`
- `Ex03.ConsoleUI/Ex03.ConsoleUI.csproj`
- `Ex03.GarageLogic/Ex03.GarageLogic.csproj`

## Source Files
- `Ex03.ConsoleUI/Program.cs`, `ConsoleUI.cs`, `eMenuOption.cs`
- `Ex03.GarageLogic/Garage/Garage.cs`, `GarageVehicle.cs`
- `Ex03.GarageLogic/Vehicles/Vehicle.cs`
- `Ex03.GarageLogic/Car/Car.cs`, `FuelCar.cs`, `ElectricCar.cs`
- `Ex03.GarageLogic/Motorcycle/Motorcycle.cs`, `FuelMotorcycle.cs`, `ElectricMotorcycle.cs`
- `Ex03.GarageLogic/Truck/Truck.cs`, `FuelTruck.cs`
- `Ex03.GarageLogic/Energy/EnergySource.cs`, `FuelEnergySource.cs`, `ElectricEnergySource.cs`
- `Ex03.GarageLogic/Interfaces/IRefuelable.cs`, `IChargeable.cs`
- `Ex03.GarageLogic/Enums/eCarColor.cs`, `eFuelType.cs`, `eLicenseType.cs`, `eVehicleStatus.cs`
- `Ex03.GarageLogic/Exceptions/ValueRangeException.cs`
- `Ex03.GarageLogic/VehicleCreator.cs`, `Wheel.cs`
- Both projects include `Properties/AssemblyInfo.cs`; the console project also includes `App.config`.

## Sample Data
- `Ex03.ConsoleUI/VehiclesDB.txt`

# Requirements

- Windows
- Visual Studio 2022 or later with .NET desktop development tools
- .NET Framework 4.7.2 Developer Pack / targeting pack
- No third-party application dependencies

# How to Run

Open `GarageManagementSystem.sln` in Visual Studio.  
Set `Ex03.ConsoleUI` as the startup project, build the solution, and run it.

Choose option `1` to load the sample vehicles, or option `2` to add a vehicle manually.  
Choose option `9` to exit.

`VehiclesDB.txt` is copied to the executable directory during the build and is loaded from that directory, regardless of the current working directory.

# Notes

The garage keeps its state in memory for the current session. It does not save changes back to the sample file.

Each data row has ten comma-separated fields:

```text
VehicleType,LicensePlate,ModelName,EnergyPercentage,WheelManufacturer,CurrentAirPressure,OwnerName,OwnerPhone,SpecificDetail1,SpecificDetail2
```

The final two fields are color and door count for cars, license type and engine capacity for motorcycles, or refrigerated-cargo flag and cargo volume for trucks. Electric motorcycles may use `0` for engine capacity. Use a decimal point for numeric values in the file.

This is a coursework project demonstrating inheritance, polymorphism, composition, interfaces, collections, enums, and exception handling. It uses a console interface; no graphical forms or external screen files are required.
