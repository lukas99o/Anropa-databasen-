using System;
using System.Collections.Generic;

namespace Anropa_databasen_.Models;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
