using Multiplayer_game_met_bois;
using System;
using System.Security.Cryptography;

public class TerrainGen
{
	double[] TerrainOutln = new double[882];
	double[] TerrainOutln1 = new double[882];
	int[] t = new int[882]; 
	public TerrainGen()
	{
		for (int i = 0; i < 882; i ++)
		{
			TerrainOutln[i] = 250 + (100 * Math.Sin(i*Math.PI / 360) + (100 * Math.Cos(Math.Pow(i * Math.PI/ 180, 2) / 20)));
			//Random random = new Random();
			//TerrainOutln[i] = random.Next(100, 400);
		}

		/*for (int i = 0; i < 882; i++)
		{
			double iNew = new double();
			double iSum = 0;
			switch (i) 
			{
				case < 6:
						for (int p = 0; p > i + 1; p++) {iSum = iSum +  }
					
				default : iNew = (TerrainOutln[i - 2] + TerrainOutln[i - 1] + TerrainOutln[i] + TerrainOutln[i + 1] + TerrainOutln[i + 2]) / 5;
					break;
            }
			TerrainOutln1[i] = iNew;
		}*/
	}

	public Bitmap TerrainImage()
	{
		Bitmap bmp = new Bitmap(883, 497);
		Graphics g = Graphics.FromImage(bmp);
		Pen pen = new Pen(Brushes.SaddleBrown);

		for (int i = 0; i < 881; i++) 
		{
			Point pt1 = new Point(i, 497);
			Point pt2 = new Point(i, Convert.ToInt32(TerrainOutln[i]));
			g.DrawLine(pen, pt1, pt2);
		}
		return bmp;
	}
}