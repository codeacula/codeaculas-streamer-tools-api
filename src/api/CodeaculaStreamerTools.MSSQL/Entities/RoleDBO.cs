namespace CodeaculaStreamerTools.MSSQL.Entities;

using System.ComponentModel.DataAnnotations;

public class RoleDBO
{
  [Key]
  public int Id { get; set; }

  [Required]
  public required string Name { get; set; }

  public required string Description { get; set; }
}
