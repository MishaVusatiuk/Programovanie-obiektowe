public interface IHasMagnitude
{
    float Magnitude();
}

class Complex : ICloneable, IEquatable<Complex>, IHasMagnitude
{
    private float realPart;
    public float Real { get => realPart; set => realPart = value; }

    private float imagPart;
    public float Imag { get => imagPart; set => imagPart = value; }

    public Complex(float real, float imag)
    {
        Real = real;
        Imag = imag;
    }

    public static Complex operator +(Complex x, Complex y)
        => new Complex(x.Real + y.Real, x.Imag + y.Imag);

    public static Complex operator -(Complex x, Complex y)
        => new Complex(x.Real - y.Real, x.Imag - y.Imag);

    public static Complex operator -(Complex x)
        => new Complex(-x.Real, -x.Imag);

    public static Complex operator *(Complex x, Complex y)
    {
        float r = (x.realPart * y.realPart) - (x.imagPart * y.imagPart);
        float i = (x.realPart * y.imagPart) + (x.imagPart * y.realPart);
        return new Complex(r, i);
    }

    public override string ToString()
    {
        string sign = imagPart >= 0 ? " + " : " - ";
        float absIm = Math.Abs(imagPart);

        return $"{realPart}{sign}{absIm}i";
    }

    public object Clone()
        => new Complex(realPart, imagPart);

    public bool Equals(Complex other)
    {
        if (other == null) return false;
        return this.realPart == other.realPart && this.imagPart == other.imagPart;
    }

    public float Magnitude()
        => MathF.Sqrt(realPart * realPart + imagPart * imagPart);
}


class Program
{
    static void Main(string[] args)
    {
        Complex a = new Complex(4, 3);
        Complex b = new Complex(-2, 5);

        Complex sum = a + b;
        Complex diff = a - b;
        Complex multiplied = a * b;

        Complex clone = (Complex)sum.Clone();

        float magnitudeA = a.Magnitude();
        bool areEqual = a.Equals(b);

        Console.WriteLine("a = " + a);
        Console.WriteLine("b = " + b);
        Console.WriteLine("a + b = " + sum);
        Console.WriteLine("a - b = " + diff);
        Console.WriteLine("a * b = " + multiplied);
        Console.WriteLine("Clone(sum) = " + clone);
        Console.WriteLine("Magnitude of a = " + magnitudeA);
        Console.WriteLine("a equals b  " + areEqual);
    }
}

