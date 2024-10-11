using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SpacecraftTracker.WebAPI.CustomActionFilters;
using System.ComponentModel.DataAnnotations;

namespace SpacecraftTracker.WebAPI.DTO
{
    public class AuthDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
