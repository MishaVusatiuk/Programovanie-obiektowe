using System;
using System.Collections.Generic;
using System.Linq;

namespace LaboratoriumZespolone;
public interface IModularne
{
    double ObliczModul();
}

public class LiczbaZespolona : ICloneable, IEquatable<LiczbaZespolona>, IModularne, IComparable<LiczbaZespolona>
{
    public double CzescRzeczywista { get; set; }
    public double CzescUrojona { get; set; }

    public LiczbaZespolona(double re, double im)
    {
        CzescRzeczywista = re;
        CzescUrojona = im;
    }

    public int CompareTo(LiczbaZespolona? inna)
    {
        if (inna is null) return 1;
        return ObliczModul().CompareTo(inna.ObliczModul());
    }

    public double ObliczModul() => Math.Sqrt(Math.Pow(CzescRzeczywista, 2) + Math.Pow(CzescUrojona, 2));

    public override string ToString()
    {
        char znak = CzescUrojona >= 0 ? '+' : '-';
        return $"{CzescRzeczywista} {znak} {Math.Abs(CzescUrojona)}i";
    }

    public object Clone() => new LiczbaZespolona(CzescRzeczywista, CzescUrojona);

    public bool Equals(LiczbaZespolona? inna) =>
        inna is not null && CzescRzeczywista == inna.CzescRzeczywista && CzescUrojona == inna.CzescUrojona;

    public override bool Equals(object? obj) => Equals(obj as LiczbaZespolona);

    public override int GetHashCode() => HashCode.Combine(CzescRzeczywista, CzescUrojona);

    public static LiczbaZespolona operator +(LiczbaZespolona a, LiczbaZespolona b) => new(a.CzescRzeczywista + b.CzescRzeczywista, a.CzescUrojona + b.CzescUrojona);
    public static LiczbaZespolona operator -(LiczbaZespolona a, LiczbaZespolona b) => new(a.CzescRzeczywista - b.CzescRzeczywista, a.CzescUrojona - b.CzescUrojona);
    public static LiczbaZespolona operator *(LiczbaZespolona a, LiczbaZespolona b) =>
        new(a.CzescRzeczywista * b.CzescRzeczywista - a.CzescUrojona * b.CzescUrojona, a.CzescRzeczywista * b.CzescUrojona + a.CzescUrojona * b.CzescRzeczywista);

    public static bool operator ==(LiczbaZespolona a, LiczbaZespolona b) => Equals(a, b);
    public static bool operator !=(LiczbaZespolona a, LiczbaZespolona b) => !Equals(a, b);
}

class Program
{
    static void Main()
    {

        var tablica = new LiczbaZespolona[]
        {
            new(3, 4), new(1, -2), new(3, 4), new(-2, 4), new(-1, -5)
        };

        Console.WriteLine("--- TABLICA ---");
        foreach (var l in tablica) Console.WriteLine(l);

        Array.Sort(tablica);
        Console.WriteLine("\nPosortowana wg modułu:");
        foreach (var l in tablica) Console.WriteLine($"{l} (Moduł: {l.ObliczModul():F2})");

        Console.WriteLine($"\nMinimum: {tablica.Min()} | Maximum: {tablica.Max()}");

        Console.WriteLine("\nFiltrowanie (Im >= 0):");
        var przefiltrowanaTablica = tablica.Where(z => z.CzescUrojona >= 0);
        foreach (var l in przefiltrowanaTablica) Console.WriteLine(l);

        Console.WriteLine("\n--- LISTA ---");
        var lista = new List<LiczbaZespolona>(tablica);

        lista.RemoveAt(1);
        lista.Remove(lista.Min()!);

        foreach (var l in lista) Console.WriteLine(l);
        lista.Clear();
        Console.WriteLine($"Po wyczyszczeniu: {lista.Count} elementów.");

        var z1 = new LiczbaZespolona(6, 7);
        var z2 = new LiczbaZespolona(1, 2);
        var z3 = new LiczbaZespolona(6, 7); 
        var z4 = new LiczbaZespolona(1, -2);
        var z5 = new LiczbaZespolona(-5, 9);

        var zbior = new HashSet<LiczbaZespolona> { z1, z2, z3, z4, z5 };
        Console.WriteLine("\n--- HASHSET ---");
        foreach (var z in zbior) Console.WriteLine(z);
        Console.WriteLine($"Min w zbiorze: {zbior.Min()} | Max: {zbior.Max()}");

        var slownik = new Dictionary<string, LiczbaZespolona>
        {
            ["z1"] = z1,
            ["z2"] = z2,
            ["z3"] = z3,
            ["z4"] = z4,
            ["z5"] = z5
        };

        Console.WriteLine("\n--- SŁOWNIK ---");
        foreach (var para in slownik) Console.WriteLine($"{para.Key}: {para.Value}");

        Console.WriteLine("\nKlucze: " + string.Join(", ", slownik.Keys));
        Console.WriteLine("Czy zawiera z6? " + slownik.ContainsKey("z6"));
        Console.WriteLine("Max wartość: " + slownik.Values.Max());
        var filtrSlownik = slownik.Values.Where(v => v.CzescUrojona >= 0);

        slownik.Remove("z3");
        var kluczDoUsuniecia = slownik.Keys.ElementAt(1);
        slownik.Remove(kluczDoUsuniecia);

        Console.WriteLine($"\nStan po zmianach (ilość): {slownik.Count}");
        slownik.Clear();
    }
}
