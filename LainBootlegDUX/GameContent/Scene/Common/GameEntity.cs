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
        public string name;
        public GameScene parent;

        public GraphicsDevice graphicDevice => parent.GraphicsDevice;
        public SpriteBatch spriteBth => parent.spriteBatch;

        private Dictionary<string, GameEntity> subEntities = new Dictionary<string, GameEntity>();

        public GameEntity(string entityName, GameScene scene)
        {
            name = entityName;
            parent = scene;
        }

        #region PassThrough
        public virtual void OnInitialize() { }
        public void Initialize()
        {
            OnInitialize();

            foreach (GameEntity entity in subEntities.Values)
                entity.OnInitialize();
        }

        public virtual void OnLoadContent() { }
        public void LoadContent()
        {
            OnLoadContent();

            foreach (GameEntity entity in subEntities.Values)
                entity.OnLoadContent();
        }

        public virtual void OnUpdate(GameTime gameTime) { }
        public void Update(GameTime gameTime)
        {
            OnUpdate(gameTime);

            foreach (GameEntity entity in subEntities.Values)
                entity.OnUpdate(gameTime);
        }

        public virtual void OnDraw(GameTime gameTime) { }
        public void Draw(GameTime gameTime)
        {
            OnDraw(gameTime);

            foreach (GameEntity entity in subEntities.Values)
                entity.OnDraw(gameTime);
        }
        #endregion

        public T AddSubEntity<T>(GameEntity entity)
        {
            if (AddSubEntity(entity))
                return (T)Convert.ChangeType(entity, typeof(T));
            else
                return default(T);
        }

        public bool AddSubEntity(GameEntity entity)
        {
            if (!subEntities.ContainsKey(entity.name))
            {
                subEntities.Add(entity.name, entity);
                return true;
            }
            else
            {
                DLog.Alert($"Entity can't contain Sub-Entitys of the same name : '{entity.name}'");
                return false;
            }
        }

        public T GetSubEntity<T>(string entityName)
        {
            if (subEntities.ContainsKey(entityName))
                return (T)Convert.ChangeType(subEntities[entityName], typeof(T));
            else
                return default(T);
        }
    }
}
