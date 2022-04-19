using pratica2PDI.Codigos.UI;

namespace pratica2PDI.Codigos.Core
{
    internal class MorphologicalImageProcessing
    {
        public static (int[,] R, int[,] G, int[,] B) getAllChannelsSkeletons(int[,] R, int[,] G, int[,] B)
        {
            int[,]
                ROutput = getChannelSkeleton(R),
                GOutput = getChannelSkeleton(G),
                BOutput = getChannelSkeleton(B);

            return (ROutput, GOutput, BOutput);
        }

        public static int[,] getChannelSkeleton(int[,] channel)
        {
            int[,] output = ColorProcessing.getFilledMatrix(channel.GetLength(0), channel.GetLength(1), 255);

            int[,] erosion = channel;
            int[,] opening;

            List<int[,]> S = new List<int[,]>();

            bool hit;

            do
            {
                int[,] tempErosion = erodeImage(erosion, new int[3, 3], out hit);

                opening = dilateImage(tempErosion, new int[3, 3]);
                int[,] Sk = imageDifference(ColorProcessing.invertChannelColors(erosion), ColorProcessing.invertChannelColors(opening));
                Sk = ColorProcessing.invertChannelColors(Sk);

                erosion = tempErosion;

                S.Add(Sk);

                if (!hit) break;

            } while (hit);

            foreach (int[,] Sk in S){
                output = getImageSum(output, Sk, false);
            }

            return output;
        }

        public static int[,] rotateStructuringElement(int[,] structuringElement)
        {
            int[,] output = new int[structuringElement.GetLength(1), structuringElement.GetLength(0)];

            for (int i = 0; i < structuringElement.GetLength(0); i++){
                for (int j = 0; j < structuringElement.GetLength(1); j++){
                    output[structuringElement.GetLength(1) - j - 1, i] = structuringElement[i, j];
                }
            }

            return output;
        }

        public static (int[,] R, int[,] G, int[,] B) getConvexHull(int[,] R, int[,] G, int[,] B, int[,] structuringElement, int rotations)
        {
            List<int[,]> outputs = new List<int[,]>();
            List<int[,]> channels = new List<int[,]>();

            channels.Add(R);
            channels.Add(G);
            channels.Add(B);


            foreach(int[,] channel in channels)
            {
                List<int[,]> convergences = new List<int[,]>();

                for (int i = 0; i <= rotations; i++)
                {
                    int[,] HitOrMissSum = channel;
                    
                    bool altered = true;
                    int[,] AtualHitOrMiss;

                    do{
                        AtualHitOrMiss = hitOrMiss(HitOrMissSum, out altered, structuringElement);
                        HitOrMissSum = getImageSum(HitOrMissSum, AtualHitOrMiss, false);
                    } while (altered);
                    
                    convergences.Add(HitOrMissSum);
                    structuringElement = rotateStructuringElement(structuringElement);
                }

                int[,] channelOutput = convergences.ElementAt(0);
                convergences.RemoveAt(0);

                foreach(int[,] convergence in convergences)
                {
                    channelOutput = getImageSum(channelOutput, convergence, false);
                }

                outputs.Add(channelOutput);

            }

            return (outputs.ElementAt(0), outputs.ElementAt(1), outputs.ElementAt(2));
        }

        public static int[,] hitOrMiss(int[,] channel, out bool someHit, int[,] B1, int[,] B2 = null)
        {
            int[,] output;

            int[,] structuring = (B2?.GetLength(0) > 0) ? getImageSum(B1, ColorProcessing.invertChannelColors(B2), false) : B1;
           
            output = erodeImage(channel, structuring, out someHit);

            return output;
        }

        public static (int[,] R, int[,] G, int[,] B) hitOrMissAll(int[,] R, int[,] G, int[,] B, out bool someHit, int[,] B1, int[,] B2 = null)
        {
            int[,] ROutput, GOutput, BOutput;

            int[,] structuring = (B2?.GetLength(0) > 0) ? getImageSum(B1, ColorProcessing.invertChannelColors(B2), false) : B1;
            
            bool Rh, Gh, Bh; //hit detect

            ROutput = erodeImage(R, structuring, out Rh);
            GOutput = erodeImage(G, structuring, out Gh);
            BOutput = erodeImage(B, structuring, out Bh);

            someHit = (Rh || Gh || Bh);

            return (ROutput, GOutput, BOutput);
        }

