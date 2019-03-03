using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCraft.Base.Helpers
{
    class PerlinNoiseOctaveHelper
    {
        private PerlinNoiseHelper[] _perlins;
        private int _octaves;
        private double _persistence;

        public PerlinNoiseOctaveHelper(long seed, int octaves, double persistence)
        {
            long prevSeed = seed;

            _persistence = persistence;
            _octaves = octaves;
            _perlins = new PerlinNoiseHelper[octaves];
            for (int i = 0; i < octaves; i++)
            {
                _perlins[i] = new PerlinNoiseHelper(prevSeed);
                prevSeed = _perlins[i].GetNextSeed();
            }
        }

        public double Noise(double x, double y, double z)
        {
            double total = 0;
            double frequency = 1;
            double amplitude = 1;
            double maxValue = 0;  // Used for normalizing result to 0.0 - 1.0
            for (int i = 0; i < _octaves; i++)
            {
                total += _perlins[i].Noise(x * frequency + 172.264155 * i / _octaves, y * frequency + 41.564324 * i / _octaves, z * frequency + 112.890239 * i / _octaves) * amplitude;

                maxValue += amplitude;

                amplitude *= _persistence;
                frequency *= 2;
            }

            return total / maxValue;
        }
    }
}
