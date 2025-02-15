﻿using Microsoft.AspNetCore.Http;

namespace SpotOps.Api.Models.Rest;

public class SpotRequest
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? FileName { get; set; }

    public IFormFile? FormFile { get; set; }

    public string? FileImageSrc { get; set; }
}