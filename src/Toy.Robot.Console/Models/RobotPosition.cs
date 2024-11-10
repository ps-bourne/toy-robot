using System.ComponentModel.DataAnnotations;
using Toy.Robot.Console.Const;

namespace Toy.Robot.Console.Models;

public record RobotPosition
{
    [Required]
    [Range(0,TableConst.MaxX)]
    public int? X { get; set; }

    [Required]
    [Range(0,TableConst.MaxY)]
    public int? Y { get; set; }

    [Required]
    public RobotFace? Face { get; set; }

    public bool IsOnTable => X.HasValue && Y.HasValue && Face.HasValue;
}
