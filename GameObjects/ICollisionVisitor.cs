using Bricker.GameObjects.Paddles;

namespace Bricker.GameObjects;

public interface ICollisionVisitor
{
    void Visit(Paddle paddle);
    void Visit(Wall wall);
    void Visit(Ball ball);
    void Visit(Brick brick);
}