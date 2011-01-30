using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Volcano
{
    class RockTiki : Actor
    {
        public override void Load()
        {
            this.Model = this.Scene.Game.Content.Load<Model>(@"Models\rocktiki");
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime time)
        {
        }
    }
}
