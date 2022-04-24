using pratica2PDI.Codigos.Core;

namespace pratica2PDI.Codigos.UI
{
    public partial class Principal : Form
    {
        private OpenFileDialog ofd;
        ImageManagment iM = new ImageManagment();

        private int[,] R, G, B;

        public Principal()
        {
            ofd = new OpenFileDialog();
            InitializeComponent();
        }

        //Abrir arquivo de imagem
        private void OpenImageButton_Click(object sender, EventArgs e)
        {
            if(ofd.ShowDialog() == DialogResult.OK){
                try{
                    Image image = Image.FromFile(ofd.FileName);
                    Bitmap bitmapImage = new Bitmap(image);
                    DiretorioText.Text = ofd.FileName;

                    var channels = ColorProcessing.getAllColorChannels(bitmapImage);

                    R = channels.R;
                    G = channels.G;
                    B = channels.B;

                    iM.deleteOutputs();
                    iM.saveOutput(R, G, B, "Imagem Original");

                    var invertedChannels = ColorProcessing.invertAllChannelsColors(R, G, B);
                    iM.saveOutput(invertedChannels.R, invertedChannels.G, invertedChannels.B, "Negativo da imagem");


                    new exibirImagem(bitmapImage).Show();

                }catch(Exception ex){
                    MessageBox.Show("Formato de arquivo não suportado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Remover pontos na imagem
        private void removerPontos(object sender, EventArgs e)
        {
            int structSizeH;
            if (!int.TryParse(estruturanteTamM.Text, out structSizeH)) structSizeH = 3;

            int structSizeW;
            if (!int.TryParse(estruturanteTamN.Text, out structSizeW)) structSizeW = 3;

            int[,] structured = new int[structSizeH, structSizeW];

            var inputLimiarizedChannels = iM.getInputImageDialog(true, 100);

            int[,]
                Ri = inputLimiarizedChannels.R,
                Gi = inputLimiarizedChannels.G,
                Bi = inputLimiarizedChannels.B;

            var eroded = MorphologicalImageProcessing.erodeAllChannels(Ri, Gi, Bi, structured);

            int[,]
                erodedR = eroded.R,
                erodedG = eroded.G,
                erodedB = eroded.B;

            int[,] ROutput = erodedR,
                    GOutput = erodedG,
                    BOutput = erodedB;

            if (MessageBox.Show("aplicar dilatação?", "Dilatação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var dilated = MorphologicalImageProcessing.dilateAllChannels(erodedR, erodedG, erodedB, structured);
                ROutput = dilated.R;
                GOutput = dilated.G;
                BOutput = dilated.B;
            }

            iM.saveOutput(ROutput, GOutput, BOutput, "Pontos Removidos");

            Bitmap output = ColorProcessing.mixColorChannels(ROutput, GOutput, BOutput);
            new exibirImagem(output).Show();
        }

        //preencher buracos na imagem
        private void preencherBuracosButton_click(object sender, EventArgs e)
        {
            var inputResult = iM.getInputImageDialog(true);

            int[,] Ri = inputResult.R,
                   Gi = inputResult.G,
                   Bi = inputResult.B;

            int size = -1;

            int[,] Rb = MorphologicalImageProcessing.fillHoles(Ri, size);
            int[,] Gb = MorphologicalImageProcessing.fillHoles(Gi, size);
            int[,] Bb = MorphologicalImageProcessing.fillHoles(Bi, size);

            iM.saveOutput(Rb, Gb, Bb, "Buracos preenchidos");

            Bitmap output = ColorProcessing.mixColorChannels(Rb, Gb, Bb);

            new exibirImagem(output).Show();
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
                intersection,
                CFactor;

            int separatedColor;
            
            var separated = ImageManagment.separateChannelDialog(Ri, Gi, Bi, out separatedColor, out intersection, out CFactor);

            Ri = separated.R;
            Gi = separated.G;
            Bi = separated.B;

            var convexResults = MorphologicalImageProcessing.getConvexHull(Ri, Gi, Bi, structuringElement, 3);
            
            var joinedChannel = ImageManagment.joinChannelDialog(convexResults.R, convexResults.G, convexResults.B, separatedColor, intersection, CFactor, separated, inputLimiarizedChannels);

            int[,]
                convR = joinedChannel.R,
                convG = joinedChannel.G,
                convB = joinedChannel.B;

            iM.saveOutput(convR, convG, convB, "Fecho Convexo");

            Bitmap convexHullImage = ColorProcessing.mixColorChannels(convR, convG, convB);
            new exibirImagem(convexHullImage, "Fecho Convexo").Show();
        }

        //Skeleton
        private void esqueletoButton_Click(object sender, EventArgs e)
        {
            var inputLimiarizedChannels = iM.getInputImageDialog(true, 100);

            int[,]
                Ri = inputLimiarizedChannels.R,
                Gi = inputLimiarizedChannels.G,
                Bi = inputLimiarizedChannels.B,
                intersection,
                CFactor;

            int separatedColor;
            var separated = ImageManagment.separateChannelDialog(Ri, Gi, Bi, out separatedColor, out intersection, out CFactor);

            Ri = separated.R;
            Gi = separated.G;
            Bi = separated.B;

            var skeletons = MorphologicalImageProcessing.getAllChannelsSkeletons(Ri, Gi, Bi);

            Bitmap skeletonImage = ColorProcessing.mixColorChannels(skeletons.R, skeletons.G, skeletons.B);
            new exibirImagem(skeletonImage, "Skeleton").Show();

            var joinedChannel = ImageManagment.joinChannelDialog(skeletons.R, skeletons.G, skeletons.B, separatedColor, intersection, CFactor, separated, inputLimiarizedChannels, true, 2);
            
            int[,]
                skR = joinedChannel.R,
                skG = joinedChannel.G,
                skB = joinedChannel.B;

            iM.saveOutput(skR, skG, skB, "esqueleto da imagem");

            skeletonImage = ColorProcessing.mixColorChannels(skR, skG, skB);
            new exibirImagem(skeletonImage, "Skeleton").Show();
        }

        //quantidade de objetos vermelhos restantes
        private void contarObjetosVermelhos_Click(object sender, EventArgs e)
        {
            var inputLimiarizedChannels = iM.getInputImageDialog(true, 100);

            int[,]
                Ri = inputLimiarizedChannels.R,
                Gi = inputLimiarizedChannels.G,
                Bi = inputLimiarizedChannels.B;

            int[,] GBIntersection = MorphologicalImageProcessing.getIntersection(Bi, Gi);
            int[,] verticalLines = MorphologicalImageProcessing.getVerticalLinesInChannel(GBIntersection, 15);
            int[,] colapsedVerticalLines = ImageManagment.collapseVerticalLines(verticalLines);

            int objectsCount = ImageManagment.countPixelsWithIntensity(colapsedVerticalLines, 0); //quantidade de objetos veremelhos

            //Exibindo os resultados obtidos
            int[,] colapsedVerticalLinesDilated = MorphologicalImageProcessing.dilateChannel(colapsedVerticalLines, new int[5, 5]);

            colapsedVerticalLinesDilated = MorphologicalImageProcessing.dilateChannel(colapsedVerticalLinesDilated, new int[9, 9]);
            int[,] mixedR = MorphologicalImageProcessing.getChannelsSum(Ri, colapsedVerticalLinesDilated, false),
                   mixedG = MorphologicalImageProcessing.getChannelsSum(Gi, colapsedVerticalLinesDilated, false),
                   mixedB = MorphologicalImageProcessing.getChannelsSum(Bi, colapsedVerticalLinesDilated, false);


            new exibirImagem(ColorProcessing.mixColorChannels(GBIntersection, GBIntersection, GBIntersection), "Intersecção entre os canais verde e azul").Show();
            new exibirImagem(ColorProcessing.mixColorChannels(verticalLines, verticalLines, verticalLines), "bordas esquerdas").Show();
            new exibirImagem(ColorProcessing.mixColorChannels(colapsedVerticalLines, colapsedVerticalLines, colapsedVerticalLines), "bordas esquerdas colapsadas").Show();
            new exibirImagem(ColorProcessing.mixColorChannels(colapsedVerticalLinesDilated, colapsedVerticalLinesDilated, colapsedVerticalLinesDilated), "bordas esquerdas colapsadas (dilatada)").Show();
            new exibirImagem(ColorProcessing.mixColorChannels(mixedR, mixedG, mixedB), "Destacados").Show();
            MessageBox.Show($"{objectsCount} objetos vermelhos na imagem");
        }

    }
}