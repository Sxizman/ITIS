namespace ITIS;

public static class ChessTask
{
    private record struct Position
    {
        public char X { get; }
        public char Y { get; }

        public Position(string value)
        {
            if (value.Length != 2)
                throw new ArgumentException("Invalid position string length");

            X = char.ToLower(value[0]);
            Y = value[1];

            if (X < 'a' || X > 'h' || Y < '1' || Y > '8')
                throw new ArgumentException("Invalid position string value");
        }
    }

    private enum Figure
    {
        Pawn,
        King,
        Queen,
        Bishop,
        Knight,
        Rook
    }

    private static Dictionary<string, Figure> _figuresDict = new Dictionary<string, Figure>
    {
        ["пешка"] = Figure.Pawn,
        ["король"] = Figure.King,
        ["ферзь"] = Figure.Queen,
        ["слон"] = Figure.Bishop,
        ["конь"] = Figure.Knight,
        ["ладья"] = Figure.Rook
    };

    private static bool CheckMoveCorrectness(Figure figure, Position from, Position to)
    {
        if (from == to)
            return false;

        var dx = Math.Abs(from.X - to.X);
        var dy = Math.Abs(from.Y - to.Y);

        switch (figure)
        {
            case Figure.Pawn:
                return (dx == 0) && (to.Y > from.Y) && (dy <= (from.Y == '2' ? 2 : 1));

            case Figure.King:
                return (dx <= 1) && (dy <= 1);

            case Figure.Queen:
                return (dx == dy) || (dx == 0) || (dy == 0);

            case Figure.Bishop:
                return (dx == dy);

            case Figure.Knight:
                return (dx == 1) && (dy == 2) || (dx == 2) && (dy == 1);

            case Figure.Rook:
                return (dx == 0) || (dy == 0);

            default:
                return false;
        }
    }

    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine("========== Проверка корректности хода шахматной фигуры ==========");

        Console.Write("Название фигуры: ");
        var figureName = (Console.ReadLine() ?? "").ToLower();
        var figure = _figuresDict.ContainsKey(figureName) ?
            _figuresDict[figureName] :
            throw new Exception("Unknown figure");

        Console.Write("Позиция перед ходом: ");
        var from = new Position(Console.ReadLine() ?? "");
        Console.Write("Позиция после хода: ");
        var to = new Position(Console.ReadLine() ?? "");

        Console.WriteLine(CheckMoveCorrectness(figure, from, to) ?
            "Ход корректен" :
            "Ход некорректен");

        Console.WriteLine();
    }
}