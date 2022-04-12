using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;

namespace Lain_Bootleg_DUX.GameContent
{
    public class GameScene : Game
    {
        public GraphicsDeviceManager graphics { get; private set; }
        public SpriteBatch spriteBatch { get; private set; }

        public Color backgroundColor { get; set; } = Color.Black;

        public bool fixedAspectRatio = false;
        public Vector2 targetAspectRatioSize { get; private set; }
        private float heightToWidthRatio;

        public GameScene()
        {
            graphics = new GraphicsDeviceManager(this);

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += (object sender, EventArgs eventArgs) => ResizeWindowToAspectRation();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        #region PassThrough
        public virtual void OnInitialize() { }
        protected override void Initialize()
        {
            lastWindowSize = Window.GetWindowSize();
            OnInitialize();
            base.Initialize();
        }

        public virtual void OnLoadContent() { }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            OnLoadContent();
        }

        public virtual void OnUpdate(GameTime gameTime) { }
        protected override void Update(GameTime gameTime)
        {
            OnUpdate(gameTime);
            base.Update(gameTime);
        }

        public virtual void OnDraw(GameTime gameTime) { }
        protected override void Draw(GameTime gameTime)
        {
            //ResizeWindowToAspectRation();

            GraphicsDevice.Clear(backgroundColor);

            spriteBatch.Begin();
            OnDraw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        public void SetAspectRatioSize(Vector2 aspectRatioSize)
        {
            targetAspectRatioSize = aspectRatioSize;
            heightToWidthRatio = aspectRatioSize.y / aspectRatioSize.x;
        }

        Vector2 lastWindowSize = Vector2.Zero;
        private void ResizeWindowToAspectRation()
        {
            if (!fixedAspectRatio)
                return;

            Vector2 adjustedSize = Vector2.Zero;
            Vector2 modifedSize = Window.GetWindowSize();

            bool widthChanged = lastWindowSize.x != modifedSize.x;     
            if (widthChanged)
            {
                adjustedSize = new Vector2(modifedSize.x, modifedSize.x * heightToWidthRatio);
                DLog.Log(adjustedSize.x + " || " + adjustedSize.y);
            }
            else
            {
                bool heightChanged = lastWindowSize.y != modifedSize.y;
                if (heightChanged)
                {

                }
            }

            if (adjustedSize != Vector2.Zero)
            {
                lastWindowSize = modifedSize;

                Window.SetWindowSize(adjustedSize);
            }
        }
    }
}