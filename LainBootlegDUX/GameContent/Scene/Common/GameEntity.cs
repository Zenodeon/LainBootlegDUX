using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LainBootlegDUX.GameContent
{
    public class GameEntity
    {
        public GameScene parent;

        public GraphicsDevice graphicDevice => parent.GraphicsDevice;
        public SpriteBatch spriteBth => parent.spriteBatch;

        public GameEntity(GameScene scene)
        {
            parent = scene;
        }

        public virtual void OnInitialize()
        {

        }

        public virtual void OnLoadContent()
        {

        }

        public virtual void OnUpdate(GameTime gameTime)
        {

        }

        public virtual void OnDraw(GameTime gameTime)
        {

        }
    }
}
