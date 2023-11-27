using System.Text.Json;
using System.Windows.Input;

class Program
{
    static void Main(string[] args)
    {
        // упаковка подарка
        //Car car = new Car("форд");
        //Present<Car> present = new Present<Car>(car);


        // распаковка подарка
        //Car catWifeAgain = present.Unpack();


        // создание объекта по его типу
        //Test<Car> test = new Test<Car>();
        //Car created = test.GetInstance("красная машина");
        //Console.WriteLine(created.Title);

        Student student = new Student { FirstName = "Валера", LastName = "Петров" };
        Group group = new Group { Title = "1125" };
        Curator cur = new Curator { FirstName = "Грымза", LastName = "Дьяволна" };

        Console.WriteLine(student.GetJson());
        Console.WriteLine(group.GetJson());
        Console.WriteLine(cur.GetJson());

        /*Program program = new Program();
        string ненадо = program.GetJson();
        Console.WriteLine(ненадо);*/
    }
}

// Обобщения в c#
// Обобщения позволяют не указывать конкретный тип 
// данных, который будет использоваться в классах 
// или методах. При этом будет создана специальная 
// переменная, с помощью которой можно обратиться к
// значению обобщенного типа.
// Обобщения позволяют написать универсальный код
// для однородной работы с разными типами данных
// обобщение можно уточнить
// where T : class - тип данных является ссылочным
// where T : struct - тип данных является значимым
// Если для обобщения указано наследование классов 
// и интерфейсов, то на обощенных переменных будут
// доступны методы/свойства/события указанных классов
// и интерфейсов

class Present<T, V> where T : Car, ICommand where V : Test<T>
{
    private readonly T arg;

    public Present(T arg)
    {
        this.arg = arg;
    }

    public T Unpack()
    { 
        return arg;
    }
}

class Test<T>
{
    public T GetInstance(params object[] args)
    {
        Type type = typeof(T);
        // создание экземпляра по типу данных
        T result = (T)Activator.CreateInstance(type, args);
        return result;
    }

}

class Car : ICommand
{ 
    
    public string Title { get; set; }

    public Car(string title)
    {
        Title = title;
    }
    // Ошибка! Тип V обобщен только для метода, т.е. используется
    // только внутри метода
    // V field;
    // обобщение для метода
    public void SetEngine<V>(V engine) where V : class
    { 
        // работа с типом V
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        throw new NotImplementedException();
    }

    public void Execute(object? parameter)
    {
        throw new NotImplementedException();
    }
}

public interface IJsonAble { }

public class Student : IJsonAble
{ 
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class Curator : IJsonAble
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class Group : IJsonAble
{
    public string Title { get; set; }
}

public static class ObjExtension
{
    // полезное применение обобщения
    public static string GetJson<T>(this T obj) where T : IJsonAble
    { 
        return JsonSerializer.Serialize(obj);
    }
}