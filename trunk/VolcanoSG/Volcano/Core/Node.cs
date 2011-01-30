using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Volcano
{
    abstract class Node
    {
        public SceneGraph Scene { get; protected set; }

        public Model Model { get; protected set; }
        public Vector3 Position { get; set; }

        public Node()
        {
            this.Position = Vector3.Zero;
        }

        public void Init(SceneGraph scene)
        {
            this.Scene = scene;
        }

        public abstract void Load();

        public abstract void Unload();

        public abstract void Update(GameTime time);

        public void Draw(GameTime time)
        {
            Matrix[] transforms = new Matrix[this.Model.Bones.Count];
            this.Model.CopyAbsoluteBoneTransformsTo(transforms);
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
            Scene.Game.GraphicsDevice.Viewport.AspectRatio, 1.0f, 10000.0f);
            Matrix view = Matrix.CreateLookAt(new Vector3(0.0f, 50.0f, 2500.0f), Vector3.Zero, Vector3.Up);

            foreach (ModelMesh mesh in this.Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.View = Scene.Camera.View;
                    effect.Projection = Scene.Camera.Projection;
                    effect.World = Matrix.Identity * transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(this.Position);
                }
                mesh.Draw();
            }
        }
    }
}
