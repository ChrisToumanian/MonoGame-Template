using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTemplate
{
    class Game : Microsoft.Xna.Framework.Game
    {
        public static Scene scene;
        private SpriteBatch batch;
        private SpriteSortMode mode = SpriteSortMode.FrontToBack;
        private GraphicsDeviceManager graphics;
        RenderTarget2D buffer;
        public static int width = 1920;
        public static int height = 1080;
        public static int gameWidth = 480;
        public static int gameHeight = 270;
        private static bool fullscreen = false;
        private static bool useDisplayResolution = false;
        private static bool mouseCursor = true;
        private static bool multisampling = true;

        // 1. Constructor
        public Game() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = fullscreen;
            DisplayMode displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            if (useDisplayResolution)
            {
                width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.PreferMultiSampling = multisampling;
            graphics.HardwareModeSwitch = true;
            IsMouseVisible = mouseCursor;
        }

        // 2. Load
        protected override void LoadContent()
        {
            // Load Images
            Assets.Load();

            // Load First Scene
            Assets.introScene.Load();
            scene = Assets.introScene;

            // Create Sprite Batch
            batch = new SpriteBatch(GraphicsDevice);
            buffer = new RenderTarget2D(GraphicsDevice, gameWidth, gameHeight);
        }

        // 3. Update
        protected override void Update(GameTime gameTime)
        {
            scene.UpdateGameObjects(gameTime);
            scene.Update(gameTime);
            Controller.Update();
        }

        // 4. Draw
        protected override void Draw(GameTime gameTime)
        {
            if (buffer.Width != scene.camera.bounds.Width)
            {
                buffer = new RenderTarget2D(GraphicsDevice, scene.camera.bounds.Width, scene.camera.bounds.Height);
            }

            // Buffer
            GraphicsDevice.SetRenderTarget(buffer);
            GraphicsDevice.Clear(scene.backgroundColor);
            batch.Begin(mode, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

            // Draw Background
            if (scene.backgroundImage != null)
            {
                batch.Draw(scene.backgroundImage.sprite.texture,
                        new Vector2(scene.backgroundImage.transform.position.X - scene.camera.bounds.X, scene.backgroundImage.transform.position.Y - scene.camera.bounds.Y),
                        scene.backgroundImage.sprite.bounds,
                        scene.backgroundImage.sprite.color,
                        scene.backgroundImage.transform.rotation,
                        scene.backgroundImage.transform.origin,
                        scene.backgroundImage.transform.scale,
                        SpriteEffects.None,
                        scene.backgroundImage.sprite.depth);
            }

            // Draw Tiles
            if (scene.map != null)
            {
                Vector2 origin = new Vector2(0, 0);
                Rectangle view = scene.camera.bounds;
                int tileWidth = scene.map.tileWidth;
                
                int tileX = scene.camera.bounds.X / tileWidth;
                if (tileX < 0) tileX = 0;

                int tileY = scene.camera.bounds.Y / tileWidth;
                if (tileY < 0) tileY = 0;

                int columns = ((scene.camera.bounds.Width + scene.camera.bounds.X) / tileWidth) + 1;
                if (columns > scene.bounds.Width / tileWidth) columns = scene.bounds.Width / tileWidth;

                int rows = ((scene.camera.bounds.Height + scene.camera.bounds.Y) / tileWidth) + 1;
                if (rows > scene.bounds.Height / tileWidth) rows = scene.bounds.Height / tileWidth;

                for (int y = tileY; y < rows; y++)
                {
                    for (int x = tileX; x < columns; x++)
                    {
                        if (scene.map.tiles[x, y] != null)
                        {
                            batch.Draw(scene.map.tiles[x, y].sprite.texture,
                                new Vector2((x * tileWidth) - scene.camera.bounds.X, (y * tileWidth) - scene.camera.bounds.Y),
                                scene.map.tiles[x, y].sprite.bounds, scene.map.tiles[x, y].sprite.color,
                                0,
                                origin,
                                1,
                                scene.map.tiles[x, y].sprite.spriteEffect,
                                scene.map.tiles[x, y].sprite.depth);
                        }
                    }
                }
            }

            // Draw GameObjects
            for (int i = 0; i < scene.gameObjects.Count; i++)
            {
                if (scene.gameObjects[i].sprite != null && scene.gameObjects[i].enabled)
                {
                    float depth = scene.gameObjects[i].sprite.depth;
                    if (scene.gameObjects[i].sprite.useLevelDepth)
                    {
                        depth = scene.gameObjects[i].transform.position.Y * 0.0002f;
                    }

                    batch.Draw(scene.gameObjects[i].sprite.texture,
                        new Vector2((int)scene.gameObjects[i].transform.position.X - scene.camera.bounds.X, (int)scene.gameObjects[i].transform.position.Y - scene.camera.bounds.Y), 
                        scene.gameObjects[i].sprite.bounds, 
                        scene.gameObjects[i].sprite.color, 
                        scene.gameObjects[i].transform.rotation,
                        scene.gameObjects[i].transform.origin, 
                        scene.gameObjects[i].transform.scale,
                        scene.gameObjects[i].sprite.spriteEffect, 
                        depth);
                }
            }

            // Swap Buffer
            batch.End();
            GraphicsDevice.SetRenderTarget(null);
            batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

            batch.Draw(buffer, new Rectangle(0, 0, width, height), Color.White);

            // Draw Textboxes
            for (int i = 0; i < scene.textboxes.Count; i++)
            {
                Textbox text = scene.textboxes[i];
                batch.DrawString(text.font.spriteFont, text.text, text.position, text.color, text.rotation, text.origin, text.scale, text.spriteEffect, text.depth);
            }

            // Draw Images
            for (int i = 0; i < scene.images.Count; i++)
            {
                if (scene.images[i].sprite != null)
                {
                    batch.Draw(scene.images[i].sprite.texture,
                        new Vector2(scene.images[i].transform.position.X, scene.images[i].transform.position.Y),
                        scene.images[i].sprite.bounds,
                        scene.images[i].sprite.color,
                        scene.images[i].transform.rotation,
                        scene.images[i].transform.origin,
                        scene.images[i].transform.scale,
                        SpriteEffects.None,
                        scene.images[i].sprite.depth);
                }
            }

            batch.End();
        }

        // 5. Unload
        protected override void UnloadContent()
        {
            batch.Dispose();
        }
    }
}
