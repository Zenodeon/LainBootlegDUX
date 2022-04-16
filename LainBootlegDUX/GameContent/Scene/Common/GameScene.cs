using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SDL2;
using System;
using System.Threading;

namespace LainBootlegDUX.GameContent
{
    public class GameScene : Game
    {
        public GraphicsDeviceManager graphics { get; private set; }
        public SpriteBatch spriteBatch { get; private set; }

        public Color backgroundColor { get; set; } = Color.Black;

        public bool fixedAspectRatio = false;
        public Vector2 targetAspectRatioSize { get; private set; }
        private float widthToHeightRatio;
        private float heightToWidthRatio;

        public List<GameEntity> sceneEntities = new List<GameEntity>();

        public GameScene()
        {
            graphics = new GraphicsDeviceManager(this);

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += WindowSizeChangedEnd;
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        #region PassThrough
        public virtual void OnInitialize() { }
        protected override void Initialize()
        {
            lastWindowSize = Window.GetWindowSize();

            foreach (GameEntity entity in sceneEntities)
                entity.OnInitialize();

            OnInitialize();

            base.Initialize();
        }

        public virtual void OnLoadContent() { }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (GameEntity entity in sceneEntities)
                entity.OnLoadContent();

            OnLoadContent();
        }

        public virtual void OnUpdate(GameTime gameTime) { }
        protected override void Update(GameTime gameTime)
        {
            foreach (GameEntity entity in sceneEntities)
                entity.OnUpdate(gameTime);

            OnUpdate(gameTime);

            base.Update(gameTime);
        }

        public virtual void OnDraw(GameTime gameTime) { }
        protected override void Draw(GameTime gameTime)
        {
            ResizeWindowToAspectRation();

            GraphicsDevice.Clear(backgroundColor);

            spriteBatch.Begin();

            foreach (GameEntity entity in sceneEntities)
                entity.OnDraw(gameTime);

            OnDraw(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        public void SetAspectRatioSize(Vector2 aspectRatioSize)
        {
            targetAspectRatioSize = aspectRatioSize;

            heightToWidthRatio = aspectRatioSize.x / aspectRatioSize.y;
            widthToHeightRatio = aspectRatioSize.y / aspectRatioSize.x;
        }

        private void WindowSizeChangedEnd(object sender, EventArgs eventArgs)
        {
            userScalingState = 0;

            Window.SetWindowSize(lastWindowSize);

            graphics.PreferredBackBufferWidth = (int)lastWindowSize.x;
            graphics.PreferredBackBufferHeight = (int)lastWindowSize.y;
            graphics.ApplyChanges();
        }

        int userScalingState = 0; // 0 : None | 1 : Width | 2 : Height
        Vector2 lastWindowSize = Vector2.Zero;
        private void ResizeWindowToAspectRation()
        {
            if (!fixedAspectRatio)
                return;

            Vector2 adjustedSize = Vector2.Zero;
            Vector2 modifedSize = Window.GetWindowSize();

            if (userScalingState == 0)
            {
                if (lastWindowSize.x != modifedSize.x)
                    userScalingState++;

                if (lastWindowSize.y != modifedSize.y)
                    if (userScalingState == 0)
                        userScalingState += 2;
            }

            switch (userScalingState)
            {
                case 0:          
                    return;

                case 1:
                    adjustedSize = new Vector2(modifedSize.X, (int)(modifedSize.x * widthToHeightRatio));
                    break;

                case 2:
                    adjustedSize = new Vector2((int)(modifedSize.y * heightToWidthRatio), modifedSize.y);
                    break;
            }

            if (adjustedSize != lastWindowSize)
            {
                Window.SetWindowSize(adjustedSize);
                
                graphics.PreferredBackBufferWidth = (int)adjustedSize.x;
                graphics.PreferredBackBufferHeight = (int)adjustedSize.y;
                graphics.ApplyChanges();
            }

            lastWindowSize = adjustedSize;
        }
    }
}