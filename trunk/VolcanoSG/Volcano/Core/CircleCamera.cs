using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Volcano
{
    public class CircleCamera
    {
        public static float MAX_HEIGHT = 5000.0f;
        public static float MIN_HEIGHT = 400.0f;

        public static int LEFT =   1;
        public static int RIGHT = -1;

        private Matrix projection;
        private Matrix view;
        private Vector3 position;
        private Vector3 target;
        private Vector3 up;

        public Matrix Projection { get { return projection; } protected set { projection = value;  } }
        public Matrix View { get { return view; } protected set { view = value;  } }

        public Vector3 Position { get { return position; } protected set { position = value;  } }
        public Vector3 Target { get { return target; } protected set { target = value;  } }
        public Vector3 Up { get { return up; } protected set { up = value; } }

        public float Radius { get; protected set; }
        public float Theta  { get; protected set; }

        public CircleCamera(Game game, float rad)
        {
            Theta  = 0.0f;
            Radius = rad;
            Init(game);
        }

        public void Init(Game game)
        {
            // Set the initial position
            UpdatePosition();
            Target = Vector3.Zero;
            Up = Vector3.Up;
            
            // Create the projection matrix
            Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 
                800 / 600, 1.0f, 10000.0f, out projection);

            // Create the view matrix
            Matrix.CreateLookAt(ref position, ref target, ref up, out view);
        }

        public void Update(GameTime time)
        {
            KeyboardState state = Keyboard.GetState();
            float delta = (float) time.ElapsedGameTime.TotalSeconds;

            if(state.IsKeyDown(Keys.Left)) {
                this.Theta += delta * CircleCamera.LEFT;
            }
            if(state.IsKeyDown(Keys.Right)) {
                this.Theta += delta * CircleCamera.RIGHT;
            }
            if(state.IsKeyDown(Keys.Up)) {
                if(position.Y < CircleCamera.MAX_HEIGHT) position.Y += 200;
            }
            if (state.IsKeyDown(Keys.Down)) {
                if(position.Y > CircleCamera.MIN_HEIGHT) position.Y -= 200;
            }

            UpdatePosition();

            Matrix.CreateLookAt(ref position, ref target, ref up, out view);
        }

        private void UpdatePosition()
        {
            position.X = Radius * (float)Math.Cos(Theta * 1.5);
            position.Z = Radius * (float)Math.Sin(Theta * 1.5);
        }
    }
}
