using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

//Zatrzymywanie myszki w oknie
using System.Runtime.InteropServices;

namespace TopDownShooter
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //Zatrzymywanie myszki w oknie
        //=================================================
        [DllImport("user32.dll")]
        static extern void ClipCursor(ref Rectangle rect);
        [DllImport("user32.dll")]
        static extern void ClipCursor(IntPtr rect);
        //=================================================


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        LevelManager levelManager = new LevelManager();
        KeyboardState kbState;
        Camera camera;
        //Texture2D crosshairTexture;
        MouseState msState;
        Vector2 crosshairPossition;

        Vector2 ScreenSize;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        protected override void Initialize()
        {
            //Zatrzymywanie myszki w oknie
            //========================================
            //this.IsMouseVisible = true;
            
            Rectangle rect = Window.ClientBounds;
            rect.Width += rect.X;
            rect.Height += rect.Y;

            ClipCursor(ref rect);
            //========================================
            

            base.Initialize();
            levelManager.Initialize(Content,ScreenSize,graphics);
            camera = new Camera(GraphicsDevice.Viewport);
            ScreenSize = new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height);
            //this.IsMouseVisible = true;
            //<Mouse handler class>
        
        }

        protected override void LoadContent()
        {
            //crosshairTexture = Content.Load<Texture2D>("Crosshair");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            levelManager.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            kbState = Keyboard.GetState();
            msState = Mouse.GetState();

            //Zatrzymywanie myszki w oknie
            //=========================================
            //if (IsActive)
            //{
            /*
            Rectangle rect = Window.ClientBounds;
            rect.Width += rect.X;
            rect.Height += rect.Y;
            ClipCursor(ref rect);
            */
            // }
            //==========================================

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kbState.IsKeyDown(Keys.Escape))
                this.Exit();

            
            levelManager.Update(gameTime, Content,ScreenSize,this);
            camera.Update(gameTime, levelManager.player,ScreenSize.X,ScreenSize.Y);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null,null,null,null,
                camera.transform);
            levelManager.Draw(spriteBatch);
            /*
            spriteBatch.Draw(crosshairTexture, new Vector2(msState.X - crosshairTexture.Width / 2,
                msState.Y - crosshairTexture.Height / 2), Color.White);
                */
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
