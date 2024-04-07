using System;
using System.Collections.Generic;

namespace project_ecommerce_api.Data;

public partial class CustomerAddress
{
    public Guid Id { get; set; }

    public string CustomerName { get; set; } = null!;

    public string City { get; set; } = null!;

    public string District { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public bool IsDefault { get; set; }

    public Guid CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
