using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCraft.Base.Helpers
{
    class PerlinNoiseHelper
    {
        public long Seed { set { _random = new Random((int)value); ComputePermutations(out _permutations); } }
        public double Repeat { get; set; } = 512;
        private Random _random;
        private int[] _permutations;

        public PerlinNoiseHelper(long seed)
        {
            Seed = seed;
        }

        private void ComputePermutations(out int[] permutations)
        {
            permutations = Enumerable.Range(0, 512).Select(i => i / 2).ToArray();

            for (var i = 0; i < permutations.Length; i++)
            {
                var source = _random.Next(permutations.Length);

                var t = permutations[i];
                permutations[i] = permutations[source];
                permutations[source] = t;
            }
        }

        public int GetNextSeed()
        {
            return _random.Next();
        }

        private static double fade(double t) { return t * t * t * (t * (t * 6 - 15) + 10); }

        private static double grad(int hash, double x, double y, double z)
        {
            int h = hash & 15;                        // CONVERT LO 4 BITS OF HASH CODE
            double u = h < 8 ? x : y,                 // INTO 12 GRADIENT DIRECTIONS.
                   v = h < 4 ? y : h == 12 || h == 14 ? x : z;
            return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
        }

        private static double lerp(double t, double a, double b) { return a + t * (b - a); }

        public double Noise(double x, double y, double z)
        {
            x = x >= 0 ? x : -x;
            y = y >= 0 ? y : -y;
            z = z >= 0 ? z : -z;
            int X = (int)Math.Floor(x) & 255,                  // FIND UNIT CUBE THAT
                  Y = (int)Math.Floor(y) & 255,                // CONTAINS POINT.
                  Z = (int)Math.Floor(z) & 255;
            x -= Math.Floor(x);                                // FIND RELATIVE X,Y,Z
            y -= Math.Floor(y);                                // OF POINT IN CUBE.
            z -= Math.Floor(z);
            double u = fade(x),                                // COMPUTE FADE CURVES
                   v = fade(y),                                // FOR EACH OF X,Y,Z.
                   w = fade(z);
            int A = _permutations[X] + Y, AA = _permutations[A] + Z, AB = _permutations[A + 1] + Z,      // HASH COORDINATES OF
                B = _permutations[X + 1] + Y, BA = _permutations[B] + Z, BB = _permutations[B + 1] + Z;      // THE 8 CUBE CORNERS,

            return lerp(w, lerp(v, lerp(u, grad(_permutations[AA], x, y, z),  // AND ADD
                                           grad(_permutations[BA], x - 1, y, z)), // BLENDED
                                   lerp(u, grad(_permutations[AB], x, y - 1, z),  // RESULTS
                                           grad(_permutations[BB], x - 1, y - 1, z))),// FROM  8
                           lerp(v, lerp(u, grad(_permutations[AA + 1], x, y, z - 1),  // CORNERS
                                           grad(_permutations[BA + 1], x - 1, y, z - 1)), // OF CUBE
                                   lerp(u, grad(_permutations[AB + 1], x, y - 1, z - 1),
                                           grad(_permutations[BB + 1], x - 1, y - 1, z - 1))));
        }
    }
}