        public static int[,] erodeImage(int[,] originalChannelMatrix, int[,] structuringElement, out bool someHit, int foreIntensity = 0, int backIntesity = 255)
        {
            //armazenando o tamanho das estruturas para simplicidade nas chamadas
            int[] channelSize = { originalChannelMatrix.GetLength(0), originalChannelMatrix.GetLength(1) };
            int[] structSize = { structuringElement.GetLength(0), structuringElement.GetLength(1) };

            int[,] aumented = ImageManagment.getAumentedChannel(originalChannelMatrix, structSize, 50);
            int[,] output = new int[channelSize[0], channelSize[1]];

            someHit = false;

            for (int h = 0; h < channelSize[0]; h++){
                for (int w = 0; w < channelSize[1]; w++){

                    bool contained = true;

                    for (int sH = 0; sH < structSize[0]; sH++){
                        for (int sW = 0; sW < structSize[1]; sW++){

                            if (structuringElement[sH, sW] < 0) continue;

                            if (aumented[h + sH, w + sW] != structuringElement[sH, sW]){
                                contained = false;
                                break;
                            }
                        }
                    }
                    if(!someHit && contained) someHit = true;
                    output[h, w] = (contained) ? foreIntensity : backIntesity;
                }
            }
            return output;
        }

        public static int[,] dilateImage(int[,] originalChannelMatrix, int[,] structuringElement)
        {
            //armazenando o tamanho das estruturas para simplicidade nas chamadas
            int[] channelSize = { originalChannelMatrix.GetLength(0), originalChannelMatrix.GetLength(1) };
            int[] structSize = { structuringElement.GetLength(0), structuringElement.GetLength(1) };

            int[,] aumented = ImageManagment.getAumentedChannel(originalChannelMatrix, structSize, 50);
            int[,] output = new int[channelSize[0], channelSize[1]];

            for (int w = 0; w < channelSize[0]; w++){
                for (int h = 0; h < channelSize[1]; h++){

                    bool present = false;

                    for (int sW = 0; sW < structSize[0]; sW++){
                        for (int sH = 0; sH < structSize[1]; sH++){

                            if (aumented[w + sW, h + sH] == structuringElement[sW, sH]){
                                present = true;
                                break;
                            }

                        }
                    }

                    int midStructIntensity = structuringElement[structSize[0] / 2, structSize[1] / 2];

                    output[w, h] = (present) ? midStructIntensity : 255 - midStructIntensity;
                }
            }
            return output;
        }

        public static int[,] getIntersection(int[,] A, int[,] B)
        {
            int[,] output = new int[A.GetLength(0), A.GetLength(1)];

            for (int w = 0; w < A.GetLength(0); w++){
                for (int h = 0; h < A.GetLength(1); h++){
                   
                    if (A[w, h] == B[w, h]){
                        output[w, h] = A[w, h];
                    }else{
                        output[w, h] = 255;
                    }
                }
            }

            return output;
        }

        public static int[,] getImageSum(int[,] A, int[,] B, bool useMax = true)
        {
            int[,] output = new int[A.GetLength(0), A.GetLength(1)];

            for (int w = 0; w < A.GetLength(0); w++){
                for (int h = 0; h < A.GetLength(1); h++){

                    if (A[w, h] == -1 && B[w, h] == -1){
                        output[w, h] = -1;
                        continue;
                    }

                    if(A[w, h] > B[w, h]){
                        output[w,h] = (useMax) ? A[w, h] : (B[w, h]>=0)? B[w, h] : A[w, h];
                    }else{
                        output[w, h] = (useMax) ? B[w, h] : (A[w, h] >= 0) ? A[w, h] : B[w, h];
                    }
                }
            }

            return output;
        }

        public static (int[,] R, int[,] G, int[,] B) fillHolesInAllChannels(int[,] R, int[,] G, int[,] B, int iterations = -1)
        {
            int[,]
                ROutput = fillHoles(R, iterations),
                GOutput = fillHoles(R, iterations),
                BOutput = fillHoles(R, iterations);

            return (ROutput, GOutput, BOutput);
        }

        public static int[,] fillHoles(int[,] original, int iterations = -1)
        {
            if (iterations < 0) iterations = Math.Max(original.GetLength(0), original.GetLength(1)) / 2;

            int[,] inverted = ColorProcessing.invertChannelColors(original);
            int[,] structure = new int[3, 3];

            int[,] dilation = ImageManagment.getImageBorders(inverted);

            for (int i = 0; i < iterations; i++)
            {
                dilation = dilateImage(dilation, structure);
                dilation = getIntersection(dilation, inverted);
            }

            int[,] output = ColorProcessing.invertChannelColors(dilation);
            output = getImageSum(output, original, false);

            return output;
        }

        public static int[,] imageDifference(int[,] A, int[,] B)
        {
            int[,] output = new int[A.GetLength(0), B.GetLength(1)];

            for(int h = 0; h < output.GetLength(0); h++){
                for (int w = 0; w < output.GetLength(1); w++){
                    output[h, w] = A[h, w] - B[h, w];
                }
            }

            return output;
        }
    }
}
