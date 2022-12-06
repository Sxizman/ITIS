using NUnit.Framework;

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

[TestFixture]
public class TrigonometricComplex_Tests
{
    private static double Epsilon = 1e-10;

    [TestCase(1, 0, 1, 0, 1, 0)]
    [TestCase(4, 0, 5, 0, 20, 0)]

    [TestCase(3, Math.PI, 5, 0, 15, Math.PI)]
    [TestCase(4, 0, 1, Math.PI, 4, Math.PI)]
    [TestCase(2, Math.PI, 8, -Math.PI, 16, 0)]
    public void TestMultiplication(double leftMod, double leftArg, double rightMod, double rightArg, double expectedMod, double expectedArg)
    {
        var left = TrigonometricComplex.FromTrigonometric(leftMod, leftArg);
        var right = TrigonometricComplex.FromTrigonometric(rightMod, rightArg);
        var result = left * right;

        Assert.AreEqual(expectedMod, result.Mod, Epsilon);
        Assert.AreEqual(expectedArg, result.Arg, Epsilon);
    }

    [TestCase(0, 0, 0, 0)]
    [TestCase(2, 0, 0, 5)]
    [TestCase(7, 2, 0, 6)]
    [TestCase(0, 3, 4, 7)]
    public void TestMultiplicationByZero(double leftMod, double leftArg, double rightMod, double rightArg)
    {
        var left = TrigonometricComplex.FromTrigonometric(leftMod, leftArg);
        var right = TrigonometricComplex.FromTrigonometric(rightMod, rightArg);
        var result = left * right;

        Assert.AreEqual(0, result.Mod);
    }

    [TestCase(1, 0, 1, 0, 1, 0)]
    [TestCase(9, 0, 3, 0, 3, 0)]

    [TestCase(4, 0, 1, Math.PI, 4, -Math.PI)]
    [TestCase(6, Math.PI, 2, Math.PI, 3, 0)]
    [TestCase(8, 0, 8, -Math.PI, 1, Math.PI)]
    public void TestDivision(double leftMod, double leftArg, double rightMod, double rightArg, double expectedMod, double expectedArg)
    {
        var left = TrigonometricComplex.FromTrigonometric(leftMod, leftArg);
        var right = TrigonometricComplex.FromTrigonometric(rightMod, rightArg);
        var result = left / right;

        Assert.AreEqual(expectedMod, result.Mod, Epsilon);
        Assert.AreEqual(expectedArg, result.Arg, Epsilon);
    }

    [TestCase(1, 0,   1)]
    [TestCase(3, Math.PI,   -3)]
    [TestCase(2, Math.PI / 2,   0)]
    public void TestReProp(double mod, double arg, double expectedRe)
    {
        var value = TrigonometricComplex.FromTrigonometric(mod, arg);
        Assert.AreEqual(expectedRe, value.Re, Epsilon);
    }

    [TestCase(1, 0,   0)]
    [TestCase(4, Math.PI,   0)]
    [TestCase(6, Math.PI / 2,   6)]
    public void TestImProp(double mod, double arg, double expectedIm)
    {
        var value = TrigonometricComplex.FromTrigonometric(mod, arg);
        Assert.AreEqual(expectedIm, value.Im, Epsilon);
    }

    [TestCase(1, 0,   1, 0)]
    [TestCase(-3, 0,   3, Math.PI)]
    [TestCase(0, 8,   8, Math.PI / 2)]
    public void TestCreateFromAlgebraic(double re, double im, double expectedMod, double expectedArg)
    {
        var value = TrigonometricComplex.FromAlgebraic(re, im);
        Assert.AreEqual(expectedMod, value.Mod, Epsilon);
        Assert.AreEqual(expectedArg, value.Arg, Epsilon);
    }
}