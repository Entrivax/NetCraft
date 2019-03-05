using NetCraft.Base.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NetCraft.Base.Worlds
{
    class ChunkGeneratorSurface : IChunkGenerator
    {
        private readonly IChunkManager _chunkManager;

        public ChunkGeneratorSurface(IChunkManager chunkManager)
        {
            _chunkManager = chunkManager;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float MAP(float value, float fromValue, float toValue, float fromResult, float toResult)
        {
            return (value - fromValue) / (toValue - fromValue) * (toResult - fromResult) + fromResult;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float MIN(float a, float b)
        {
            return a < b ? a : b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float MAX(float a, float b)
        {
            return a > b ? a : b;
        }

        public void PopulateChunk(World world, Chunk chunk, ChunkPosition position, bool parallelComputing)
        {
            var rand = new Random((int)world.Seed);
            var perlin = new PerlinNoiseOctaveHelper((rand.Next() << 16) | rand.Next(), 8, 3);
            var mountainPerlin = new PerlinNoiseOctaveHelper((rand.Next() << 16) | rand.Next(), 8, 3);
            var cavePerlin = new PerlinNoiseOctaveHelper((rand.Next() << 16) | rand.Next(), 5, 3);
            var temperaturePerlin = new PerlinNoiseOctaveHelper((rand.Next() << 16) | rand.Next(), 5, 3);
            var humidityPerlin = new PerlinNoiseOctaveHelper((rand.Next() << 16) | rand.Next(), 5, 3);

            if (!parallelComputing)
            {
                for (byte x = 0; x < 16; x++)
                    for (byte z = 0; z < 16; z++)
                    {
                        PerlinGeneration(world.Seed, chunk, position, perlin, mountainPerlin, cavePerlin, temperaturePerlin, humidityPerlin, x, z);
                    }
            }
            else
            {
                Parallel.For(0, 256, (i) =>
                {
                    PerlinGeneration(world.Seed, chunk, position, perlin, mountainPerlin, cavePerlin, temperaturePerlin, humidityPerlin, (byte)(i % 16), (byte)(i / 16));
                });
            }
        }

        private void PerlinGeneration(long seed, Chunk chunk, ChunkPosition position,
            PerlinNoiseOctaveHelper perlin, PerlinNoiseOctaveHelper mountainPerlin, PerlinNoiseOctaveHelper cavePerlin,
            PerlinNoiseOctaveHelper temperaturePerlin, PerlinNoiseOctaveHelper humidityPerlin, byte x, byte z)
        {
            var rand = new Random((int)seed);
            rand = new Random((position.X * 16 + x) * rand.Next() * (position.Z * 16 + z) * rand.Next());
            var frequency = 0.0005f;
            var moutainFrequency = 0.00005f;
            var caveFrequency = 0.003f;
            var temperatureFrequency = 0.0003f;
            var temperature = (byte)MAX(MIN(MAP((float)temperaturePerlin.Noise((position.X * 16 + x) * temperatureFrequency, 0, (position.Z * 16 + z) * temperatureFrequency), -0.5f, 0.5f, 0, 255), 255), 0);

            _chunkManager.SetTemperature(chunk, x, z, temperature);
            var humidityFrequency = 0.0003f;
            var humidity = (byte)MAX(MIN(MAP((float)humidityPerlin.Noise((position.X * 16 + x) * humidityFrequency, 0, (position.Z * 16 + z) * humidityFrequency), -0.5f, 0.5f, 0, 255), 255), 0);
            _chunkManager.SetHumidity(chunk, x, z, humidity);

            var perlinResult = perlin.Noise(((position.X * 16 + x)) * frequency, 0, ((position.Z * 16 + z)) * frequency);
            var mountainPerlinResult = mountainPerlin.Noise(((position.X * 16 + x)) * moutainFrequency, 0, ((position.Z * 16 + z)) * moutainFrequency);
            var perlinHeight = MAP((float)perlinResult, -0.5f, 0.5f, 20, 30) * MAP((float)((mountainPerlinResult + 0.5f) * (temperature / 255f)), 0, 1f, 0.5f, 4);
            if (perlinHeight < 0)
                perlinHeight = 0;
            else if (perlinHeight > 127)
                perlinHeight = 127;
            byte height = (byte)perlinHeight;
            for (byte y = 0; y < height; y++)
            {
                var cavePerlinResult = ((float)cavePerlin.Noise(((position.X * 16 + x)) * caveFrequency, y * caveFrequency, ((position.Z * 16 + z)) * caveFrequency) + 0.5f) * ((y) / 64f);
                cavePerlinResult = MIN(MAP(cavePerlinResult, 0f, 0.1f, 0.1f, 0.2f), cavePerlinResult);
                if (cavePerlinResult > 0.04f)
                {
                    if (temperature > 160 || humidity > 64)
                    {
                        if (y == height - 1)
                        {
                            if (y > 0 && _chunkManager.GetBlockId(chunk, x, (byte)(y - 1), z) != 0 && rand.Next(10) == 0)
                                _chunkManager.SetBlockIdAndMetadata(chunk, x, y, z, 31, 1);
                        }
                        else if (y == height - 2)
                            _chunkManager.SetBlockIdAndMetadata(chunk, x, y, z, 35, 1);
                        else if (y >= height - 5)
                            _chunkManager.SetBlockId(chunk, x, y, z, 3);
                        else
                            _chunkManager.SetBlockId(chunk, x, y, z, 1);
                    }
                    else
                    {
                        if (y == height - 1)
                        {
                            if (rand.Next(30) == 0)
                                _chunkManager.SetBlockIdAndMetadata(chunk, x, y, z, 31, 1);
                        }
                        else if (y < height - 1 && y >= height - 4)
                            _chunkManager.SetBlockId(chunk, x, y, z, 12);
                        else if (y >= height - 6)
                            _chunkManager.SetBlockId(chunk, x, y, z, 24);
                        else
                            _chunkManager.SetBlockId(chunk, x, y, z, 1);
                    }
                }
            }
        }
    }
}
