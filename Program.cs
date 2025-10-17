using System;

namespace project
{
    // --- Интерфейсы (минимальные и простые) ---
    public interface IStartable { void Start(); void Stop(); }
    public interface IPowerMeasurable { double GetPower(); }
    public interface IPrintable { void Print(); }

    // --- Абстрактный базовый класс Двигатель ---
    public abstract class Engine : IStartable, IPowerMeasurable, IPrintable
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public double Power { get; set; } // в лошадиных силах
        public string Manufacturer { get; set; }

        protected Engine(string name, string model, double power, string manufacturer)
        {
            Name = name;
            Model = model;
            Power = power;
            Manufacturer = manufacturer;
        }

        // Полиморфизм: виртуальные методы переопределяются в потомках
        public virtual double GetPower() => Power;

        public virtual void Start()
        {
            Console.WriteLine($"{Name} запущен");
        }

        public virtual void Stop()
        {
            Console.WriteLine($"{Name} остановлен");
        }

        public virtual void Print()
        {
            Console.WriteLine($"Двигатель: {Name} [{Model}], мощность: {GetPower():0.###} л.с.");
        }
    }

    // --- Дизельный двигатель ---
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

        public override void Print()
        {
            Console.WriteLine($"Дизельный двигатель: {Name} [{Model}], мощность: {GetPower():0.###} л.с., " +
                            $"расход: {FuelConsumption} л/100км, турбо: {HasTurbo}");
        }
    }

    // --- Двигатель внутреннего сгорания ---
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

        public override double GetPower()
        {
            // Дополнительная логика: мощность зависит от количества цилиндров
            return Power * (1 + (CylindersCount - 4) * 0.05);
        }

        public override void Print()
        {
            Console.WriteLine($"ДВС: {Name} [{Model}], мощность: {GetPower():0.###} л.с., " +
                            $"топливо: {FuelType}, цилиндров: {CylindersCount}");
        }
    }

    // --- Реактивный двигатель ---
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

        public override double GetPower()
        {
            // Для реактивных двигателей используем тягу как основной показатель мощности
            return Thrust * 0.1; // упрощенная формула перевода тяги в л.с.
        }

        public override void Start()
        {
            Console.WriteLine($"{Name}: ЗАПУСК РЕАКТИВНОЙ ТЯГИ!");
        }

        public override void Stop()
        {
            Console.WriteLine($"{Name}: ОСТАНОВКА РЕАКТИВНОГО ДВИГАТЕЛЯ");
        }

        public override void Print()
        {
            Console.WriteLine($"Реактивный двигатель: {Name} [{Model}], тяга: {Thrust} кгс, " +
                            $"мощность: {GetPower():0.###} л.с., применение: {Application}");
        }
    }

    class Program
    {
        static void Main()
        {
            // Создаём объекты
            Engine diesel = new DieselEngine("Cummins ISF", "ISF2.8", 120, "Cummins", 8.5, true);
            Engine combustion = new InternalCombustionEngine("Toyota 1ZZ-FE", "1ZZ", 110, "Toyota", "Бензин", 4);
            Engine jet = new JetEngine("Pratt & Whitney F135", "F135-PW-100", 43000, "Pratt & Whitney", 19100, "Истребитель F-35");

            // Полиморфный вызов через базовый тип
            Engine[] engines = { diesel, combustion, jet };
            foreach (var engine in engines)
                engine.Print();

            Console.WriteLine();

            // Полиморфный вызов через интерфейс
            IPrintable[] toPrint = { diesel, combustion, jet };
            foreach (var pr in toPrint)
                pr.Print();

            Console.WriteLine();

            // Полиморфный расчёт мощности через интерфейс
            double sum = 0;
            foreach (var engine in engines)
                sum += ((IPowerMeasurable)engine).GetPower();
            Console.WriteLine("Суммарная мощность: " + sum.ToString("0.###") + " л.с.");

            Console.WriteLine();

            // Демонстрация полиморфного запуска
            IStartable[] toStart = { diesel, combustion, jet };
            foreach (var startable in toStart)
            {
                startable.Start();
                startable.Stop();
                Console.WriteLine();
            }
        }
    }
}