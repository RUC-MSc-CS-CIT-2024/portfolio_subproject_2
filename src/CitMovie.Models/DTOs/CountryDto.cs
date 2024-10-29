using System;

namespace CitMovie.Models.DTOs;

public class CountryDto
{
    public int CountryId { get; set; }
    public string? ImdbCountryCode { get; set; }
    public string IsoCode { get; set; }
    public string Name { get; set; }

}
