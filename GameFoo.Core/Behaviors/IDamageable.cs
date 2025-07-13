using GameFoo.Core.Actors;
using GameFoo.Core.Primitives;

namespace GameFoo.Core.Behaviors;

public interface IDamageable
{
    List<BoundingBox> GetHurtBoxes();
    void TakeDamage(IActor actor);
}
