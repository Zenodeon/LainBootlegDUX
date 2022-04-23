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

        public List<GameEntity> subEntities = new List<GameEntity>();

        public GameEntity(GameScene scene)
        {
            parent = scene;
        }

        #region PassThrough
        public virtual void OnInitialize() { }
        public void Initialize()
        {
            OnInitialize();

            foreach (GameEntity entity in subEntities)
            {
                entity.parent = parent;
                entity.OnInitialize();
            }
        }

        public virtual void OnLoadContent() { }
        public void LoadContent()
        {
            OnLoadContent();

            foreach (GameEntity entity in subEntities)
                entity.OnLoadContent();
        }

        public virtual void OnUpdate(GameTime gameTime) { }
        public void Update(GameTime gameTime)
        {
            OnUpdate(gameTime);

            foreach (GameEntity entity in subEntities)
                entity.OnUpdate(gameTime);
        }

        public virtual void OnDraw(GameTime gameTime) { }
        public void Draw(GameTime gameTime)
        {
            OnDraw(gameTime);

            foreach (GameEntity entity in subEntities)
                entity.OnDraw(gameTime);
        }
        #endregion
    }
}
