using System;

namespace pratica2PDI
{
    internal class FiltersTests
    {
        public static Bitmap aplyMedianFilter(Bitmap original, int filterSize)
        {
            Bitmap aumentada = new Bitmap(original.Width + filterSize - 1, original.Height + filterSize - 1);
            Bitmap output = new Bitmap(aumentada.Width, aumentada.Height);
            int[] colors = new int[(int)Math.Pow(filterSize, 2)];
            int totalSize = colors.Length;
            int factor = (filterSize - 1) / 2;

            for (int w = 0; w < aumentada.Width; w++)
            {
                for (int h = 0; h < aumentada.Height; h++)
                {
                    aumentada.SetPixel(w, h, Color.White);
                }
            }


            for (int w = 0; w < original.Width; w++)
            {
                for (int h = 0; h < original.Height; h++)
                {
                    aumentada.SetPixel(w + factor, h + factor, original.GetPixel(w, h));
                }
            }

            

            for (int w = 0; w < original.Width; w++)
            {
                for(int h = 0; h < original.Height; h++)
                {
                    int n = 0;

                    for(int i = 0; i < filterSize; i++)
                    {
                        for(int j = 0; j < filterSize; j++)
                        {
                            Color pixel = aumentada.GetPixel(w + i, h + j);
                            colors[n++] = pixel.R;
                        }
                    }

                    Array.Sort(colors);

                    int center = totalSize / 2;
                    //int med = (totalSize % 2 == 0) ? (int)(colors[center] + colors[center + 1]) / 2 :
                    //    colors[center];

                    int med = colors[center];

                    Color color = Color.FromArgb(med, med, med);
                    output.SetPixel(w, h, color);
                }
            }

            return output;
        }
    }
}
