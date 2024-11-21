using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;

namespace Bricker.GameObjects.Paddles;


public interface IPaddle : IGameObject, IGameComponent
{
    Vector2 Position { get; }
    RectangleF _rectangle { get; }
    Vector2 Velocity { get; }
}

public class Paddle : IPaddle, ICollidableRectangle
{
    public BrickerGameManager GameManager;
    private Vector2 _velocity;
    private Texture2D Texture;

    public Paddle(BrickerGameManager game, Vector2 velocity, Texture2D texture, Vector2 position)
    {
        GameManager = game;
        _velocity = velocity;

        Texture = texture;
        Position = position;
        _rectangle = new RectangleF(position, new Point(texture.Width, texture.Height));
        _spriteBatch = new SpriteBatch(GameManager.GraphicsDevice);

    }

    public bool IsDestroyed { get; set; } = false;


    public Vector2 Position { get; protected set; }
    public RectangleF _rectangle { get; protected set; } = new RectangleF();

    private SpriteBatch _spriteBatch;

    //public IShapeF Bounds => _rectangle;

    public Vector2 Velocity
    {
        get => _velocity;
        protected set => _velocity = value;
    }

    public RectangleF Bounds => _rectangle;

    public int DrawOrder => 1;

    public bool Visible { get; set; } = true;
    public bool Enabled { get; set; } = true;

    public int UpdateOrder => 1;

    public event EventHandler<EventArgs> DrawOrderChanged;
    public event EventHandler<EventArgs> VisibleChanged;
    public event EventHandler<EventArgs> EnabledChanged;
    public event EventHandler<EventArgs> UpdateOrderChanged;

    public void Accept(ICollisionVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, _rectangle.Position, null, Color.White);
    }

    public void Draw(GameTime gameTime)
    {
        //Draw(_spriteBatch);
        Draw(GameManager.SpriteBatch);
    }

    public void Initialize()
    {
    }

    public void OnCollision(CollisionEventArgs collisionInfo)
    {

    }

    public virtual void Update(GameTime gameTime)
    {


    }
}