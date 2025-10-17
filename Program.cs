using System;

namespace EngineProject
{
    // Базовый класс Двигатель
    public class Engine
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public double Power { get; set; } // в лошадиных силах
        public string Manufacturer { get; set; }

        public Engine(string name, string model, double power, string manufacturer)
        {
            Name = name;
            Model = model;
            Power = power;
            Manufacturer = manufacturer;
        }
    }

    // Производный класс Дизельный двигатель
    public class DieselEngine : Engine
    {
        public double FuelConsumption { get; set; } // расход топлива л/100км
        public bool HasTurbo { get; set; }

        public DieselEngine(string name, string model, double power, string manufacturer,
                          double fuelConsumption, bool hasTurbo)
            : base(name, model, power, manufacturer)
        {
            FuelConsumption = fuelConsumption;
            HasTurbo = hasTurbo;
        }
    }

    // Производный класс Двигатель внутреннего сгорания
    public class InternalCombustionEngine : Engine
    {
        public string FuelType { get; set; } // бензин, газ и т.д.
        public int CylindersCount { get; set; }

        public InternalCombustionEngine(string name, string model, double power, string manufacturer,
                                      string fuelType, int cylindersCount)
            : base(name, model, power, manufacturer)
        {
            FuelType = fuelType;
            CylindersCount = cylindersCount;
        }
    }

    // Производный класс Реактивный двигатель
    public class JetEngine : Engine
    {
        public double Thrust { get; set; } // тяга в кгс
        public string Application { get; set; } // применение: самолет, ракета и т.д.

        public JetEngine(string name, string model, double power, string manufacturer,
                        double thrust, string application)
            : base(name, model, power, manufacturer)
        {
            Thrust = thrust;
            Application = application;
        }
    }

    class Program
    {
        static void Main()
        {
            // Создание объектов различных типов двигателей
            var diesel = new DieselEngine("Cummins ISF", "ISF2.8", 120, "Cummins", 8.5, true);
            var combustion = new InternalCombustionEngine("Toyota 1ZZ-FE", "1ZZ", 110, "Toyota", "Бензин", 4);
            var jet = new JetEngine("Pratt & Whitney F135", "F135-PW-100", 43000, "Pratt & Whitney", 19100, "Истребитель F-35");
        }
    }
}