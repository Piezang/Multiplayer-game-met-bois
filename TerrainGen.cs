using Multiplayer_game_met_bois;
using System;

public class TerrainGen
{
	double[] TerrainOutln = new double[882];
	int[] t = new int[882]; 
	public TerrainGen()
	{
		for (int i = 0; i < 882; i += 1)
		{
			TerrainOutln[i] = 250 + (100 * Math.Sin(i*Math.PI / 360) + (100 * Math.Cos(Math.Pow(i * Math.PI/ 180, 2) / 20)));       
		}
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