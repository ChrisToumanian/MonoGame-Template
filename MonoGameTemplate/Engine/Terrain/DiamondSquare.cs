using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameTemplate
{
    class DiamondSquare
    {
        private int size;
        private int height;
        private double roughness;
        private double seed;

        // Constructor
        public DiamondSquare(int size, int height, double roughness, double seed)
        {
            this.size = size + 1;
            this.height = height;
            this.roughness = roughness * 100 / 2;
            this.seed = seed * 100;
        }

        // Get Fractal Data
        public int[,] GetData()
        {
            int[,] map = new int[size, size];
            double[,] data = DiamondSquareAlgorithm();

            double max = data.Cast<double>().Max();
            double min = data.Cast<double>().Min();

            for (int x = 0; x < size - 1; x++)
            {
                for (int y = 0; y < size - 1; y++)
                {
                    double n = data[x, y];
                    n += roughness / 100 - 1 + Math.Abs(min) / 100;
                    map[x, y] = (int) n;
                }
            }

            return map;
        }

        // Normalize Data
        public int[,] Normalize(int[,] map)
        {
            int min = map.Cast<int>().Min();
            int max = map.Cast<int>().Max();
            double div = (height * 10) / max;

            for (int x = 0; x < size - 1; x++)
            {
                for (int y = 0; y < size - 1; y++)
                {
                    int n = map[x, y] + Math.Abs(min);
                    map[x, y] = (int)(n * div);
                }
            }

            return map;
        }

        // Diamond Square Algorithm
        private double[,] DiamondSquareAlgorithm()
        {
            double[,] data = new double[size, size];
            data[0, 0] = seed;
            data[0, size - 1] = seed;
            data[size - 1, 0] = seed;
            data[size - 1, size - 1] = seed;
            double h = roughness;
            Random r = new Random();

            for (int sideLength = size - 1; sideLength >= 2; sideLength /= 2, h /= 2.0)
            {
                int halfSide = sideLength / 2;

                for (int x = 0; x < size - 1; x += sideLength)
                {
                    for (int y = 0; y < size - 1; y += sideLength)
                    {
                        double avg = data[x, y] + data[x + sideLength, y] + data[x, y + sideLength] + data[x + sideLength, y + sideLength];
                        avg /= 4.0;
                        data[x + halfSide, y + halfSide] = avg + (r.NextDouble() * 2 * h) - h;
                    }
                }

                for (int x = 0; x < size - 1; x += halfSide)
                {
                    for (int y = 0; y < size - 1; y += halfSide)
                    {
                        double avg = data[(x - halfSide + size) % size, y] + data[(x + halfSide) % size, y] + data[x, (y + halfSide) % size] + data[x, (y - halfSide + size) % size];
                        avg /= 4.0;
                        avg = avg + (r.NextDouble() * 2 * h) - h;
                        data[x, y] = avg;
                        if (x == 0) data[size - 1, y] = avg;
                        if (y == 0) data[x, size - 1] = avg;
                    }
                }
            }

            return data;
        }
    }
}
