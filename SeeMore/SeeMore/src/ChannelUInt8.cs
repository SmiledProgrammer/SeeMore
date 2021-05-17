﻿using System;

namespace SeeMore
{
    public class ChannelUInt8 : Channel<byte>
    {
        public ChannelUInt8(uint width, uint height) : base(width, height)
        { }

        public override Channel<byte> Clone()
        {
            ChannelUInt8 clone = new ChannelUInt8(Width, Height);
            for (uint x = 0; x < Width; x++)
            {
                for (uint y = 0; y < Height; y++)
                {
                    clone.Pixels[x, y] = Pixels[x, y];
                }
            }
            return clone;
        }

        public override void Average(Channel<byte> originalChannel, GenericImage<byte>.NeighborhoodFunction neighborhoodFunction, uint x, uint y)
        {
            ChannelUInt8 castedOriginalChannel = (ChannelUInt8)originalChannel;
            ushort sum = 0;
            byte count = 0;
            Action<byte> filterFunction = (p) =>
            {
                sum += (ushort)p;
                count++;
            };
            neighborhoodFunction(castedOriginalChannel.Pixels, x, y, filterFunction);
            byte average = (byte)(sum / count);
            Pixels[x, y] = average;
        }

        public override void Median(Channel<byte> originalChannel, GenericImage<byte>.NeighborhoodFunction neighborhoodFunction, uint x, uint y, uint neighborhoodSize)
        {
            ChannelUInt8 castedOriginalChannel = (ChannelUInt8)originalChannel;
            byte[] pixels = new byte[neighborhoodSize * neighborhoodSize];
            byte count = 0;
            Action<byte> filterFunction = (p) =>
            {
                pixels[count] = p;
                count++;
            };
            neighborhoodFunction(castedOriginalChannel.Pixels, x, y, filterFunction);
            Array.Sort(pixels);
            byte median = pixels[count / 2];
            Pixels[x, y] = median;
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