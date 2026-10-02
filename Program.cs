using System;

namespace Assignment4
{
    // =========================================================================
    //  TYPES USED BY THE QUESTIONS (enums, structs, class)
    // =========================================================================

    // A simple class (reference type) used in Question 2
    class Box
    {
        public int Value;
    }

    // Used in Question 9
    enum WeekDays
    {
        Monday = 1,
        Tuesday,    // 2
        Wednesday,  // 3
        Thursday,   // 4
        Friday,     // 5
        Saturday,   // 6
        Sunday      // 7
    }

    // Used in Question 11
    enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    // Used in Questions 12 and 13.
    // [Flags] + powers of two (1, 2, 4, 8) lets us combine several permissions
    // inside ONE variable.
    [Flags]
    enum Permissions
    {
        None = 0,
        Read = 1,     // 0001
        Write = 2,    // 0010
        Delete = 4,   // 0100
        Execute = 8   // 1000
    }

    // Used in Question 14
    enum Colors
    {
        Red,
        Green,
        Blue
    }

    // Used in Questions 10 and 16
    struct Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    // Used in Question 15
    struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        // distance = sqrt( (x2-x1)^2 + (y2-y1)^2 )
        public double DistanceTo(Point other)
        {
            double dx = X - other.X;
            double dy = Y - other.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }

    internal class Program
    {
        // =====================================================================
        //  MAIN: a small menu so you can run any question you want
        // =====================================================================
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("\n===== Assignment 4 =====");
                int q = ReadInt("Enter question number (1-16), or 0 to exit: ");
                if (q == 0) break;
                Console.WriteLine();

