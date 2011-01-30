using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Volcano
{
    class Volcano : Actor
    {
        public override void Load()
        {
            this.Model = this.Scene.Game.Content.Load<Model>(@"Models\volcano");
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime time)
        {
        }
    }
}
