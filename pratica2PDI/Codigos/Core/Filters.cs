﻿namespace pratica2PDI.Codigos.Core
{
    internal class Filters
    {
        public static int[,] aplyMedianFilter(int[,] original, int filterSize)
        {
            int[,] aumentada = ImageManagment.getAumentedChannel(original, new int[] { filterSize, filterSize }, -1);

            int[,] output = new int[original.GetLength(0), original.GetLength(1)];
            int[] intensities = new int[(int)Math.Pow(filterSize, 2)];

            for (int w = 0; w < original.GetLength(0); w++)
            {
                for (int h = 0; h < original.GetLength(1); h++)
                {
                    int n = -1;
                    int lastValid = -1;

                    for (int i = 0; i < filterSize; i++)
                    {
                        for (int j = 0; j < filterSize; j++)
                        {
                            int pixelIntensity = aumentada[w + i, h + j];

                            if (pixelIntensity < 0){
                                pixelIntensity = 999999999;
                            }
                            else
                            {
                                lastValid++;
                            }

                            intensities[++n] = pixelIntensity;
                        }
                    }

                    Array.Sort(intensities);
                    int center = lastValid/ 2;
                    
                    //int med = (totalSize % 2 == 0) ? (int)(colors[center] + colors[center + 1]) / 2 :
                    //    colors[center];

                    int newIntensity = intensities[center];
                    //if(w == original.GetLength(0) - 1)
                    //MessageBox.Show(t + " centro: " + center +  " : " + newIntensity);

                    output[w, h] = newIntensity;
                }
            }

            return output;
        }


    }
}
