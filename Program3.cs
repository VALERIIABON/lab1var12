using System;
using System.Collections.Generic;
using System.IO;

namespace edu_project
{
    public class Student
    {
        public string Id;
        public string FullName;
        public Dictionary<string, int> Marks = new Dictionary<string, int>();

        public Student(string id, string fullName) { Id = id; FullName = fullName; }

        public double GetAverageMark()
        {
            if (Marks.Count == 0) return 0;
            double sum = 0;
            foreach (var mark in Marks.Values) sum += mark;
            return sum / Marks.Count;
        }

        public override string ToString() => $"{FullName} (ID:{Id})";
    }

    public class Group
    {
        public string Name;
        public List<Student> Students = new List<Student>();

        public Group(string name) { Name = name; }

        public double GetAverageMark()
        {
            if (Students.Count == 0) return 0;
            double sum = 0;
            foreach (var student in Students) sum += student.GetAverageMark();
            return sum / Students.Count;
        }

        public override string ToString() => $"Группа {Name}";
    }

    public class Course
    {
        public int Number;
        public List<Group> Groups = new List<Group>();

        public Course(int number) { Number = number; }
        public override string ToString() => $"Курс {Number}";
    }

    public class Institute
    {
        public string Name;
        public List<string> Subjects = new List<string>();
        public List<Course> Courses = new List<Course>();

        public Institute(string name) { Name = name; }
        public override string ToString() => $"Институт {Name}";
    }

    class Program
    {
        static List<Institute> institutes = new List<Institute>();
        static int autoId = 1;

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            SeedData();

            while (true)
            {
                Console.WriteLine("=== СИСТЕМА УЧЁТА СТУДЕНТОВ ===");
                Console.WriteLine("1) Добавить институт");
                Console.WriteLine("2) Добавить курс");
                Console.WriteLine("3) Добавить группу");
                Console.WriteLine("4) Добавить студента");
                Console.WriteLine("5) Добавить предмет");
                Console.WriteLine("6) Поставить оценку");
                Console.WriteLine("7) Показать все данные");
                Console.WriteLine("8) Запрос: Студенты со средним баллом 4.5");
                Console.WriteLine("0) Выход\n");

                Console.Write("Выбор: ");
                var choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "0") break;

