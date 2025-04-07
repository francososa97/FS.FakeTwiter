using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS.FakeTwiter.Domain.Entities;

public class Tweet
{
    public Guid Id { get; set; } = Guid.NewGuid(); // ID único del tweet
    public string UserId { get; set; } = string.Empty; // ID del usuario que lo publicó
    public string Content { get; set; } = string.Empty; // Texto del tweet
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Fecha de publicación
}
