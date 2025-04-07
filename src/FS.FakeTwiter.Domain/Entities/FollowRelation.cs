using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS.FakeTwiter.Domain.Entities;

public class FollowRelation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FollowerId { get; set; } = string.Empty; // Quien sigue
    public string FolloweeId { get; set; } = string.Empty; // A quién sigue
    public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
}
