using System;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("To jest ćwiczenie 1");

        Zwierze[] zwierzeta = new Zwierze[3];

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"\nPodaj informacje o zwierzęciu nr {i + 1}:");

            Console.Write("Nazwa: ");
            string nazwa = Console.ReadLine();

            Console.Write("Gatunek: ");
            string gatunek = Console.ReadLine();

            Console.Write("Liczba nóg: ");
            int nogi = int.Parse(Console.ReadLine());

            zwierzeta[i] = new Zwierze(nazwa, gatunek, nogi);
        }

        Zwierze klon = new Zwierze(zwierzeta[0]);
        Console.Write("Podaj nowe imię dla klona: ");
        klon.Nazwa = Console.ReadLine();

        Console.WriteLine("Lista zwierząt");
        foreach (var z in zwierzeta)
        {
            z.WyswietlInformacje();
            z.daj_glos();
        }

        Console.WriteLine("\n--- Klon ---");
        klon.WyswietlInformacje();
        klon.daj_glos();
        Console.WriteLine($"\nLiczba utworzonych zwierząt: {Zwierze.IleZwierzat()}");
    }
}

public class Zwierze
{
    private string nazwa;
    private string gatunek;
    private int liczbaNog;

    private static int licznikZwierzat = 0;
    public string Nazwa
    {
        get { return nazwa; }
        set { nazwa = value; }
    }

    public string Gatunek
    {
        get { return gatunek; }
    }

    public int LiczbaNog
    {
        get { return liczbaNog; }
    }

    public Zwierze()
    {
        this.nazwa = "Rex";
        this.gatunek = "Pies";
        this.liczbaNog = 4;
        licznikZwierzat++;
    }

    public Zwierze(string nazwa, string gatunek, int liczbaNog)
    {
        this.nazwa = nazwa;
        this.gatunek = gatunek;
        this.liczbaNog = liczbaNog;
        licznikZwierzat++;
    }

    public Zwierze(Zwierze inne)
    {
        this.nazwa = inne.nazwa;
        this.gatunek = inne.gatunek;
        this.liczbaNog = inne.liczbaNog;
        licznikZwierzat++;
    }

    public void daj_glos()
    {
        if (gatunek.ToLower() == "kot")
            Console.WriteLine("Miau miau!");
        else if (gatunek.ToLower() == "krowa")
            Console.WriteLine("MUUUUUU");
        else if (gatunek.ToLower() == "pies")
            Console.WriteLine("HAU HAU!");
       else
            Console.WriteLine($"{gatunek} robi: ???");
    }

    public static int IleZwierzat()
    {
        return licznikZwierzat;
    }
    public void WyswietlInformacje()
    {
        Console.WriteLine($"Zwierzę: {nazwa}, Gatunek: {gatunek}, Nóg: {liczbaNog}");
    }

}