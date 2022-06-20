namespace Rogue.Development.Ui.Stocks;

public class StockChange
{
    public string? Name { get; set; }
    public decimal Value { get; set; }
    public char ChangeDirection { get; set; }
    public decimal Change { get; set; }
}