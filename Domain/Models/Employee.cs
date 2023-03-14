using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CQRS_Example.Domain.Models;

public class Employee
{
    [JsonPropertyName("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }

    [Required]
    [JsonPropertyName("lastName")]
    public string LastName { get; set; }

    [Required]
    [JsonPropertyName("department")]
    public string Department { get; set; }

    [Required]
    [JsonPropertyName("jobTitle")]
    public string JobTitle { get; set; }

    [Required]
    [JsonPropertyName("dateOfEmployment")]
    public DateTime DateOfEmployment { get; set; }

    [JsonPropertyName("managerId")]
    public int? ManagerId { get; set; }
}
