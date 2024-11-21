using Bricker.Extensions;
using Bricker.GameObjects.Paddles;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Bricker.GameObjects
{
    public class BallCollisionVisitor : ICollisionVisitor
    {
        private Ball _ball;
        private BrickerGameManager _game;

        public BallCollisionVisitor(Ball ball, BrickerGameManager game)
        {
            _ball = ball;
            _game = game;
        }

        public void Visit(Paddle paddle)
        {
            if (!_ball._circle.Intersects((BoundingRectangle)paddle._rectangle))
                return;

            HandleRectangleCollision(paddle._rectangle);
        }

        private void HandleRectangleCollision(RectangleF rectangle)
        {
            _ball._velocity = _ball._velocity.Y_AxisFlipped();
            _ball._circle = new CircleF(new Point2(_ball._circle.Position.X, _ball._circle.Position.Y + BallAndRectangleVerticalGapCorrection(rectangle)), _ball._circle.Radius);
        }


        private float BallAndRectangleVerticalGapCorrection(RectangleF rectangle)
        {
            var screenCenter = _game.GraphicsDevice.PresentationParameters.BackBufferHeight / 2;
            bool isBallAboveCenter;
            if (_ball._circle.Center.Y < screenCenter) //ball is above center
            {
                isBallAboveCenter = true;
            }
            else //ball is under center
            {
                isBallAboveCenter = false;
            }

            var rectangleBound = isBallAboveCenter ? rectangle.Bottom :
                rectangle.Top;
            var circleBound = isBallAboveCenter ? _ball._circle.Center.Y - _ball._circle.Radius :
                _ball._circle.Center.Y + _ball._circle.Radius;

            var gap = rectangleBound - circleBound;

            return gap;
        }

        private float BallAndRectangleHorizonalGapCorrection(RectangleF _rectangle)
        {
            var screenCenter = _game.GraphicsDevice.PresentationParameters.BackBufferWidth / 2;
            bool isBallLeftToCenter;
            if (_ball._circle.Center.X < screenCenter) //ball is right to the center
            {
                isBallLeftToCenter = true;
            }
            else
            {
                isBallLeftToCenter = false;
            }

            var rectangleBound = isBallLeftToCenter ? _rectangle.Right :
                _rectangle.Left;
            var circleBound = isBallLeftToCenter ? _ball._circle.Center.X - _ball._circle.Radius :
                _ball._circle.Center.X + _ball._circle.Radius;

            var gap = rectangleBound - circleBound;

            return gap;
        }


        public void Visit(Wall wall)
        {
            HandleWallCollision(wall);
        }

        private void HandleWallCollision(Wall wall)
        {
            if (IsHorizonWall(wall))
            {
                _ball._velocity = _ball._velocity.Y_AxisFlipped();
                _ball._circle = new CircleF(new Point2(_ball._circle.Position.X, _ball._circle.Position.Y + BallAndRectangleVerticalGapCorrection(wall._rectangle)), _ball._circle.Radius);

            }
            else//side wall
            {
                _ball._velocity = _ball._velocity.X_AxisFlipped();
                _ball._circle = new CircleF(new Point2(_ball._circle.Position.X + BallAndRectangleHorizonalGapCorrection(wall._rectangle), _ball._circle.Position.Y), _ball._circle.Radius);
            }
        }

        private bool IsHorizonWall(Wall wall)
        {
            return wall.Bounds.Position.X == 0 && wall._rectangle.Right == _game.GraphicsDevice.PresentationParameters.BackBufferWidth;
        }

        public void Visit(Ball ball)
        {
            throw new NotImplementedException();
        }

        //void ICollisionVisitor.Visit(Paddle paddle)
        //{
        //    throw new NotImplementedException();
        //}

        //void ICollisionVisitor.Visit(Wall wall)
        //{
        //    throw new NotImplementedException();
        //}

        //void ICollisionVisitor.Visit(Ball ball)
        //{
        //    throw new NotImplementedException();
        //}

        void ICollisionVisitor.Visit(Brick brick)
        {
            HandleRectangleCollision(brick._rectangle);
            brick._collisionStrategy.OnCollision(brick, _ball);
        }
    }
}
