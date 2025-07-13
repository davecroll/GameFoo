using GameFoo.Core.Primitives;

namespace GameFoo.Core.Behaviors;

public interface ICollidable
{
    BoundingBox GetRoughBoundingBox();
    List<BoundingBox> GetCollisionBoxes(BoundingBox? areaOfConcern = null);
}
