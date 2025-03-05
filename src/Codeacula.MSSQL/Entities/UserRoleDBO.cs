namespace Codeacula.MSSQL.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UserRoleDBO
{
  [Key]
  public int Id { get; set; }

  public Guid UserId { get; set; }

  public int RoleId { get; set; }

  [ForeignKey("RoleId")]
  public virtual required RoleDBO Role { get; set; }
}