                switch (q)
                {
                    case 1: Q1(); break;
                    case 2: Q2(); break;
                    case 3: Q3(); break;
                    case 4: Q4(); break;
                    case 5: Q5(); break;
                    case 6: Q6(); break;
                    case 7: Q7(); break;
                    case 8: Q8(); break;
                    case 9: Q9(); break;
                    case 10: Q10(); break;
                    case 11: Q11(); break;
                    case 12: Q12(); break;
                    case 13: Q13(); break;
                    case 14: Q14(); break;
                    case 15: Q15(); break;
                    case 16: Q16(); break;
                    default: Console.WriteLine("Please choose a number between 1 and 16."); break;
                }
            }
        }

        // =====================================================================
        //  HELPER METHODS
        // =====================================================================

        static int ReadInt(string message)
        {
            int value;
            Console.Write(message);
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Invalid input. " + message);
            }
            return value;
        }

        static double ReadDouble(string message)
        {
            double value;
            Console.Write(message);
            while (!double.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Invalid input. " + message);
            }
            return value;
        }

        static int ReadSize(string message = "Enter array size: ")
        {
            int size = ReadInt(message);
            while (size <= 0)
            {
                size = ReadInt("Size must be greater than 0. " + message);
            }
            return size;
        }

        static int[] ReadArray(int size, string name = "the array")
        {
            int[] arr = new int[size];
            Console.WriteLine($"Enter {size} elements for {name}:");
            for (int i = 0; i < size; i++)
            {
                arr[i] = ReadInt($"  [{i}] = ");
            }
            return arr;
        }

        // =====================================================================
        //  PART 1: FUNCTIONS (Q1 - Q8)
        // =====================================================================

        // 1- Explain the difference between passing (Value type parameters) by
        //    value and by reference then write a suitable c# example.
        //
        //    EXPLANATION:
        //    - By VALUE: the method receives a COPY of the variable. Changing the
        //      copy does NOT change the original variable.
        //    - By REFERENCE (ref): the method receives the variable ITSELF (its
        //      address). Changing it inside the method DOES change the original.
        static void Q1()
        {
            int a = 10;
            int b = 10;
            Console.WriteLine($"Before: a = {a}, b = {b}");

            AddFiveByValue(a);    // a is copied
            AddFiveByRef(ref b);  // b itself is sent

            Console.WriteLine($"After : a = {a} (by value -> unchanged), b = {b} (by ref -> changed)");
        }

        static void AddFiveByValue(int x)
        {
            x += 5;
            Console.WriteLine($"  inside AddFiveByValue: x = {x}");
        }

        static void AddFiveByRef(ref int x)
        {
            x += 5;
            Console.WriteLine($"  inside AddFiveByRef  : x = {x}");
        }

        // 2- Explain the difference between passing (Reference type parameters) by
        //    value and by reference then write a suitable c# example.
        //
        //    EXPLANATION:
        //    A reference-type variable holds the ADDRESS of an object.
        //    - By VALUE: the method gets a COPY OF THE ADDRESS. So it can change
        //      the object's CONTENT (the caller sees that), but if it makes the
        //      parameter point to a NEW object, the caller still points to the old one.
        //    - By REFERENCE (ref): the method gets the variable itself, so it can
        //      also make the caller's variable point to a NEW object.
        static void Q2()
        {
            Box box = new Box { Value = 1 };
            Console.WriteLine($"Start                     : box.Value = {box.Value}");

            ChangeContentByValue(box);
            Console.WriteLine($"After ChangeContentByValue: box.Value = {box.Value}  (content changed)");

            ReassignByValue(box);
            Console.WriteLine($"After ReassignByValue     : box.Value = {box.Value}  (NOT changed)");

            ReassignByRef(ref box);
            Console.WriteLine($"After ReassignByRef       : box.Value = {box.Value}  (now points to a new object)");
        }

        static void ChangeContentByValue(Box b)
        {
            b.Value = 100; // same object -> the caller sees it
        }

        static void ReassignByValue(Box b)
        {
            b = new Box { Value = 999 }; // only the local copy of the address changes
        }

        static void ReassignByRef(ref Box b)
        {
            b = new Box { Value = 999 }; // the caller's variable changes too
        }

        // 3- Write a c# Function that accept 4 parameters from user and return
        //    result of summation and subtracting of two numbers
        //    (my reading: 2 input numbers + 2 output parameters for the results)
        static void Q3()
        {
            int a = ReadInt("Enter first number: ");
            int b = ReadInt("Enter second number: ");

            SumAndSubtract(a, b, out int sum, out int difference);

            Console.WriteLine($"{a} + {b} = {sum}");
            Console.WriteLine($"{a} - {b} = {difference}");

            // Same job using a tuple (a cleaner modern alternative)
            var result = SumAndSubtractTuple(a, b);
            Console.WriteLine($"(tuple version) sum = {result.Sum}, difference = {result.Difference}");
        }

        static void SumAndSubtract(int a, int b, out int sum, out int difference)
        {
            sum = a + b;
            difference = a - b;
        }

        static (int Sum, int Difference) SumAndSubtractTuple(int a, int b)
        {
            return (a + b, a - b);
        }

        // 4- Write a program in C# Sharp to create a function to calculate the sum
        //    of the individual digits of a given number.
        //    Output should be like
        //    Enter a number: 25
        //    The sum of the digits of the number 25 is: 7
        static void Q4()
        {
            int number = ReadInt("Enter a number: ");
            int sum = SumOfDigits(number);
            Console.WriteLine($"The sum of the digits of the number {number} is: {sum}");
        }

        static int SumOfDigits(int number)
        {
            long n = Math.Abs((long)number); // ignore the minus sign
            int sum = 0;

            while (n > 0)
            {
                sum += (int)(n % 10); // take the last digit and add it
                n /= 10;              // remove the last digit
            }
            return sum;
        }

        // 5- Create a function named "IsPrime", which receives an integer number
        //    and retuns true if it is prime, or false if it is not:
        static void Q5()
        {
            int number = ReadInt("Enter a number: ");

            if (IsPrime(number))
                Console.WriteLine($"{number} is prime.");
            else
                Console.WriteLine($"{number} is not prime.");
        }

        static bool IsPrime(int number)
        {
            if (number < 2) return false;

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        // 6- Create a function named MinMaxArray, to return the minimum and maximum
        //    values stored in an array, using reference parameters
        static void Q6()
        {
            int size = ReadSize();
            int[] arr = ReadArray(size);

            MinMaxArray(arr, out int min, out int max);

            Console.WriteLine($"Minimum value = {min}");
            Console.WriteLine($"Maximum value = {max}");
        }

        static void MinMaxArray(int[] arr, out int min, out int max)
        {
            min = arr[0];
            max = arr[0];

            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] < min) min = arr[i];
                if (arr[i] > max) max = arr[i];
            }
        }

        // 7- Create an iterative (non-recursive) function to calculate the
        //    factorial of the number specified as parameter
        static void Q7()
        {
            int n = ReadInt("Enter a number: ");

            if (n < 0)
            {
                Console.WriteLine("Factorial is not defined for negative numbers.");
            }
            else if (n > 20)
            {
                Console.WriteLine("The number is too large (20! is the biggest that fits in a long).");
            }
            else
            {
                Console.WriteLine($"{n}! = {Factorial(n)}");
            }
        }

        static long Factorial(int n)
        {
            long result = 1; // 0! = 1 and 1! = 1

            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        // 8- Create a function named "ChangeChar" to modify a letter in a certain
        //    position (0 based) of a string, replacing it with a different letter
        static void Q8()
        {
            Console.Write("Enter a string: ");
            string text = Console.ReadLine() ?? "";

            if (text.Length == 0)
            {
                Console.WriteLine("The string is empty.");
                return;
            }

            int position = ReadInt($"Enter the position (0 to {text.Length - 1}): ");
            if (position < 0 || position >= text.Length)
            {
                Console.WriteLine("Position is out of range.");
                return;
            }

            Console.Write("Enter the new letter: ");
            string letterInput = Console.ReadLine() ?? "";
            if (letterInput.Length == 0)
            {
                Console.WriteLine("You did not enter a letter.");
                return;
            }

            string result = ChangeChar(text, position, letterInput[0]);
            Console.WriteLine($"Before: {text}");
            Console.WriteLine($"After : {result}");
        }

        static string ChangeChar(string text, int position, char newChar)
        {
            if (position < 0 || position >= text.Length)
                return text; // nothing to change

            // strings are IMMUTABLE, so we copy the chars into an array, edit, and rebuild
            char[] chars = text.ToCharArray();
            chars[position] = newChar;
            return new string(chars);
        }

        // =====================================================================
        //  PART 2: ENUM AND STRUCT (Q9 - Q16)
        // =====================================================================

        // 9- Create an enum called "WeekDays" with the days of the week (Monday to
        //    Sunday) as its members. Then, write a C# program that prints out all
        //    the days of the week using this enum.
        static void Q9()
        {
            foreach (WeekDays day in Enum.GetValues(typeof(WeekDays)))
            {
                Console.WriteLine($"{(int)day} - {day}");
            }
        }

        // 10- Define a struct "Person" with properties "Name" and "Age". Create an
        //     array of three "Person" objects and populate it with data. Then, write
        //     a C# program to display the details of all the persons in the array.
        static void Q10()
        {
            Person[] people = new Person[3];
            people[0] = new Person("Ali", 25);
            people[1] = new Person("Sara", 30);
            people[2] = new Person("Omar", 22);

            foreach (Person p in people)
            {
                Console.WriteLine($"Name: {p.Name}, Age: {p.Age}");
            }
        }

        // 11- Create an enum called "Season" with the four seasons (Spring, Summer,
        //     Autumn, Winter) as its members. Write a C# program that takes a season
        //     name as input from the user and displays the corresponding month range
        //     for that season. Note range for seasons
        //     ( spring march to may , summer june to august ,
        //       autumn September to November , winter December to February)
        static void Q11()
        {
            Console.Write("Enter a season name (Spring, Summer, Autumn, Winter): ");
            string input = Console.ReadLine() ?? "";

            if (Enum.TryParse(input, true, out Season season) && Enum.IsDefined(typeof(Season), season))
            {
                switch (season)
                {
                    case Season.Spring:
                        Console.WriteLine("Spring: March to May");
                        break;
                    case Season.Summer:
                        Console.WriteLine("Summer: June to August");
                        break;
                    case Season.Autumn:
                        Console.WriteLine("Autumn: September to November");
                        break;
                    case Season.Winter:
                        Console.WriteLine("Winter: December to February");
                        break;
                }
            }
            else
            {
                Console.WriteLine("This is not a valid season name.");
            }
        }

        // 12- Assign the following Permissions (Read, write, Delete, Execute) in a
        //     form of Enum.
        //     (the enum "Permissions" is declared at the top of the file;
        //      here we just print its members and their values)
        static void Q12()
        {
            foreach (Permissions p in Enum.GetValues(typeof(Permissions)))
            {
                Console.WriteLine($"{p,-8} = {(int)p}");
            }
        }

        // 13- Create Variable from previous Enum to Add and Remove Permission from
        //     variable, check if specific Permission is existed inside variable
        static void Q13()
        {
            Permissions p = Permissions.None;
            Console.WriteLine($"Start               : {p}");

            // ADD permissions with |=
            p |= Permissions.Read;
            p |= Permissions.Write | Permissions.Execute;
            Console.WriteLine($"After adding        : {p}  (value = {(int)p})");

            // CHECK if a permission exists
            bool canRead = (p & Permissions.Read) == Permissions.Read;
            bool canDelete = p.HasFlag(Permissions.Delete);
            Console.WriteLine($"Has Read?   {canRead}");
            Console.WriteLine($"Has Delete? {canDelete}");

            // REMOVE a permission with &= ~
            p &= ~Permissions.Write;
            Console.WriteLine($"After removing Write: {p}  (value = {(int)p})");
            Console.WriteLine($"Has Write?  {p.HasFlag(Permissions.Write)}");
        }

        // 14. Create an enum called "Colors" with the basic colors (Red, Green, Blue)
        //     as its members. Write a C# program that takes a color name as input
        //     from the user and displays a message indicating whether the input
        //     color is a primary color or not.
        static void Q14()
        {
            Console.Write("Enter a color name: ");
            string input = Console.ReadLine() ?? "";

            if (Enum.TryParse(input, true, out Colors color) && Enum.IsDefined(typeof(Colors), color))
                Console.WriteLine($"{color} is a primary color.");
            else
                Console.WriteLine($"{input} is NOT a primary color.");
        }

        // 15- Create a struct called "Point" to represent a 2D point with properties
        //     "X" and "Y". Write a C# program that takes two points as input from
        //     the user and calculates the distance between them.
        static void Q15()
        {
            Console.WriteLine("First point:");
            double x1 = ReadDouble("  X = ");
            double y1 = ReadDouble("  Y = ");
            Point p1 = new Point(x1, y1);

            Console.WriteLine("Second point:");
            double x2 = ReadDouble("  X = ");
            double y2 = ReadDouble("  Y = ");
            Point p2 = new Point(x2, y2);

            Console.WriteLine($"Distance = {p1.DistanceTo(p2):F2}");
        }

        // 16. Create a struct called "Person" with properties "Name" and "Age".
        //     Write a C# program that takes details of 3 persons as input from the
        //     user and displays the name and age of the oldest person.
        static void Q16()
        {
            Person[] people = new Person[3];

            for (int i = 0; i < people.Length; i++)
            {
                Console.WriteLine($"Person {i + 1}:");
                Console.Write("  Name: ");
                string name = Console.ReadLine() ?? "";
                int age = ReadInt("  Age : ");
                people[i] = new Person(name, age);
            }

            Person oldest = people[0]; // assume the first is the oldest
            for (int i = 1; i < people.Length; i++)
            {
                if (people[i].Age > oldest.Age)
                    oldest = people[i];
            }

            Console.WriteLine($"The oldest person is {oldest.Name}, Age: {oldest.Age}");
        }
    }
}
