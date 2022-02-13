using System;

namespace SpotOps.Api.Models;

public class Spot
{
    /// <summary>
    ///     Spot Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Name of spot
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Date Created
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    ///     Spot Type
    /// </summary>
    public string? Type { get; set; }
}