using Bricker.Extensions;
using Bricker.GameObjects.Paddles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bricker.GameObjects;

public class Ball : IGameObject, IGameComponent, ICollidable
{
    private BrickerGameManager GameManager { get; }
    public Vector2 _velocity;
    public CircleF _circle = new CircleF();
    private float _incrementSpeed;

    public event EventHandler<EventArgs> DrawOrderChanged;
    public event EventHandler<EventArgs> VisibleChanged;
    public event EventHandler<EventArgs> EnabledChanged;
    public event EventHandler<EventArgs> UpdateOrderChanged;

    public float IncrementSpeed
    {
        get
        {
            _incrementSpeed += 0.0002f;
            return _incrementSpeed;
        }
        set
        {
            _incrementSpeed = value;
        }
    }
        public IShapeF Bounds => _circle;
    private Texture2D Texture { get; }
    private SoundEffect _collisionSound { get; }
    public bool IsDestroyed { get; set; } = false;

    public int DrawOrder => 1;

    public bool Visible  { get; set; } = true;                                         
    public bool Enabled  { get; set; } = true;

public int UpdateOrder => 1;

    public Ball(BrickerGameManager game, Vector2 velocity, Texture2D texture, Vector2 position, Microsoft.Xna.Framework.Audio.SoundEffect collisionSound)
    {
        GameManager = game;
        _velocity = velocity;

        Texture = texture;
        _collisionSound = collisionSound;
        _circle = new CircleF(position, texture.Height / 2);
        

    }


    public void Initialize()
    {

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture: Texture,
                          position: _circle.Position,
                          sourceRectangle: null,
                          color: Color.White,
                          rotation: 0f,
                          origin: new Vector2(Texture.Width / 2, Texture.Height / 2),
                          scale: Vector2.One,
                          effects: SpriteEffects.None,
                          layerDepth: 0f);

        if (_circle.Center.Y - _circle.Radius >= GameManager.GraphicsDevice.PresentationParameters.BackBufferHeight)
        {
            GameManager.CurrentState = GameState.GameOver;

        }
    }
   

    public void Update(GameTime gameTime)
    {

        var ballCollisionVisitor = new BallCollisionVisitor(this, GameManager);

        foreach(ICollidableRectangle obj in GameManager._gameObjects.Where(obj=> obj is ICollidableRectangle))
        {
            if(_circle.Intersects((BoundingRectangle)obj.Bounds))
            {
                _collisionSound.Play();
                obj.Accept(ballCollisionVisitor);
            }
        }

        _circle.Position += _velocity * (gameTime.GetElapsedSeconds() * 100 + IncrementSpeed);

        
    }

    public void Draw(GameTime gameTime)
    {
        Draw(GameManager.SpriteBatch);
    }
}
