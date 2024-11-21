using System;
using Bricker.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System.Collections.Generic;
using System.Security.Principal;
using Bricker.GameObjects.Paddles;
using MonoGame.Extended.Timers;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Reflection.Metadata;
using Bricker.Strategies;
using System.Linq;

namespace Bricker
{
    public class BrickerGameManager : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch;
        //custom fields
        private Ball _ball;
        private readonly CollisionComponent _collisionComponent;
        public readonly List<IGameObject> _gameObjects = new List<IGameObject>();
        private SpriteFont _spriteFont;
        private bool _exitMessageDisplayed = false;
        public GameState CurrentState { get; set; }

        public BrickerGameManager()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _collisionComponent = new CollisionComponent(new RectangleF(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight));

        }

        protected override void Initialize()
        {
            //sounds
            var blop = Content.Load<SoundEffect>("blop_cut_silenced");

            //Create Ball
            var ballTexture = Content.Load<Texture2D>("ball");
            var ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            _ball = new Ball(this, new Vector2(1, 3), ballTexture, ballPosition, blop);
            CurrentState = GameState.Start;
            _gameObjects.Add(_ball);
            Components.Add(_ball);
            
            //Create Paddles
            var paddleTexture = Content.Load<Texture2D>("paddle");
            var paddleHeights = new int[] { _graphics.PreferredBackBufferHeight - 30 };
            var paddleVelocities = new Vector2[] { new Vector2(300f)};
            var paddles = new Type[] { typeof(UserControlledPaddle)};
            for (var i = 0; i < paddleHeights.Length; i++)
            {
                var paddleCenterPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, paddleHeights[i]);
                var paddlePosition = new Vector2(paddleCenterPosition.X - paddleTexture.Width / 2, paddleCenterPosition.Y - paddleTexture.Height / 2);
                var paddle = (IPaddle)Activator.CreateInstance(paddles[i], new object[]{ this, paddleVelocities[i], paddleTexture, paddlePosition });
                _gameObjects.Add(paddle);
                Components.Add(paddle);
            }

            //CreateBricks
            var brickTexture = Content.Load<Texture2D>("brick");

            var numOfBricksInRow = (_graphics.PreferredBackBufferWidth - 30) / brickTexture.Width;
            var numOfRows = (_graphics.PreferredBackBufferHeight / 2 - 20) / brickTexture.Height;
            var firstBrickPlace = new Point2(20, 20);
            for (var j = 0; j < numOfRows; ++j)
            {
                for (var i = 0; i < numOfBricksInRow; ++i)
                {
                    var brickPlace = new Point2(firstBrickPlace.X + (i * brickTexture.Width), firstBrickPlace.Y + (j * brickTexture.Height));
                    var brick = new Brick(this, brickPlace, brickTexture, new SimpleBrickCollisionStrategy(this));
                    _gameObjects.Add(brick);
                    Components.Add(brick);

                }
            }


            //Walls
            const float WallWidth = 4f;
            var wallBounds = new RectangleF(0, 0, WallWidth, _graphics.PreferredBackBufferHeight);
            var leftWall = new Wall(this, wallBounds);
            _gameObjects.Add(leftWall);
            Components.Add(leftWall);


            wallBounds = new RectangleF(_graphics.PreferredBackBufferWidth - WallWidth, 0, WallWidth, _graphics.PreferredBackBufferHeight);
            var rightWall = new Wall(this, wallBounds);
            _gameObjects.Add(rightWall);
            Components.Add(rightWall);


            wallBounds = new RectangleF(0, 0, _graphics.PreferredBackBufferWidth, WallWidth);
            var upperWall = new Wall(this, wallBounds);
            _gameObjects.Add(upperWall);
            Components.Add(upperWall);


            //foreach (var entity in _gameObjects)
            //{
            //    _collisionComponent.Insert(entity);
            //}


            //////
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteFont = Content.Load<SpriteFont>("MyFont");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach (var entity in _gameObjects)
            {
                entity.Update(gameTime);
            }
            _collisionComponent.Update(gameTime);

            _gameObjects.RemoveAll(obj => obj.IsDestroyed);


            ////
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Orange);

            // TODO: Add your drawing code here
            SpriteBatch.Begin();
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Draw(SpriteBatch);
            }

            if (!_gameObjects.Any(obj => obj is Brick))
            {
                SpriteBatch.DrawString(_spriteFont, "You win!", new Vector2(100, 100), Color.White);
            }

            if (CurrentState == GameState.GameOver)
            {
                SpriteBatch.DrawString(_spriteFont, "Goodbye!", new Vector2(100, 100), Color.White);
                Exit();
            }



            /////
            //base.Draw(gameTime);
            SpriteBatch.End();
        }
    }
}