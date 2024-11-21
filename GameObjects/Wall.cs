using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;

namespace Bricker.GameObjects;

public class Wall : IGameObject, IGameComponent, ICollidableRectangle
{
    private BrickerGameManager GameManager;
    public RectangleF _rectangle;
    private SpriteBatch _spriteBatch;

    public event EventHandler<EventArgs> DrawOrderChanged;
    public event EventHandler<EventArgs> VisibleChanged;
    public event EventHandler<EventArgs> EnabledChanged;
    public event EventHandler<EventArgs> UpdateOrderChanged;

    public RectangleF Bounds => _rectangle;

    public Wall(BrickerGameManager game, RectangleF wallBounds)
    {
        GameManager = game;
        _rectangle = wallBounds;
        _spriteBatch = new SpriteBatch(GameManager.GraphicsDevice);

    }

    public void Initialize()
    {
    }

    public void OnCollision(CollisionEventArgs collisionInfo)
    {

    }

    public void Update(GameTime gameTime)
    {
        
    }

    public bool IsDestroyed { get; set; } = false;

    public int DrawOrder => 1;

    public bool Visible { get; set; } = true;
    public bool Enabled { get; set; } = true;

    public int UpdateOrder => 1;

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawRectangle(_rectangle, Color.Aqua);
    }

    public void Accept(ICollisionVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Draw(GameTime gameTime)
    {
        Draw(GameManager.SpriteBatch);
    }
}