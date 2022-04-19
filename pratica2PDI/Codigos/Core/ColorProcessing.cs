namespace pratica2PDI.Codigos.Core
{
    internal class ColorProcessing
    {
        public static int[,] getFilledMatrix(int height, int width, int defaultValue = 0)
        {
            int[,] output = new int[height, width];

            if (defaultValue == 0) return output;

            for(int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    output[h, w] = defaultValue;
                }
            }

            return output;
        }

        public static (int[,] R, int[,] G, int[,] B) limiarizeChannels(int[,] R, int[,] G, int[,] B, int limiar, int min = 0, int max = 255)
        {
            int[,] ROutput = limiarizeChannel(R, limiar, min, max),
                   GOutput = limiarizeChannel(G, limiar, min, max),
                   BOutput = limiarizeChannel(B, limiar, min, max);

            return (ROutput, GOutput, BOutput);
        }

        public static int[,] limiarizeChannel(int[,] channel, int limiar, int min = 0, int max = 255)
        {
            int[,] output = new int[channel.GetLength(0), channel.GetLength(1)];

            for(int h = 0; h < channel.GetLength(0); h++)
            {
                for(int w = 0; w < channel.GetLength(1); w++)
                {
                    output[h, w] = (channel[h, w] > limiar) ? max : min;
                }
            }

            return output;
        }

        public static (int[,] R, int[,] G, int[,] B) getAllColorChannels(Bitmap original)
        {
            int[,]
                outputR = new int[original.Height, original.Width],
                outputG = new int[original.Height, original.Width],
                outputB = new int[original.Height, original.Width];


            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color pixelColor = original.GetPixel(x, y);
                    outputR[y,x] = pixelColor.R;
                    outputG[y,x] = pixelColor.G;
                    outputB[y,x] = pixelColor.B;
                }
            }

            return (outputR, outputG, outputB);
        }

        public static int[,] getColorChannel(Bitmap original, int channel)//channel -> 1: red; 2: green; 3: blue.
        {
            int[,] output = new int[original.Height, original.Width];

            for(int y = 0; y < original.Height; y++)
            {
                for(int x = 0; x < original.Width; x++)
                {
                    Color pixelColor = original.GetPixel(x, y);
                    output[y,x] =  (channel == 1) ? pixelColor.R : (channel == 2) ? pixelColor.G : pixelColor.B;
                }
            }

            return output;
        }

        public static Bitmap mixColorChannels(int[,] R, int[,] G, int[,] B)
        {
            Bitmap output = new Bitmap(R.GetLength(1), R.GetLength(0));

            for(int y = 0; y < R.GetLength(0); y++)
            {
                for(int x = 0; x < G.GetLength(1); x++)
                {
                    Color pixelColor = Color.FromArgb(R[y, x], G[y, x], B[y, x]);
                    output.SetPixel(x, y, pixelColor);
                }
            }

            return output;
        }

        public static (int[,] R, int[,] G, int[,] B) invertAllChannelsColors(int[,] R, int[,] G, int[,] B)
        {
            int[,]
                ROutput = invertChannelColors(R),
                GOutput = invertChannelColors(G),
                BOutput = invertChannelColors(B);

            return (ROutput, GOutput, BOutput);
        }

        public static int[,] invertChannelColors(int[,] original)
        {
            int[,] output = new int[original.GetLength(0), original.GetLength(1)];

            for (int w = 0; w < original.GetLength(0); w++)
            {
                for (int h = 0; h < original.GetLength(1); h++)
                {
                    if (original[w, h] < 0)
                    {
                        output[w, h] = original[w, h];
                        continue;
                    }
                    output[w,h] = 255 - original[w,h];
                }
            }

            return output;
        }

        public static Bitmap invertImageColor(Bitmap original)
        {
            Bitmap output = new Bitmap(original.Width, original.Height);
            for(int w = 0;w < original.Width; w++)
            {
                for (int h = 0; h < original.Height; h++)
                {
                    Color pixelColor = original.GetPixel(w, h);
                    Color newPixelColor = Color.FromArgb(255 - pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B);
                    output.SetPixel(w, h, newPixelColor);
                }
            }

            return output;
        }

    }
}
