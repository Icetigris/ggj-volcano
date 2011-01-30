using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Volcano
{
    class SceneGraph : Node
    {
        public CircleCamera Camera { get; protected set; }
        public MainGame Game;
        public List<Node> Nodes { get; protected set; }

        public SceneGraph(MainGame game)
        {
            this.Game = game;
            this.Nodes = new List<Node>();
            this.Camera = new CircleCamera(Game, 2500.0f);
        }

        public void Init()
        {
            foreach (Node node in this.Nodes) node.Init(this);
        }

        public override void Load()
        {
            foreach (Node node in this.Nodes) node.Load();
        }

        public override void Unload()
        {
            foreach (Node node in this.Nodes) node.Unload();
        }

        public override void Update(GameTime time)
        {
            this.Camera.Update(time);
            foreach (Node node in this.Nodes) node.Update(time);
        }

        public new void Draw(GameTime time)
        {
            foreach (Node node in this.Nodes) node.Draw(time);
        }
    }
}