                switch (choice)
                {
                    case "1": AddInstitute(); break;
                    case "2": AddCourse(); break;
                    case "3": AddGroup(); break;
                    case "4": AddStudent(); break;
                    case "5": AddSubject(); break;
                    case "6": AddMark(); break;
                    case "7": ShowAllData(); break;
                    case "8": ExecuteQuery12(); break;
                    default: Console.WriteLine("Неверный выбор.\n"); break;
                }
            }
        }

        static Institute SelectInstitute()
        {
            if (institutes.Count == 0) { Console.WriteLine("Институтов нет.\n"); return null; }
            for (int i = 0; i < institutes.Count; i++) Console.WriteLine($"{i + 1}. {institutes[i].Name}");
            Console.Write("Выберите институт: ");
            return int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= institutes.Count ? institutes[index - 1] : null;
        }

        static Course SelectCourse(Institute institute)
        {
            if (institute.Courses.Count == 0) { Console.WriteLine("Курсов нет.\n"); return null; }
            for (int i = 0; i < institute.Courses.Count; i++) Console.WriteLine($"{i + 1}. {institute.Courses[i]}");
            Console.Write("Выберите курс: ");
            return int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= institute.Courses.Count ? institute.Courses[index - 1] : null;
        }

        static Group SelectGroup(Course course)
        {
            if (course.Groups.Count == 0) { Console.WriteLine("Групп нет.\n"); return null; }
            for (int i = 0; i < course.Groups.Count; i++) Console.WriteLine($"{i + 1}. {course.Groups[i]}");
            Console.Write("Выберите группу: ");
            return int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= course.Groups.Count ? course.Groups[index - 1] : null;
        }

        static Student SelectStudent(Group group)
        {
            if (group.Students.Count == 0) { Console.WriteLine("Студентов нет.\n"); return null; }
            for (int i = 0; i < group.Students.Count; i++) Console.WriteLine($"{i + 1}. {group.Students[i]}");
            Console.Write("Выберите студента: ");
            return int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= group.Students.Count ? group.Students[index - 1] : null;
        }

        static string SelectSubject(Institute institute)
        {
            if (institute.Subjects.Count == 0) { Console.WriteLine("Предметов нет.\n"); return null; }
            for (int i = 0; i < institute.Subjects.Count; i++) Console.WriteLine($"{i + 1}. {institute.Subjects[i]}");
            Console.Write("Выберите предмет: ");
            return int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= institute.Subjects.Count ? institute.Subjects[index - 1] : null;
        }

        static void AddInstitute()
        {
            Console.Write("Введите название института: ");
            var name = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(name)) { Console.WriteLine("Название не может быть пустым.\n"); return; }
            institutes.Add(new Institute(name));
            Console.WriteLine("Институт добавлен.\n");
        }

        static void AddCourse()
        {
            var institute = SelectInstitute(); if (institute == null) return;
            Console.Write("Введите номер курса (1-6): ");
            if (!int.TryParse(Console.ReadLine(), out int number) || number < 1 || number > 6) { Console.WriteLine("Неверный номер курса.\n"); return; }
            if (institute.Courses.Exists(c => c.Number == number)) { Console.WriteLine("Курс уже существует.\n"); return; }
            institute.Courses.Add(new Course(number));
            Console.WriteLine("Курс добавлен.\n");
        }

        static void AddGroup()
        {
            var institute = SelectInstitute(); if (institute == null) return;
            var course = SelectCourse(institute); if (course == null) return;
            Console.Write("Введите название группы: ");
            var name = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(name)) { Console.WriteLine("Название не может быть пустым.\n"); return; }
            if (course.Groups.Exists(g => g.Name == name)) { Console.WriteLine("Группа уже существует.\n"); return; }
            course.Groups.Add(new Group(name));
            Console.WriteLine("Группа добавлена.\n");
        }

        static void AddStudent()
        {
            var institute = SelectInstitute(); if (institute == null) return;
            var course = SelectCourse(institute); if (course == null) return;
            var group = SelectGroup(course); if (group == null) return;
            var id = $"S{autoId++:000}";
            Console.Write("Введите ФИО студента: ");
            var fullName = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(fullName)) { Console.WriteLine("ФИО не может быть пустым.\n"); return; }
            group.Students.Add(new Student(id, fullName));
            Console.WriteLine($"Студент {fullName} добавлен.\n");
        }

        static void AddSubject()
        {
            var institute = SelectInstitute(); if (institute == null) return;
            Console.Write("Введите название предмета: ");
            var subject = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(subject)) { Console.WriteLine("Название не может быть пустым.\n"); return; }
            if (institute.Subjects.Contains(subject)) { Console.WriteLine("Предмет уже существует.\n"); return; }
            institute.Subjects.Add(subject);
            Console.WriteLine("Предмет добавлен.\n");
        }

        static void AddMark()
        {
            var institute = SelectInstitute(); if (institute == null) return;
            var course = SelectCourse(institute); if (course == null) return;
            var group = SelectGroup(course); if (group == null) return;
            var student = SelectStudent(group); if (student == null) return;
            var subject = SelectSubject(institute); if (subject == null) return;
            Console.Write("Введите оценку (2-5): ");
            if (!int.TryParse(Console.ReadLine(), out int mark) || mark < 2 || mark > 5) { Console.WriteLine("Неверная оценка.\n"); return; }
            student.Marks[subject] = mark;
            Console.WriteLine($"Оценка {mark} поставлена.\n");
        }

        static void ShowAllData()
        {
            if (institutes.Count == 0) { Console.WriteLine("Данных нет.\n"); return; }
            foreach (var institute in institutes)
            {
                Console.WriteLine($"=== {institute.Name} ===");
                if (institute.Subjects.Count > 0) Console.WriteLine("Предметы: " + string.Join(", ", institute.Subjects));
                foreach (var course in institute.Courses)
                {
                    Console.WriteLine($"  Курс {course.Number}:");
                    foreach (var group in course.Groups)
                    {
                        Console.WriteLine($"    Группа {group.Name} (средний балл: {group.GetAverageMark():F2}):");
                        foreach (var student in group.Students)
                        {
                            Console.Write($"      {student.FullName}");
                            if (student.Marks.Count > 0)
                            {
                                var marks = new List<string>();
                                foreach (var mark in student.Marks) marks.Add($"{mark.Key}: {mark.Value}");
                                Console.Write(" - " + string.Join(", ", marks));
                            }
                            Console.WriteLine();
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        static void ExecuteQuery12()
        {
            var results = new List<string>();
            foreach (var institute in institutes)
            {
                foreach (var course in institute.Courses)
                {
                    foreach (var group in course.Groups)
                    {
                        if (Math.Abs(group.GetAverageMark() - 4.5) < 0.1)
                        {
                            foreach (var student in group.Students)
                            {
                                results.Add($"Студент: {student.FullName}, Группа: {group.Name}, Институт: {institute.Name}, Средний балл: {student.GetAverageMark():F2}");
                            }
                        }
                    }
                }
            }

            if (results.Count == 0) Console.WriteLine("Студентов со средним баллом 4.5 не найдено.\n");
            else
            {
                Console.WriteLine("=== РЕЗУЛЬТАТЫ ЗАПРОСА ===");
                foreach (var result in results) Console.WriteLine(result);
                Console.WriteLine();
            }

            try
            {
                using (StreamWriter writer = new StreamWriter("query12_results.txt", false, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine("=== СТУДЕНТЫ СО СРЕДНИМ БАЛЛОМ 4.5 ===");
                    writer.WriteLine($"Дата: {DateTime.Now}\n");
                    if (results.Count == 0) writer.WriteLine("Студентов не найдено.");
                    else foreach (var result in results) writer.WriteLine(result);
                }
                Console.WriteLine("Результаты сохранены в файл 'query12_results.txt'\n");
            }
            catch (Exception ex) { Console.WriteLine($"Ошибка записи: {ex.Message}\n"); }
        }

        static void SeedData()
        {
            var institute1 = new Institute("Институт информационных технологий");
            institute1.Subjects.AddRange(new[] { "Программирование", "Математика", "Базы данных" });
            var course1 = new Course(1);
            var group1 = new Group("ИТ-21");

            var student1 = new Student("S001", "Иванов Иван");
            student1.Marks["Программирование"] = 5; student1.Marks["Математика"] = 4; student1.Marks["Базы данных"] = 5;

            var student2 = new Student("S002", "Петров Петр");
            student2.Marks["Программирование"] = 5; student2.Marks["Математика"] = 4; student2.Marks["Базы данных"] = 4;

            group1.Students.AddRange(new[] { student1, student2 });
            course1.Groups.Add(group1);
            institute1.Courses.Add(course1);

            var institute2 = new Institute("Инженерный институт");
            institute2.Subjects.AddRange(new[] { "Физика", "Химия", "Механика" });
            var course2 = new Course(2);
            var group2 = new Group("ИНЖ-22");

            var student3 = new Student("S003", "Сидорова Анна");
            student3.Marks["Физика"] = 5; student3.Marks["Химия"] = 4; student3.Marks["Механика"] = 5;

            group2.Students.Add(student3);
            course2.Groups.Add(group2);
            institute2.Courses.Add(course2);

            institutes.AddRange(new[] { institute1, institute2 });
            autoId = 4;
            Console.WriteLine("Тестовые данные загружены.\n");
        }
    }
}