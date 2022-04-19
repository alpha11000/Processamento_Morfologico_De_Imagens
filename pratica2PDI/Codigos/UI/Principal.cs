using pratica2PDI.Codigos.Core;
using System.Windows.Forms;

namespace pratica2PDI.Codigos.UI
{
    public partial class Principal : Form
    {
        private OpenFileDialog ofd;
        private Image image;
        private Bitmap bitmapImage;

        ImageManagment iM = new ImageManagment();

        private int[,] R, G, B;

        public Principal()
        {
            ofd = new OpenFileDialog();
            InitializeComponent();
        }

        private void OpenImageButton_Click(object sender, EventArgs e)
        {
            if(ofd.ShowDialog() == DialogResult.OK){
                try{
                    image = Image.FromFile(ofd.FileName);
                    bitmapImage = new Bitmap(image);
                    DiretorioText.Text = ofd.FileName;

                    var channels = ColorProcessing.getAllColorChannels(bitmapImage);

                    R = channels.R;
                    G = channels.G;
                    B = channels.B;

                    iM.saveOutput(R, G, B, "Imagem Original");

                    var invertedChannels = ColorProcessing.invertAllChannelsColors(R, G, B);
                    iM.saveOutput(invertedChannels.R, invertedChannels.G, invertedChannels.B, "Negativo da imagem");


                    new exibirImagem(bitmapImage).Show();

                }catch(Exception ex){
                    MessageBox.Show("Formato de arquivo não suportado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Teste_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var inputResult = iM.getInputImageDialog(true);

            int[,] Ri = inputResult.R,
                   Gi = inputResult.G,
                   Bi = inputResult.B;

            int size = -1;

            /*if (MessageBox.Show("Limitar iterações?", ":)", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //////////////
            }*/

            int[,] Rb = MorphologicalImageProcessing.fillHoles(Ri, size);
            int[,] Gb = MorphologicalImageProcessing.fillHoles(Gi, size);
            int[,] Bb = MorphologicalImageProcessing.fillHoles(Bi, size);

            Bitmap output = ColorProcessing.mixColorChannels(Rb, Gb, Bb);

            new exibirImagem(output).Show();
            watch.Stop();
            MessageBox.Show($"in {watch.ElapsedMilliseconds / 1000} seconds");
        
        }

        private void removerPontos(object sender, EventArgs e)
        {
            int erosionRepetitions = 0;
            _ = int.TryParse(erosoesSucessivas.Text, out erosionRepetitions);

            int structSize;
            if(!int.TryParse(estruturanteTam.Text, out structSize)) structSize = 3;

            int[,] structured = new int[structSize, structSize];

            var inputLimiarizedChannels = iM.getInputImageDialog(true, 100);

            int[,]
                Ri = inputLimiarizedChannels.R,
                Gi = inputLimiarizedChannels.G,
                Bi = inputLimiarizedChannels.B;

            int[,] erodedR = Ri,
                   erodedG = Gi,
                   erodedB = Bi;

            for (int i = 0; i < erosionRepetitions - 1; i++)
            {
                erodedR = MorphologicalImageProcessing.erodeImage(erodedR, structured, out _);
                erodedG = MorphologicalImageProcessing.erodeImage(erodedG, structured, out _);
                erodedB = MorphologicalImageProcessing.erodeImage(erodedB, structured, out _);
            }

            int[,]  ROutput = erodedR,
                    GOutput = erodedG,
                    BOutput = erodedB;

            if (MessageBox.Show("aplicar filtro da mediana?", "Mediana", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int medianSize;
                if(!int.TryParse(MedianaTam.Text, out medianSize)) medianSize = 3;

                ROutput = Filters.aplyMedianFilter(ROutput, medianSize);
                GOutput = Filters.aplyMedianFilter(GOutput, medianSize);
                BOutput = Filters.aplyMedianFilter(BOutput, medianSize);
            }

            if (MessageBox.Show("aplicar dilatação?", "Dilatação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                for(int i = 0; i < erosionRepetitions; i++)
                {
                    ROutput = MorphologicalImageProcessing.dilateImage(ROutput, structured);
                    GOutput = MorphologicalImageProcessing.dilateImage(GOutput, structured);
                    BOutput = MorphologicalImageProcessing.dilateImage(BOutput, structured);
                }
            }

            iM.saveOutput(ROutput, GOutput, BOutput, "Pontos Removidos");

            Bitmap output = ColorProcessing.mixColorChannels(ROutput, GOutput, BOutput);
            new exibirImagem(output).Show();

        }

        private void erodeTest_Click_Experimental(object sender, EventArgs e)
        {
            int structSize = int.Parse(estruturanteTam.Text.ToString());
            int[,] structred = new int[structSize, structSize];

            var colorChannels = ColorProcessing.getAllColorChannels(bitmapImage);

            int[,]
                R = colorChannels.R,
                G = colorChannels.G,
                B = colorChannels.B;

            int[,] eR = MorphologicalImageProcessing.erodeImage(R, structred, out _);
            int[,] eG = MorphologicalImageProcessing.erodeImage(G, structred, out _);
            int[,] eB = MorphologicalImageProcessing.erodeImage(B, structred, out _);

            Bitmap erode = ColorProcessing.mixColorChannels(eR, eG, eB);
            new exibirImagem(erode, "erode 1").Show();

            int[,] dR = MorphologicalImageProcessing.dilateImage(eR, structred);
            int[,] dG = MorphologicalImageProcessing.dilateImage(eG, structred);
            int[,] dB = MorphologicalImageProcessing.dilateImage(eB, structred);

            Bitmap dilate = ColorProcessing.mixColorChannels(dR, dG, dB);
            new exibirImagem(dilate, "dilate 1").Show();

            int[,] d2R = MorphologicalImageProcessing.dilateImage(dR, structred);
            int[,] d2G = MorphologicalImageProcessing.dilateImage(dG, structred);
            int[,] d2B = MorphologicalImageProcessing.dilateImage(dB, structred);

            Bitmap dilate2 = ColorProcessing.mixColorChannels(d2R, d2G, d2B);
            new exibirImagem(dilate2, "dilate 2").Show();

            int[,] e2R = MorphologicalImageProcessing.erodeImage(d2R, structred, out _);
            int[,] e2G = MorphologicalImageProcessing.erodeImage(d2G, structred, out _);
            int[,] e2B = MorphologicalImageProcessing.erodeImage(d2B, structred, out _);

           // noDots = ColorProcessing.mixColorChannels(e2R, e2G, e2B);
           // new exibirImagem(noDots, "erode 2").Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[,] structuringElement = new int[,] { { 0,  -1,  -1 },
                                                     { 0,  255, -1 },
                                                     { 0, - 1,  -1 } };

            var colorChannels = ColorProcessing.getAllColorChannels(bitmapImage);
            var limiarizedChannels = ColorProcessing.limiarizeChannels(colorChannels.R, colorChannels.G, colorChannels.B, 100);

            int[,]
                R = limiarizedChannels.R,
                G = limiarizedChannels.G,
                B = limiarizedChannels.B;

            var convexResults = MorphologicalImageProcessing.getConvexHull(R, G, B, structuringElement, 3);

            int[,]
                conR = convexResults.R,
                conG = convexResults.G,
                conB = convexResults.B;

            
            Bitmap convex = ColorProcessing.mixColorChannels(conR, conG, conB);
            new exibirImagem(convex, "CONVEX").Show();
        }

        //Fecho Convexo
        private void fechoConvexoButton(object sender, EventArgs e)
        {
            int[,] structuringElement = new int[,] { { 0,  -1,  -1 },{ 0,  255, -1 },{ 0, - 1,  -1 } };

            var inputLimiarizedChannels = iM.getInputImageDialog(true, 100);

            int[,] 
                Ri = inputLimiarizedChannels.R,
                Gi = inputLimiarizedChannels.G,
                Bi = inputLimiarizedChannels.B,
                intersection;

            int separatedColor;
            var separated = ImageManagment.separateChannelDialog(Ri, Gi, Bi, out separatedColor, out intersection);

            Ri = separated.R;
            Gi = separated.G;
            Bi = separated.B;

            var convexResults = MorphologicalImageProcessing.getConvexHull(Ri, Gi, Bi, structuringElement, 3);
            var joinedChannel = ImageManagment.joinChannelDialog(convexResults.R, convexResults.G, convexResults.B, separatedColor, intersection);

            int[,]
                convR = joinedChannel.R,
                convG = joinedChannel.G,
                convB = joinedChannel.B;

            iM.saveOutput(convR, convG, convB, "Fecho Convexo");

            Bitmap convexHullImage = ColorProcessing.mixColorChannels(convR, convG, convB);
            new exibirImagem(convexHullImage, "Fecho Convexo").Show();
        }

        //Skeleton
        private void button4_Click(object sender, EventArgs e)
        {
            var inputLimiarizedChannels = iM.getInputImageDialog(true, 100);

            int[,]
                Ri = inputLimiarizedChannels.R,
                Gi = inputLimiarizedChannels.G,
                Bi = inputLimiarizedChannels.B,
                intersection;

            int separatedColor;
            var separated = ImageManagment.separateChannelDialog(Ri, Gi, Bi, out separatedColor, out intersection);

            Ri = separated.R;
            Gi = separated.G;
            Bi = separated.B;

            var skeletons = MorphologicalImageProcessing.getAllChannelsSkeletons(Ri, Gi, Bi);
            var joinedChannel = ImageManagment.joinChannelDialog(skeletons.R, skeletons.G, skeletons.B, separatedColor, intersection);
            
            int[,]
                skR = joinedChannel.R,
                skG = joinedChannel.G,
                skB = joinedChannel.B;

            Bitmap skeletonImage = ColorProcessing.mixColorChannels(skR, skG, skB);
            new exibirImagem(skeletonImage, "Skeleton").Show();
        }

        
    }
}