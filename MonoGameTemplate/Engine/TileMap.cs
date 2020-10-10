using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameTemplate
{
    class TileMap
    {
        public Tile[,] tiles;
        public List<Tile> tileTypes = new List<Tile>();
        public List<GameObject> gameObjectTypes = new List<GameObject>();
        public List<string> gameObjectColors = new List<string>();
        public int width;
        public int height;
        public int tileWidth;

        public TileMap(int width, int height, int tileWidth)
        {
            this.width = width / tileWidth;
            this.height = height / tileWidth;
            this.tileWidth = tileWidth;
            tiles = new Tile[this.width, this.height];
        }

        public void AddTileType(Sprite sprite, bool collision, string color)
        {
            tileTypes.Add(new Tile(sprite, collision, color));
        }

        public void AddTileType(Texture2D texture, int spriteX, int spriteY, int spriteWidth, bool collision, string color)
        {
            tileTypes.Add(new Tile(new Sprite(texture, spriteX, spriteY, spriteWidth), collision, color));
        }

        public void AddTileType(string name, Texture2D texture, int spriteX, int spriteY, int spriteWidth, bool collision, string color)
        {
            tileTypes.Add(new Tile(new Sprite(texture, spriteX, spriteY, spriteWidth), collision, color, name));
        }

        public void AddTileType(string name, Texture2D texture, int spriteX, int spriteY, int spriteWidth, bool collision, string color, float depth)
        {
            Sprite sprite = new Sprite(texture, spriteX, spriteY, spriteWidth);
            sprite.depth = depth;
            tileTypes.Add(new Tile(sprite, collision, color, name));
        }

        public void SetTile(Tile tile, int x, int y)
        {
            tiles[x, y] = tile;
        }

        public void SetTile(Sprite sprite, int x, int y)
        {
            tiles[x, y] = new Tile(sprite);
        }

        public void SetTile(Sprite sprite, bool collision, int x, int y)
        {
            tiles[x, y] = new Tile(sprite, collision);
        }

        public void DeleteTile(int x, int y)
        {
            tiles[x, y] = null;
        }

        public void FillMap(Sprite sprite)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tiles[x, y] = new Tile(sprite);
                }
            }
        }

        public void CreateMapFromTexture(Texture2D texture)
        {
            string[,] colors = GetHexColorsFromTexture(texture);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    SetTile(GetTileByColor(colors[x, y]), x, y);
                }
            }
        }

        public void AddGameObjectType(GameObject gameObject, string color)
        {
            gameObjectColors.Add(color);
            gameObjectTypes.Add(gameObject);
        }

        public void CreateGameObjectsFromTexture(Texture2D texture, Scene scene)
        {
            string[,] colors = GetHexColorsFromTexture(texture);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int i = 0; i < gameObjectColors.Count; i++)
                    {
                        if (gameObjectColors[i] == colors[x, y])
                        {
                            //scene.AddGameObject(gameObjectTypes[i].Clone<GameObject>(), scene, x * tileWidth, y * tileWidth);
                        }
                    }
                }
            }
        }

        public List<Vector2> GetPositionsByColor(Texture2D texture, string color)
        {
            string[,] colors = GetHexColorsFromTexture(texture);
            List<Vector2> positions = new List<Vector2>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (colors[x, y] == color)
                    {
                        positions.Add(new Vector2(x * tileWidth, y * tileWidth));
                    }
                }
            }
            return positions;
        }

        public Tile GetTileByColor(string color)
        {
            for (int i = 0; i < tileTypes.Count; i++)
            {
                if (color == tileTypes[i].color)
                {
                    return tileTypes[i];
                }
            }
            return null;
        }

        public string[,] GetHexColorsFromTexture(Texture2D texture)
        {
            string[,] array = new string[texture.Width, texture.Height];

            int width = texture.Width;
            int height = texture.Height;

            Color[] c = new Color[width * height];
            texture.GetData(c);

            int i = 0;
            for (int y = 0; y < texture.Height; y++)
            {
                for (int x = 0; x < texture.Width; x++)
                {
                    array[x, y] = c[i].R.ToString("X2").ToLower() + c[i].G.ToString("X2").ToLower() + c[i].B.ToString("X2").ToLower();
                    i++;
                }
            }

            return array;
        }

        public Tile GetTile(int x, int y)
        {
            if (y >= height) return null;
            if (y < 0) return null;
            if (x >= width) return null;
            if (x < 0) return null;
            return tiles[x, y];
        }

        public Tile GetTile(Vector2 position)
        {
            int x = (int)(position.X / tileWidth);
            int y = (int)(position.Y / tileWidth);

            if (y >= height) return null;
            if (y < 0) return null;
            if (x >= width) return null;
            if (x < 0) return null;

            return tiles[x, y];
        }

        public List<Tile> GetTiles(Rectangle rect)
        {
            List<Tile> tiles = new List<Tile>();

            Vector2 topLeft = GetTilePosition(new Vector2(rect.Left, rect.Top));
            Vector2 bottomLeft = GetTilePosition(new Vector2(rect.Left, rect.Bottom));
            Vector2 topRight = GetTilePosition(new Vector2(rect.Right, rect.Top));
            Vector2 bottomRight = GetTilePosition(new Vector2(rect.Right, rect.Bottom));

            for (int x = (int)topLeft.X; x <= topRight.X; x++)
            {
                for (int y = (int)topLeft.Y; y <= bottomLeft.Y; y++)
                {
                    Tile tile = GetTile(x, y);
                    if (tile != null) tiles.Add(tile);
                }
            }

            return tiles;
        }

        public Vector2 GetTilePosition(Vector2 position)
        {
            int x = (int)(position.X / tileWidth);
            int y = (int)(position.Y / tileWidth);
            return new Vector2(x, y);
        }
    }
}
