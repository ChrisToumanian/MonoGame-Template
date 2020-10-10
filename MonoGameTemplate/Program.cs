using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameTemplate
{
    class Program
    {
        public static Game game;

        static void Main(string[] args)
        {
            // Start Game
            game = new Game();
            game.Run();
        }
    }
}
