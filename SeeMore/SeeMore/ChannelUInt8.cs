using System;

namespace SeeMore
{
    public class ChannelUInt8 : Channel<byte>
    {
        public ChannelUInt8(uint width, uint height) : base(width, height)
        {}

        public override void Apply(Func<byte[,], int, int, byte> operation)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Pixels[x, y] = operation(Pixels, x, y);
                }
            }
        }

        public override void Print() //tmp
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write(Pixels[x, y] + "   ");
                }
                Console.WriteLine();
            }
        }

        public override void Randomize() //tmp
        {
            Random rand = new Random();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Pixels[x, y] = (byte)rand.Next(0, 255);
                }
            }
        }
    }
}
