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
        public Vector2Int targetAspectRatioSize { get; private set; }
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

            OnInitialize();

            foreach (GameEntity entity in sceneEntities)
                entity.Initialize();

            base.Initialize();
        }

        public virtual void OnLoadContent() { }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            OnLoadContent();

            foreach (GameEntity entity in sceneEntities)
                entity.LoadContent();
        }

        public virtual void OnUpdate(GameTime gameTime) { }
        protected override void Update(GameTime gameTime)
        {
            OnUpdate(gameTime);

            foreach (GameEntity entity in sceneEntities)
                entity.Update(gameTime);

            base.Update(gameTime);
        }

        public virtual void OnDraw(GameTime gameTime) { }
        protected override void Draw(GameTime gameTime)
        {
            ResizeWindowToAspectRation();

            GraphicsDevice.Clear(backgroundColor);

            spriteBatch.Begin();

            OnDraw(gameTime);

            foreach (GameEntity entity in sceneEntities)
                entity.Draw(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        public void SetAspectRatioSize(Vector2Int aspectRatioSize)
        {
            targetAspectRatioSize = aspectRatioSize;

            Vector2 ratioSize = aspectRatioSize.vector;
            heightToWidthRatio = ratioSize.x / ratioSize.y;
            widthToHeightRatio = ratioSize.y / ratioSize.x;
        }

        private void WindowSizeChangedEnd(object sender, EventArgs eventArgs)
        {
            userScalingState = 0;

            ChangeWindowSize(lastWindowSize);
        }

        int userScalingState = 0; // 0 : None | 1 : Width | 2 : Height
        Vector2Int lastWindowSize = Vector2Int.zero;
        private void ResizeWindowToAspectRation()
        {
            if (!fixedAspectRatio)
                return;

            Vector2Int adjustedSize = Vector2Int.zero;
            Vector2Int modifedSize = Window.GetWindowSize();

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
                case 1:
                    adjustedSize = new Vector2Int(modifedSize.x, modifedSize.x * widthToHeightRatio);
                    break;

                case 2:
                    adjustedSize = new Vector2Int(modifedSize.y * heightToWidthRatio, modifedSize.y);
                    break;

                default: return;
            }

            if (adjustedSize != lastWindowSize)
                ChangeWindowSize(adjustedSize);

            lastWindowSize = adjustedSize;
        }

        public void ChangeWindowSize(Vector2Int newSize)
        {
            Window.SetWindowSize(newSize);

            graphics.PreferredBackBufferWidth = newSize.x;
            graphics.PreferredBackBufferHeight = newSize.y;
            graphics.ApplyChanges();
        }
    }
}