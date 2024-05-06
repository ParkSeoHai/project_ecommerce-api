using System;
using System.Collections.Generic;

namespace project_ecommerce_api.Models;

public partial class Order
{
    public Guid Id { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Note { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public Guid StatusId { get; set; }

    public Guid CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Status Status { get; set; } = null!;
}
