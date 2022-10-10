using ITIS;

try
{
    /*CubeRootTask.Run();
    FibonacciTask.Run();
    FactorizationTask.Run();
    GCD_LSM_Task.Run();
    SortTask.Run();
    ChessTask.Run();
    SummingSeriesTask.Run();*/

    //PS2_1_Task.Run();
    ArraysTask.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}
finally
{
    Console.WriteLine("Нажмите любую клавишу для выхода");
    Console.ReadKey();
}