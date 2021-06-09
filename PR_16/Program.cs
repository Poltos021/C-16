using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace PR_16_V_21
{
    class Student
    {
        [Required(ErrorMessage = "Укажите Имя")]
        [RegularExpression(@"^[А-Я][а-я]+\s[А-Я][а-я]*\s?[А-Я]?[а-я]*$", ErrorMessage = "Имя и фамилия должны начинаться с заглавной буквы")]
        public string Full_Name { get; set; }

        [Required(ErrorMessage = "Введите специализацию в формате **.**.**")]
        [RegularExpression(@"[0-9]{2}\.[0-9]{2}\.[0-9]{2}", ErrorMessage = "Введите специализацию в формате **.**.**")]
        public string Specialization { get; set; }

        [Range(1, 5, ErrorMessage = "укажите курс от 1 до 5")]
        public byte Course { get; set; }

        public override string ToString()
        {
            return $"{Full_Name}\n {Specialization}\n {Course}";
        }
    }

    class Student2 : IValidatableObject
    {
        public string Full_Name { get; set; }
        public string Specialization { get; set; }
        public byte Course { get; set; }
        public override string ToString()
        {
            return $"{Full_Name}\n {Specialization}\n {Course}";
        }


        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Full_Name == null)
                errors.Add(new ValidationResult("Введите ФИО"));
            else if (!Regex.IsMatch(Full_Name, @"^[А-Я][а-я]+\s[А-Я][а-я]*\s?[А-Я]?[а-я]*$"))
                errors.Add(new ValidationResult("Имя и фамилия должны начинаться с заглавной буквы"));

            if (Specialization == null)
                errors.Add(new ValidationResult("Введите специализацию в формате **.**.**"));
            else if (!Regex.IsMatch(Specialization, @"[0-9]{2}\.[0-9]{2}\.[0-9]{2}"))
                errors.Add(new ValidationResult("Введите специализацию в формате **.**.**"));
            if (Course <= 0 || Course > 5)
                errors.Add(new ValidationResult("укажите курс от 1 до 5"));

            return errors;

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
            int answer = 1;
            do
            {
                Console.WriteLine("Введите имя, фамилию. Специализацию в формате **.**.**. Укажите курс от 1 - 5");
                byte C;


                Student student = new Student
                {
                    Full_Name = Console.ReadLine(),
                    Specialization = Console.ReadLine(),
                    Course = byte.TryParse(Console.ReadLine(), out C) ? C : 0
                };



                Console.WriteLine(student + "\n");

                var result = new List<ValidationResult>();

                if (Validator.TryValidateObject(student, new ValidationContext(student), result, true))
                    students.Add(student);
                else
                    foreach (var error in result)
                        Console.WriteLine(error.ErrorMessage);
                Console.WriteLine($"\nЭлементов в колекции {students.Count}");

            }
            while (answer != 0);
            //--------------------------------------------------------------------------------------

            List<Student2> students1 = new List<Student2>();
            answer = 1;
            do
            {
                Console.WriteLine("Введите имя, фамилию. Специализацию в формате **.**.**. Укажите курс от 1 - 5");
                byte C;

                Student2 student1 = new Student2
                {
                    Full_Name = Console.ReadLine(),
                    Specialization = Console.ReadLine(),
                    Course = byte.TryParse(Console.ReadLine(), out C) ? C : 0
                };



                Console.WriteLine(student1 + "\n");

                var result1 = new List<ValidationResult>();

                if (Validator.TryValidateObject(student1, new ValidationContext(student1), result1, true))
                    students1.Add(student1);
                else
                    foreach (var error in result1)
                        Console.WriteLine(error.ErrorMessage);
                Console.WriteLine($"\nЭлементов в колекции {students1.Count}");

            }
            while (answer != 0);
            Console.ReadKey();
        }
    }
}
