using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTemplate.Scenes
{
    class Map : Scene
    {
        Entities.Player player;

        // Constructor
        public Map()
        {
            name = "Map";
            bounds = new Rectangle(0, 0, 2048, 2048);
            backgroundColor = Color.Black;
            camera = new Camera(this);
            camera.Zoom(0.75f);
        }

        // Load
        public override void Load()
        {
            map = new TileMap(bounds.Width, bounds.Height, 16);
            backgroundImage = new Image(Assets.level01background);

            Objects.Ball ball = new Objects.Ball();
            AddGameObject(ball, this, Geometry.RandomInt(0, 32), Geometry.RandomInt(0, 32));

            // Create tile sprites
            map.AddTileType("grass01", Assets.tiles, 1, 1, 16, false, "81d1ad");
            map.AddTileType("grass02", Assets.tiles, 2, 1, 16, false, "6aba96");
            map.AddTileType("grass03", Assets.tiles, 3, 1, 16, false, "56a783");
            map.AddTileType("grass04", Assets.tiles, 4, 1, 16, false, "489673");
            map.AddTileType("grassTopEdge", Assets.tiles, 9, 4, 16, false, "a0e8c8");
            map.AddTileType("flower01", Assets.tiles, 5, 1, 16, false, "ec907a");
            map.AddTileType("flower02", Assets.tiles, 12, 4, 16, false, "b96574");
            map.AddTileType("flower03", Assets.tiles, 9, 7, 16, false, "e0a878");
            map.AddTileType("rock", Assets.tiles, 12, 3, 16, true, "a8b8c8");
            map.CreateMapFromTexture(Assets.level01);

            // Place Trees
            foreach (Vector2 position in map.GetPositionsByColor(Assets.level01, "496a1a"))
            {
                AddGameObject(new Scenery.SmallPineTree(), this, (int)position.X, (int)position.Y);
            }

            // Place Objects
            string[,] colors = map.GetHexColorsFromTexture(Assets.level01);
            colors = map.GetHexColorsFromTexture(Assets.level01objects);
            for (int x = 0; x < map.width; x++)
            {
                for (int y = 0; y < map.height; y++)
                {
                    string c = colors[x, y];
                    if (c == "496a1a")
                    {
                        Scenery.SmallPineTree tree = new Scenery.SmallPineTree();
                        AddGameObject(tree, this, x * map.tileWidth + 8, y * map.tileWidth + 8);
                    }
                }
            }

            // Place Textboxes
            Textbox text = new Textbox(Assets.font, 10, 10, "HEALTH: 100     MANA: 509");
            text.scale = 3;
            textboxes.Add(text);

            // Create Player
            player = new Entities.Player();
            AddGameObject(player, this, 128, 64);
            camera.parent = player.transform;

            loaded = true;
        }

        // Update
        public override void Update(GameTime gameTime)
        {
            // Controls
            if (Controller.IsKeyDown(Keys.Escape)) // Exit game
            {
                Program.game.Exit();
            }

            // Camera Controls
            if (Controller.IsScrollingUp()) // Camera Zoom In
            {
                camera.Zoom(0.96f);
            }
            if (Controller.IsScrollingDown()) // Camera Zoom Out
            {
                camera.Zoom(1.04f);
            }

            // Camera Follow Player
            camera.Update();
        }
    }
}
