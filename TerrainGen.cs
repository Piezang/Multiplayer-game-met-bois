using Multiplayer_game_met_bois;
using System;
using System.Security.Cryptography;

public class TerrainGen
{
	double[] TerrainOutln = new double[883];
	double[] TerrainOutln1 = new double[883];
	int width;
	public TerrainGen(int Width)
	{
		
		for (int i = 0; i <= 882; i ++)
		{
			//TerrainOutln[i] = 250 + (100 * Math.Sin(i*Math.PI / 360) + (100 * Math.Cos(Math.Pow(i * Math.PI/ 180, 2) / 20)));
			Random random = new Random();
			Random outliers = new Random();
			int iOutliers = outliers.Next(0, 149);
			switch (iOutliers)
			{
				case 0:
                    switch (i)
                    {
                        case <= 39: for (int p = 0; p <= i + 40; p++) 
							{ TerrainOutln[p] = TerrainOutln[p] = TerrainOutln[p] - random.Next(0, 10) ; };break;
                        case >= 843: for (int p = i - 20; p <= 882; p++)
							{ TerrainOutln[p] = TerrainOutln[p] = TerrainOutln[p] - random.Next(0, 10) ; } break;
                        default: for (int p = i - 40; p <= i + 40; p++) 
							{ TerrainOutln[p] = TerrainOutln[p] = TerrainOutln[p] - random.Next(0, 10) ; } break;
                    }
                    break;
				default: TerrainOutln[i] = TerrainOutln[i] = TerrainOutln[i] +  random.Next(250, 450); break;
            }
		}
		for (int k = 0; k <= 20; k++)
		{
			for (int i = 0; i <= 882; i++)
			{
				double iNew = new double();
				double iSum = 0;
				switch (i)
				{
					case <= 4: for (int p = 0; p <= i + 5; p++) { iSum = iSum + TerrainOutln[p]; } iNew = iSum / (i + 5); break;
					case >= 878: for (int p = i - 5; p <= 882; p++) { iSum = iSum + TerrainOutln[p]; } iNew = iSum / (882 - i + 5); break;
					default: for (int p = i - 5; p <= i + 5; p++) { iSum = iSum + TerrainOutln[p]; } iNew = iSum / 11; break;
				}
				TerrainOutln1[i] = (float)iNew;
				if (i == 882) { TerrainOutln = TerrainOutln1; }
			}
		}
	}

	public Bitmap TerrainImage(Bitmap bitmap)
	{
		//Bitmap bmp = new Bitmap(883, 497);
		Graphics g = Graphics.FromImage(bitmap);
		Pen pen = new Pen(Brushes.SaddleBrown);

		for (int i = 0; i <= 882; i++) 
		{
			Point pt1 = new Point(i, 497);
			Point pt2 = new Point(i, Convert.ToInt32(TerrainOutln[i]));
			g.DrawLine(pen, pt1, pt2);
		}
		return bitmap;
	}
}