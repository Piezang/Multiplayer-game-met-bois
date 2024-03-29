﻿using Multiplayer_game_met_bois;
using System;
using System.Security.Cryptography;

public class TerrainGen
{
	double[] TerrainOutln = new double[4000];
	double[] TerrainOutln1 = new double [4000];
	int width;
	public TerrainGen(int Width)
	{
		width = Width;
		for (int i = 0; i <= width; i ++)   //882
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
						case <= 699: for (int p = 0; p <= i + 500; p++)
                            { int t = random.Next(25, 100);  if (TerrainOutln[p] - t > 25) { TerrainOutln[p] = TerrainOutln[p] - t; } }; break;   
                        case >= 3500: for (int p = i - 500; p <= 3999; p++)
							{ int t = random.Next(25, 100); if (TerrainOutln[p] - t > 25) { TerrainOutln[p] = TerrainOutln[p] - t; } } break;
                        default: for (int p = i - 500; p <= i + 500; p++) 
							{ int t = random.Next(25, 100); if (TerrainOutln[p] - t > 25) { TerrainOutln[p] = TerrainOutln[p] - t; } } break;
                    }
                    break;
				default: TerrainOutln[i] = TerrainOutln[i] = TerrainOutln[i] +  random.Next(450, 800); break;
            }
		}

		for (int i = 0; i <= 400; i ++) { TerrainOutln[i] = 600; }
		for (int i = 3599; i <= 3999; i++) { TerrainOutln[i] = 600; }

		for (int k = 0; k <= 30; k++)
		{
			for (int i = 0; i <= 3999; i++)
			{
				double iNew = new double();
				double iSum = 0;
				switch (i)
				{
					case <= 4: for (int p = 0; p <= i + 5; p++) { iSum = iSum + TerrainOutln[p]; } iNew = iSum / (i + 6); break;
					case >= 3995: for (int p = i - 5; p <= 3999; p++) { iSum = iSum + TerrainOutln[p]; } iNew = iSum / (3999 - i + 6); break;
					default: for (int p = i - 5; p <= i + 5; p++) { iSum = iSum + TerrainOutln[p]; } iNew = iSum / 11; break;
				}
				TerrainOutln1[i] = (float)iNew;
				if (i == 3999) { TerrainOutln = TerrainOutln1; }
			}
		}
	}
	public Bitmap terrain = null!;
	public static int[] ServerTerrain = new int[4000];
	private Rectangle srcRegion = new Rectangle(0, 0, 4000,800);
	private Rectangle destRegion = new Rectangle(0, 0, 4000, 800);
	int count = 0;
    public Bitmap TerrainImage(Bitmap bitmap)
	{
        if (terrain != null)
		{  
            bitmap = CopyRegionIntoImage(terrain,srcRegion, ref bitmap,destRegion);
			return bitmap; //new Terrain(bitmap)
		}  //bitmap.GetPixel(400, 400).ToString() == "Color [A=0, R=0, G=0, B=0]" &&
		//FMessageBox.Show("Null");
        //Image basepng = Image.FromFile("base.png");
        Point p = new Point(20, 250);
		//Bitmap bmp = new Bitmap(883, 497);
		Graphics g = Graphics.FromImage(bitmap);
		Pen pensky = new Pen(Brushes.Pink);
		Pen penDirt = new Pen(Brushes.SaddleBrown);
		Pen penGrass = new Pen(Brushes.DarkGreen);
		
		for (int i = 0; i <= 3999; i++) 
		{
			Point pt1 = new Point(i, 800);
			Point pt3 = new Point(i, Convert.ToInt32(TerrainOutln[i]) - 20);
			Point pt2 = new Point(i, Convert.ToInt32(TerrainOutln[i]));
			ServerTerrain[i] = Convert.ToInt32(TerrainOutln[i] - 20);
            g.DrawLine(penDirt, pt1, pt2);
			g.DrawLine(penGrass, pt2, pt3);
			g.DrawLine(pensky, pt3, new Point(i, 0));
		}
		terrain = new Bitmap(bitmap);
        return bitmap;
	}
    Bitmap CopyRegionIntoImage(Bitmap srcBitmap, Rectangle srcRegion, ref Bitmap destBitmap, Rectangle destRegion)
    {
        using (Graphics grD = Graphics.FromImage(destBitmap))
        {
            grD.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
			return destBitmap;
        }
    }
}