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

        public Color backgroundColor { get; set; } = Color.White;

        private bool ticking = false;

        public GameScene()
        {
            graphics = new GraphicsDeviceManager(this);
           
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnWindowSizeChange;
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        private int lastValue = 0;
        private void TimerTick(object sender, EventArgs e)
        {
            bool paused = !ticking;
            ticking = false;

            //DLog.Log(this.sus + "");
        }

        private void OnWindowSizeChange(object sender, EventArgs e)
        {
            //Window.BeginScreenDeviceChange(false);
        }       

        #region PassThrough
        public virtual void OnInitialize() { }
        protected override void Initialize()
        {
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
            ticking = true;
            //DLog.Log("Updating");
            OnUpdate(gameTime);
            base.Update(gameTime);
        }

        public virtual void OnDraw(GameTime gameTime) { }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            spriteBatch.Begin();
            OnDraw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

    }
}