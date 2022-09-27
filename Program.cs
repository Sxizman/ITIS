using ITIS;

try
{
    SortTask.Run();
    ChessTask.Run();
    SummingSeriesTask.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
    Console.ReadKey();
}