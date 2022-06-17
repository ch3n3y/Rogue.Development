using System;

namespace Rogue.Development.Channels.Stocks;

public class StockUpdateDto
{
    public Guid StockId { get; set; }
    public decimal Value { get; set; }
}