using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace TopDownShooter
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {   
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        LevelManager levelManager = new LevelManager();
        KeyboardState kbState;
        Camera camera;

        Vector2 ScreenSize;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            levelManager.Initialize(Content);
            camera = new Camera(GraphicsDevice.Viewport);
            ScreenSize = new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height);
            //this.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            levelManager.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            kbState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kbState.IsKeyDown(Keys.Escape))
                this.Exit();

            
            levelManager.Update(gameTime, Content,ScreenSize);
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
           
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
