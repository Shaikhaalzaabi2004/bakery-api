using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BakeryAPI.Models;

public partial class Ingredient
{
    public int Id { get; set; }

    public string? Name { get; set; }
    [JsonIgnore]
    public virtual ICollection<ProductsIngredient> ProductsIngredients { get; set; } = new List<ProductsIngredient>();
}
