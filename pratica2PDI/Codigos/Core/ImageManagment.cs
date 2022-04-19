using pratica2PDI.Codigos.UI;

namespace pratica2PDI.Codigos.Core
{
    internal class ImageManagment
    {
        private Dictionary<string, List<int[,]>> outputs = new Dictionary<string, List<int[,]>>();

        public static (int[,] R, int[,] G, int[,] B) joinChannelDialog(int[,] R, int[,] G, int[,] B, int separatedColor, int[,] intersection)
        {
            if (separatedColor == 0) return (R, G, B);

            new exibirImagem(ColorProcessing.mixColorChannels(R, R, R), "R").ShowDialog();
            new exibirImagem(ColorProcessing.mixColorChannels(G, G, G), "G").ShowDialog();
            new exibirImagem(ColorProcessing.mixColorChannels(B, B, B), "B").ShowDialog();

            int[,]
                ROutput = (separatedColor == 1) ? R : MorphologicalImageProcessing.getImageSum(R, intersection, false),
                GOutput = (separatedColor == 2) ? G : MorphologicalImageProcessing.getImageSum(G, intersection, false),
                BOutput = (separatedColor == 3) ? B : MorphologicalImageProcessing.getImageSum(B, intersection, false);

            return (ROutput, GOutput, BOutput);
        }

        public static (int[,] R, int[,] G, int[,] B) separateChannelDialog(int[,] R, int[,] G, int[,] B, out int isolatedColor, out int[,] intersection)
        {
            string[] options = { "Nenhuma", "Vermelho", "Verde", "Azul" };
            ChooseOption cO = new ChooseOption("Ignorar cor:", options);
            cO.ShowDialog();
            isolatedColor = cO.getChoice();

            List<int[,]> channels = new List<int[,]>() { R, G, B };

            if (isolatedColor == 0)
            {
                intersection = null;
                return (R, G, B);
            }

            int[,] invColor = ColorProcessing.invertChannelColors(channels.ElementAt(isolatedColor - 1));
            int[,] channelA = channels.ElementAt(mod((isolatedColor - 2), 3));
            int[,] channelB = channels.ElementAt(mod(isolatedColor, 3));

            intersection = MorphologicalImageProcessing.getIntersection(channelA, channelB);
            intersection = MorphologicalImageProcessing.getImageSum(intersection, invColor, true);

            new exibirImagem(ColorProcessing.mixColorChannels(intersection, intersection, intersection)).ShowDialog();

            int[,] invIntersect = ColorProcessing.invertChannelColors(intersection);

            int[,]
                ROutput = (isolatedColor == 1) ? R : MorphologicalImageProcessing.getImageSum(invIntersect, R, true),
                GOutput = (isolatedColor == 2) ? G : MorphologicalImageProcessing.getImageSum(invIntersect, G, true),
                BOutput = (isolatedColor == 3) ? B : MorphologicalImageProcessing.getImageSum(invIntersect, B, true);

            return (ROutput, GOutput, BOutput);

        }

        public (int[,] R, int[,] G, int[,] B) getInputImageDialog(bool limiarize = false, int limiar = 100)
        {
            int[,] ROutput, GOutput, BOutput;

            string[] options = new string[outputs.Count];
            int i = 0;

            foreach (var o in outputs)
            {
                options[i++] = o.Key;
            }

            ChooseOption cO = new ChooseOption("A partir de qual imagem?", options);
            cO.ShowDialog();
            int choice = cO.getChoice();

            List<int[,]> choicedInput = outputs[options[choice]];
            ROutput = choicedInput.ElementAt(0);
            GOutput = choicedInput.ElementAt(1);
            BOutput = choicedInput.ElementAt(2);

            if (limiarize)
            {
                ROutput = ColorProcessing.limiarizeChannel(ROutput, limiar);
                GOutput = ColorProcessing.limiarizeChannel(GOutput, limiar);
                BOutput = ColorProcessing.limiarizeChannel(BOutput, limiar);
            }

            return (ROutput, GOutput, BOutput);

        }

        public void saveOutput(int[,] R, int[,] G, int[,] B, string key)
        {
            List<int[,]> output = new List<int[,]> { R, G, B };

            if (outputs.ContainsKey(key))
            {
                outputs[key] = output;
                return;
            }

            outputs.Add(key, output);
        }

        public static int[,] getImageBorders(int[,] original)
        {
            int[,] output = new int[original.GetLength(0), original.GetLength(1)];

            for (int w = 0; w < output.GetLength(0); w++)
            {
                for (int h = 0; h < output.GetLength(1); h++)
                {

                    if (w == 0 || w == output.GetLength(0) - 1 || h == 0 || h == output.GetLength(1) - 1)
                    {
                        output[w, h] = original[w, h];
                    }
                    else
                    {
                        output[w, h] = 255;
                    }
                }
            }

            return output;
        }

        public static int countPixelsWithIntensity(int[,] channel, int intensity)
        {
            int sum = 0;

            foreach (int i in channel)
            {
                if (i == intensity) sum++;
            }
            return sum;
        }

        public static int[,] collapseVerticalLines(int[,] channel, int lineColor = 0, int backColor = 255)
        {
            int[,] output = new int[channel.GetLength(0), channel.GetLength(1)];
            Array.Copy(channel, output, channel.Length);

            for (int h = 0; h < output.GetLength(0); h++)
            {
                for (int w = 0; w < output.GetLength(1); w++)
                {
                    if (output[h, w] == lineColor)
                    {
                        for (int hT = h + 1; hT < output.GetLength(0); hT++)
                        {
                            if (output[hT, w] == lineColor)
                            {
                                output[hT, w] = backColor;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return output;
        }

        public static int[,] getVerticalLinesInChannel(int[,] channel, int minHeight)
        {
            int[,] verticalLineDetect = new int[minHeight, 3];

            int[,] output = ColorProcessing.getFilledMatrix(channel.GetLength(0), channel.GetLength(1), 255);

            for (int h = 0; h < minHeight; h++)
            {
                verticalLineDetect[h, 0] = 255;
                verticalLineDetect[h, 1] = 0;
                verticalLineDetect[h, 2] = -1;
            }

            int[,] hitOrMissChannel = MorphologicalImageProcessing.hitOrMiss(channel, out _, verticalLineDetect);
            output = MorphologicalImageProcessing.getImageSum(output, hitOrMissChannel, false);

            return output;
        }

        public static int[,] getAumentedChannel(int[,] originalChannelMatrix, int[] structSize, int intensityToFill = 50)
        {
            //armazenando o tamanho das estruturas para simplicidade nas chamadas
            int[] channelSize = { originalChannelMatrix.GetLength(0), originalChannelMatrix.GetLength(1) };

            int[,] aumented = new int[channelSize[0] + structSize[0], channelSize[1] + structSize[1]];

            for (int h = 0; h < aumented.GetLength(0); h++)
            {
                for (int w = 0; w < aumented.GetLength(1); w++)
                {
                    aumented[h, w] = intensityToFill;
                }
            }

            for (int h = structSize[0] / 2; h < channelSize[0] + (structSize[0] / 2); h++)
            {
                for (int w = structSize[1] / 2; w < channelSize[1] + (structSize[1] / 2); w++)
                {
                    aumented[h, w] = originalChannelMatrix[h - (structSize[0] / 2), w - (structSize[1] / 2)];
                }
            }

            return aumented;
        }

       public static int mod(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}
