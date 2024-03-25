
internal class Program
{
    static void Main()
    {
        var nums = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 10, 12, 14, 15, 17 };

        // var evens = nums.FindAll(i => i % 2 == 0);

        var evens = nums.AsParallel().Where(i => i % 2 == 0);

        /*
        foreach (int i in nums) {
            if (i % 2 == 0) evens.Add(i);
        }
        */

        foreach (var n in evens) {
            Console.WriteLine(n);
        }

        Console.WriteLine("===========");

        nums.Add(12);

        foreach (var n in evens) {
            Console.WriteLine(n);
        }

        /*
        var people = new List<Person> {
            new Person(FirstName: "Bill", LastName: "Gates"),
            new Person(FirstName: "Joe", LastName: "Gates"),
            new Person(FirstName: "Jane", LastName: "Gates")
        };

        foreach(var p in people) Console.WriteLine($"{p.FirstName} {p.LastName}");

        Console.WriteLine("==================");

        var newPeople = people
            .Where(p => p.FirstName.StartsWith("J"))
            .Select(p => p with { LastName = "Smith" }
        );

        foreach (var p in newPeople) Console.WriteLine($"{p.FirstName} {p.LastName}");
        */

        /*
        Person p = new Person(FirstName: "Bill", LastName: "Gates");
        Person p2 = p with { FirstName = "Steve" };
        */

        /*
        if (object.ReferenceEquals(p, p2)) {
            Console.WriteLine("EQUAL");
        } else {
            Console.WriteLine("NOT EQUAL");
        }
        */

        //Console.WriteLine(p.FirstName);


        /*
        Person p = new Person { FirstName = "Bill", LastName = "Gates" };

        Console.WriteLine(p.FirstName); // Bill

        DoStuff(ref p);

        Console.WriteLine(p.FirstName);  // ?
        */


        //Console.WriteLine("Hello, World!");
    }

    static void DoStuff(ref Person p) 
    {
        //p = new Person { FirstName = "Steve", LastName = "Jobs" };
    }
    
    static bool IsEven(int i)
    {
        return i % 2 == 0;
    }
}





