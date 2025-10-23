using System;

// Структура для хранения комплексных чисел
struct ComplexNumber
{
    public double Real;      // Вещественная часть
    public double Imaginary; // Мнимая часть

    // Конструктор для создания комплексного числа
    public ComplexNumber(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    // Метод сложения комплексных чисел
    public void Add(ComplexNumber other)
    {
        Real += other.Real;
        Imaginary += other.Imaginary;
    }

    // Метод вычитания комплексных чисел
    public void Subtract(ComplexNumber other)
    {
        Real -= other.Real;
        Imaginary -= other.Imaginary;
    }

    // Метод умножения комплексных чисел
    public void Multiply(ComplexNumber other)
    {
        double tempReal = Real * other.Real - Imaginary * other.Imaginary;
        double tempImaginary = Real * other.Imaginary + Imaginary * other.Real;
        Real = tempReal;
        Imaginary = tempImaginary;
    }

    // Метод деления комплексных чисел
    public void Divide(ComplexNumber other)
    {
        double denominator = other.Real * other.Real + other.Imaginary * other.Imaginary;

        if (denominator == 0)
        {
            Console.WriteLine("Ошибка: деление на ноль!");
            return;
        }

        double tempReal = (Real * other.Real + Imaginary * other.Imaginary) / denominator;
        double tempImaginary = (Imaginary * other.Real - Real * other.Imaginary) / denominator;
        Real = tempReal;
        Imaginary = tempImaginary;
    }

    // Метод нахождения модуля комплексного числа
    public double GetMagnitude()
    {
        return Math.Sqrt(Real * Real + Imaginary * Imaginary);
    }

    // Метод нахождения аргумента комплексного числа (в радианах)
    public double GetArgument()
    {
        return Math.Atan2(Imaginary, Real);
    }

    // Метод возврата вещественной части
    public double GetRealPart()
    {
        return Real;
    }

    // Метод возврата мнимой части
    public double GetImaginaryPart()
    {
        return Imaginary;
    }

    // Метод вывода комплексного числа
    public void Print()
    {
        if (Imaginary >= 0)
            Console.WriteLine($"{Real} + {Imaginary}i");
        else
            Console.WriteLine($"{Real} - {Math.Abs(Imaginary)}i");
    }
}

class Program
{
    static void Main()
    {
        // Начальное комплексное число (0 + 0i)
        ComplexNumber number = new ComplexNumber(0, 0);

        Console.WriteLine("Калькулятор комплексных чисел");
        Console.WriteLine("Текущее число: ");
        number.Print();

        // Бесконечный цикл для меню
        while (true)
        {
            Console.WriteLine("\n=== МЕНЮ ===");
            Console.WriteLine("S - Задать новое комплексное число");
            Console.WriteLine("+ - Сложение");
            Console.WriteLine("- - Вычитание");
            Console.WriteLine("* - Умножение");
            Console.WriteLine("/ - Деление");
            Console.WriteLine("M - Модуль числа");
            Console.WriteLine("A - Аргумент числа");
            Console.WriteLine("R - Вещественная часть");
            Console.WriteLine("I - Мнимая часть");
            Console.WriteLine("P - Вывести текущее число");
            Console.WriteLine("Q - Выход");
            Console.Write("Выберите команду: ");

            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Неизвестная команда");
                continue;
            }

            char command = input[0];

            switch (command)
            {
                case 'S':
                case 's':
                    // Задание нового комплексного числа
                    Console.Write("Введите вещественную часть: ");
                    double real = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Введите мнимую часть: ");
                    double imaginary = Convert.ToDouble(Console.ReadLine());

                    number = new ComplexNumber(real, imaginary);
                    Console.Write("Новое число: ");
                    number.Print();
                    break;

                case '+':
                    // Сложение
                    Console.Write("Введите вещественную часть второго числа: ");
                    double real2 = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Введите мнимую часть второго числа: ");
                    double imaginary2 = Convert.ToDouble(Console.ReadLine());

                    ComplexNumber addNumber = new ComplexNumber(real2, imaginary2);
                    number.Add(addNumber);
                    Console.Write("Результат сложения: ");
                    number.Print();
                    break;

                case '-':
                    // Вычитание
                    Console.Write("Введите вещественную часть второго числа: ");
                    double real3 = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Введите мнимую часть второго числа: ");
                    double imaginary3 = Convert.ToDouble(Console.ReadLine());

                    ComplexNumber subNumber = new ComplexNumber(real3, imaginary3);
                    number.Subtract(subNumber);
                    Console.Write("Результат вычитания: ");
                    number.Print();
                    break;

                case '*':
                    // Умножение
                    Console.Write("Введите вещественную часть второго числа: ");
                    double real4 = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Введите мнимую часть второго числа: ");
                    double imaginary4 = Convert.ToDouble(Console.ReadLine());

                    ComplexNumber mulNumber = new ComplexNumber(real4, imaginary4);
                    number.Multiply(mulNumber);
                    Console.Write("Результат умножения: ");
                    number.Print();
                    break;

                case '/':
                    // Деление
                    Console.Write("Введите вещественную часть второго числа: ");
                    double real5 = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Введите мнимую часть второго числа: ");
                    double imaginary5 = Convert.ToDouble(Console.ReadLine());

                    ComplexNumber divNumber = new ComplexNumber(real5, imaginary5);
                    number.Divide(divNumber);
                    Console.Write("Результат деления: ");
                    number.Print();
                    break;

                case 'M':
                case 'm':
                    // Модуль числа
                    double magnitude = number.GetMagnitude();
                    Console.WriteLine($"Модуль числа: {magnitude:F4}");
                    break;

                case 'A':
                case 'a':
                    // Аргумент числа
                    double argument = number.GetArgument();
                    Console.WriteLine($"Аргумент числа (в радианах): {argument:F4}");
                    break;

                case 'R':
                case 'r':
                    // Вещественная часть
                    double realPart = number.GetRealPart();
                    Console.WriteLine($"Вещественная часть: {realPart}");
                    break;

                case 'I':
                case 'i':
                    // Мнимая часть
                    double imaginaryPart = number.GetImaginaryPart();
                    Console.WriteLine($"Мнимая часть: {imaginaryPart}");
                    break;

                case 'P':
                case 'p':
                    // Вывод текущего числа
                    Console.Write("Текущее число: ");
                    number.Print();
                    break;

                case 'Q':
                case 'q':
                    // Выход из программы
                    Console.WriteLine("Выход из программы...");
                    return;

                default:
                    Console.WriteLine("Неизвестная команда");
                    break;
            }
        }
    }
}