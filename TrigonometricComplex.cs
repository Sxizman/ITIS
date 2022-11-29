namespace ITIS;

public class TrigonometricComplex
{
    private double mod;
    private double arg;

    public double Re
    {
        get { return mod * Math.Cos(arg); }
    }
    public double Im
    {
        get { return mod * Math.Sin(arg); }
    }

    public double Mod
    {
        get { return mod; }
        init
        {
            if (value < 0)
                throw new ArgumentException("Invalid mod value");
            mod = value;
        }
    }
    public double Arg
    {
        get { return arg; }
        init { arg = value; }
    }

    private TrigonometricComplex(double mod = 0, double arg = 0)
    {
        Mod = mod;
        Arg = arg;
    }

    public static TrigonometricComplex FromAlgebraic(double re, double im)
    {
        return new TrigonometricComplex(Math.Sqrt(re * re + im * im), Math.Atan2(im, re));
    }

    public static TrigonometricComplex FromTrigonometric(double mod, double arg)
    {
        return new TrigonometricComplex(mod, arg);
    }

    public override string ToString()
    {
        if (mod > 0)
            return $"{mod} * (cos({arg}) + i * sin({arg}))";
        return "0";
    }

    public string ToAlgebraicString()
    {
        var re = Re;
        var im = Im;

        if (im > 0)
            return $"{re} + {im}i";
        if (im < 0)
            return $"{re} + {-im}";
        return re.ToString();
    }

    public string ToTrigonometricString()
    {
        return ToString();
    }

    public static TrigonometricComplex operator * (TrigonometricComplex left, TrigonometricComplex right)
    {
        return new TrigonometricComplex(
            left.mod * right.mod,
            left.arg + right.arg
            );
    }

    public static TrigonometricComplex operator / (TrigonometricComplex left, TrigonometricComplex right)
    {
        if (right.mod == 0)
            throw new ArgumentException("Division by 0 is not allowed");

        return new TrigonometricComplex(
            left.mod / right.mod,
            left.arg - right.arg
            );
    }
}