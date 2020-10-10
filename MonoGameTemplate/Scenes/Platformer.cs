using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTemplate.Scenes
{
    class Platformer : Scene
    {
        Textbox textHealth;

        // Constructor
        public Platformer()
        {
            name = "Platformer";
            bounds = new Rectangle(0, 0, 2048, 1024);
            map = new TileMap(bounds.Width, bounds.Height, 16);
            gameObjects = new List<GameObject>();
            backgroundColor = Color.LightBlue;
            camera = new Camera(this);
            camera.Zoom(1);
        }

        // Load
        public override void Load()
        {
            // Place Tiles
            map.AddTileType("ground top", Assets.tiles, 2, 9, 16, true, "fcb5a8");
            map.AddTileType("ground bottom", Assets.tiles, 2, 10, 16, true, "fc8e2e");
            map.AddTileType("gold brick", Assets.tiles, 3, 9, 16, true, "f6e377");
            map.CreateMapFromTexture(Assets.platformerLevel01);

            // Place GameObjects
            foreach (Vector2 position in map.GetPositionsByColor(Assets.platformerLevel01, "f95151"))
            {
                AddGameObject(new Objects.Ball(), this, (int) position.X, (int) position.Y);
            }

            // Textboxes
            textHealth = new Textbox(Assets.font, 10, 10, "HEALTH: 100");
            textHealth.scale = 1.5f;
            textboxes.Add(textHealth);

            // Create Player
            player = new Entities.Mario();
            AddGameObject(player, this, 64, 900);
            camera.parent = player.transform;
        }

        // Update
        public override void Update(GameTime gameTime)
        {
            // Controls
            if (Controller.IsKeyDown(Keys.Escape)) // Exit game
            {
                Program.game.Exit();
            }

            // Camera Follow Player
            camera.Update();
        }
    }
}
