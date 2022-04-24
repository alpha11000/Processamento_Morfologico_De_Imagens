using pratica2PDI.Codigos.UI;

namespace pratica2PDI.Codigos.Core
{
    internal class ImageManagment
    {
        private Dictionary<string, List<int[,]>> outputs = new Dictionary<string, List<int[,]>>();

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

        public static bool compareChannels(int[,] A, int[,] B)//return true or false - equal or not
        {
            for(int h = 0; h < A.GetLength(0); h++)
            {
                for(int w = 0; w < A.GetLength(1); w++)
                {
                    if (A[h, w] != B[h, w]) 
                        return false;
                }
            }

            return true;
        }

        public static (int[,] R, int[,] G, int[,] B) separateChannelDialog(int[,] R, int[,] G, int[,] B, out int separatedColor, out int[,] ABIntersection, out int[,] CFactor)
        {
            string[] options = { "Nenhuma", "Vermelho", "Verde", "Azul" };
            ChooseOption cO = new ChooseOption("Ignorar cor:", options);
            cO.ShowDialog();
            separatedColor = cO.getChoice();

            if (separatedColor == 0)
            {
                ABIntersection = null;
                CFactor = null;
                return (R, G, B);
            }

            int APos = mod((separatedColor - 2), 3),
                BPos = mod(separatedColor, 3),
                CPos = mod(separatedColor - 1, 3);

            List<int[,]> channels = new List<int[,]>() { R, G, B };

            int[,] channelA = channels.ElementAt(APos),
                   channelB = channels.ElementAt(BPos),
                   channelC = channels.ElementAt(CPos);

            int[,] invA = ColorProcessing.invertChannelColors(channelA);
            int[,] invB = ColorProcessing.invertChannelColors(channelB);
            int[,] invC = ColorProcessing.invertChannelColors(channelC); //Cc

            ABIntersection = MorphologicalImageProcessing.getIntersection(channelA, channelB); //I

            int[,] tmpOperations = MorphologicalImageProcessing.getChannelsSum(ABIntersection, invC, true);
            tmpOperations = ColorProcessing.invertChannelColors(tmpOperations); //Mc
            tmpOperations = MorphologicalImageProcessing.imageDifference(tmpOperations, invC);

            int[,] outA = MorphologicalImageProcessing.getIntersection(invB, channelA);
            int[,] outB = MorphologicalImageProcessing.getIntersection(invA, channelB);
            int[,] CIntersection = MorphologicalImageProcessing.getChannelsSum(outA, outB, false);

            int[,] tmpDifference = MorphologicalImageProcessing.imageDifference(ABIntersection, invC);
            tmpDifference = ColorProcessing.invertChannelColors(tmpDifference);

            CFactor = MorphologicalImageProcessing.getIntersection(CIntersection, tmpDifference);

            int[,] outC = channelC;


            int[,]  ROutput = (CPos == 0) ? outC : (BPos == 0) ? outB : outA,
                    GOutput = (CPos == 1) ? outC : (BPos == 1) ? outB : outA,
                    BOutput = (CPos == 2) ? outC : (BPos == 2) ? outB : outA;


            return (ROutput, GOutput, BOutput);

        }

        public static (int[,] R, int[,] G, int[,] B) joinChannelDialog(int[,] R, int[,] G, int[,] B, int separatedColor, int[,] ABintersection, int[,] CFactor, (int[,] R, int[,] G, int[,] B) separated, (int[,] R, int[,] G, int[,] B) original, bool skeleton = false, int overridingC = 0)
        {
            if (separatedColor == 0) return (R, G, B);

            int[,] empty = ColorProcessing.getFilledMatrix(R.GetLength(0), R.GetLength(1), 0);

            List<int[,]> channels = new List<int[,]>() { R, G, B };
            List<int[,]> separatedList = new List<int[,]> { separated.R, separated.G, separated.B };
            List<int[,]> originalList = new List<int[,]> { original.R, original.G, original.B };

            int APos = mod((separatedColor - 2), 3),
                BPos = mod(separatedColor, 3),
                CPos = mod(separatedColor - 1, 3);

            int[,] channelA = channels.ElementAt(APos);
            int[,] channelB = channels.ElementAt(BPos);
            int[,] channelC = channels.ElementAt(CPos);

            int[,] intersectionAC = MorphologicalImageProcessing.getIntersection(channelA, channelC);
            intersectionAC = ColorProcessing.invertChannelColors(intersectionAC);
            
            int[,] intersectionBC = MorphologicalImageProcessing.getIntersection(channelB, channelC);
            intersectionBC = ColorProcessing.invertChannelColors(intersectionBC);
            
            int[,] invABIntersection = ColorProcessing.invertChannelColors(ABintersection);

            int[,] outA = MorphologicalImageProcessing.getChannelsSum(channelA, ABintersection, false),
                   outB = MorphologicalImageProcessing.getChannelsSum(channelB, ABintersection, false),
                   outC = MorphologicalImageProcessing.getChannelsSum(invABIntersection, channelC, true);

            outA = MorphologicalImageProcessing.getChannelsSum(outA, intersectionBC, true); //Max
            outB = MorphologicalImageProcessing.getChannelsSum(outB, intersectionAC, true); //Max

            if (skeleton)
            {
                int overlayChannel = channelOverlayDialog(separatedColor);

                if(overlayChannel != 0)
                {
                    int[,] separatedCh = (overlayChannel == 2) ? separatedList.ElementAt(APos) : separatedList.ElementAt(BPos);
                    int[,] ch = (overlayChannel == 2) ? channelA : channelB;
                    ch = ColorProcessing.invertChannelColors(ch);

                    int[,] cFactorIntersection = MorphologicalImageProcessing.getIntersection(CFactor, separatedCh);
                    cFactorIntersection = MorphologicalImageProcessing.getChannelsSum(cFactorIntersection, ch, true);

                    outA = MorphologicalImageProcessing.getChannelsSum(outA, cFactorIntersection, false);
                    outB = MorphologicalImageProcessing.getChannelsSum(outB, cFactorIntersection, false);
                }
            }


            int[,] ROutput = (CPos == 0) ? outC : (BPos == 0) ? outB : outA,
                   GOutput = (CPos == 1) ? outC : (BPos == 1) ? outB : outA,
                   BOutput = (CPos == 2) ? outC : (BPos == 2) ? outB : outA;

            return (ROutput, GOutput, BOutput);
        }


        public static int channelOverlayDialog(int separatedColor)
        {
            if (separatedColor < 1 || separatedColor > 3)
            {
                return 0;
            }

            if (MessageBox.Show("Na imagem de entrada existe alguma sobreposição com objetos da cor escolhida?", "", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return 0;
            }

            int APos = mod((separatedColor - 2), 3),
                BPos = mod(separatedColor, 3),
                CPos = mod(separatedColor - 1, 3);


            string[] originalOptions = { "Vermelho", "Verde", "Azul" };
            string[] options = { originalOptions[APos], originalOptions[BPos] };

            ChooseOption cO = new ChooseOption("cor do objeto sobrepondo:", options);
            cO.ShowDialog();
            int choice = cO.getChoice();

            return choice + 1;
        }

        public void deleteOutputs()
        {
            outputs.Clear();
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
