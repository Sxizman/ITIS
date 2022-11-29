namespace ITIS;

public class AlgebraicComplex
{
    private double re;
    private double im;

    public double Re
    {
        get { return re; }
        init { re = value; }
    }
    public double Im
    {
        get { return im; }
        init { im = value; }
    }

    public double Mod
    {
        get { return Math.Sqrt(re * re + im * im); }
    }
    public double Arg
    {
        get { return Math.Atan2(im, re); }
    }

    private AlgebraicComplex(double re = 0, double im = 0)
    {
        Re = re;
        Im = im;
    }

    public static AlgebraicComplex FromAlgebraic(double re, double im)
    {
        return new AlgebraicComplex(re, im);
    }

    public static AlgebraicComplex FromTrigonometric(double mod, double arg)
    {
        if (mod < 0)
            throw new ArgumentException("Invalid mod value");

        return new AlgebraicComplex(mod * Math.Cos(arg), mod * Math.Sin(arg));
    }

    public override string ToString()
    {
        if (im > 0)
            return $"{re} + {im}i";
        if (im < 0)
            return $"{re} + {-im}";
        return re.ToString();
    }

    public string ToAlgebraicString()
    {
        return ToString();
    }

    public string ToTrigonometricString()
    {
        var mod = Mod;
        var arg = Arg;

        if (mod > 0)
            return $"{mod} * (cos({arg}) + i * sin({arg}))";
        return "0";
    }

    public static AlgebraicComplex operator + (AlgebraicComplex left, AlgebraicComplex right)
    {
        return new AlgebraicComplex(
            left.re + right.re,
            left.im + right.im
            );
    }

    public static AlgebraicComplex operator - (AlgebraicComplex left, AlgebraicComplex right)
    {
        return new AlgebraicComplex(
            left.re - right.re,
            left.im - right.im
            );
    }

    public static AlgebraicComplex operator * (AlgebraicComplex left, AlgebraicComplex right)
    {
        return new AlgebraicComplex(
            left.re * right.re - left.im * right.im,
            left.re * right.im + left.im * right.re
            );
    }

    public static AlgebraicComplex operator / (AlgebraicComplex left, AlgebraicComplex right)
    {
        var mul = 1 / (right.re * right.re + right.im * right.im);
        if (double.IsInfinity(mul))
            throw new ArgumentException("Division by 0 is not allowed");

        return new AlgebraicComplex(
            left.re * mul,
            -left.im * mul
            );
    }
}