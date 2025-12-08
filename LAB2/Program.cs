public class Zwierzak
{
    private protected string _imie;
    public string Imie
    {
        get => _imie;
        set => _imie = value;
    }

    public Zwierzak(string imie)
    {
        Imie = imie;
    }

    public virtual void WydajDzwiek()
    {
        Console.WriteLine("Zwierzak wydaje nieokreślony dźwięk...");
    }
}

public class Pies : Zwierzak
{
    public Pies(string imie) : base(imie) { }

    public override void WydajDzwiek()
    {
        Console.WriteLine("Hau Hau!");
    }
}

public class Kot : Zwierzak
{
    public Kot(string imie) : base(imie) { }

    public override void WydajDzwiek()
    {
        Console.WriteLine("Miauu!");
    }
}

public class Wonsz : Zwierzak
{
    public Wonsz(string imie) : base(imie) { }

    public override void WydajDzwiek()
    {
        Console.WriteLine("Sssss...");
    }
}

public abstract class Pracownik
{
    public abstract void WykonajPrace();
}

public class Cukiernik : Pracownik
{
    public override void WykonajPrace()
    {
        Console.WriteLine("Przygotowuję ciasto...");
    }
}

public class KlasaA
{
    public KlasaA()
    {
        Console.WriteLine("Wywołano konstruktor klasy A");
    }
}

public class KlasaB : KlasaA
{
    public KlasaB() : base()
    {
        Console.WriteLine("Wywołano konstruktor klasy B");
    }
}

public class KlasaC : KlasaB
{
    public KlasaC() : base()
    {
        Console.WriteLine("Wywołano konstruktor klasy C");
    }
}

class Program
{
    public static void TestujDzwiek(Zwierzak zw)
    {
        Console.WriteLine("Aktualny typ obiektu: " + zw.GetType().Name);
        zw.WydajDzwiek();
    }

    public static void Main(string[] args)
    {
        Zwierzak ogolny = new Zwierzak("Tajemniczy zwierz");
        Pies burek = new Pies("Azor");
        Kot mruczek = new Kot("Mruczek");
        Wonsz python = new Wonsz("Gadzina");

        Cukiernik cukiernik = new Cukiernik();

        KlasaA konstruktorA = new KlasaA();
        KlasaB konstruktorB = new KlasaB();
        KlasaC konstruktorC = new KlasaC();

        TestujDzwiek(python);
        cukiernik.WykonajPrace();
    }
}
