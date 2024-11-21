using Bricker.GameObjects;
using Bricker.Strategies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

namespace Bricker
{
    public class Brick : IGameObject, ICollidableRectangle
    {
        private BrickerGameManager GameManager;
        private Point2 Position;
        private Texture2D Texture;
        public RectangleF _rectangle;

        public IBrickCollisionStrategy _collisionStrategy;
        private SpriteBatch _spriteBatch;

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public Brick(BrickerGameManager brickerGameManager, Point2 position, Texture2D texture, IBrickCollisionStrategy collistionStrategy)
        {
            this.GameManager = brickerGameManager;
            this.Position = position;
            this.Texture = texture;
            _rectangle = new RectangleF(position, new Point(texture.Width, texture.Height));
            _collisionStrategy = collistionStrategy;
            _spriteBatch = new SpriteBatch(GameManager.GraphicsDevice);

        }

        public RectangleF Bounds => _rectangle;

        public bool IsDestroyed { get; set; } = false;

    public int DrawOrder => 1;

    public bool Visible  { get; set; } = true;                                         
    public bool Enabled  { get; set; } = true;

public int UpdateOrder => 1;

        public void Accept(ICollisionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //if (Visible)
            {
                spriteBatch.Draw(Texture, _rectangle.Position, null, Color.White);
            }
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Initialize()
        {
        }

        public void Draw(GameTime gameTime)
        {
            //_spriteBatch.Draw(Texture, _rectangle.Position, null, Color.White);
            GameManager.SpriteBatch.Draw(Texture, _rectangle.Position, null, Color.White);

        }
    }
}