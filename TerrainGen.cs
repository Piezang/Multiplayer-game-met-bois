using Multiplayer_game_met_bois;
using System;

public class TerrainGen
{
	double[] TerrainOutln = new double[882];

	public TerrainGen()
	{
		for (int i = 0; i < 882; i++)
		{
			TerrainOutln[i] = 200 * (2 + (-1 * Math.Sin(i)));
		}
	}

	public Bitmap TerrainImage()
	{
		Bitmap bmp = new Bitmap(883, 497);
		Graphics g = Graphics.FromImage(bmp);
		Pen pen = new Pen(Brushes.Brown);

		for (int i = 0; i < 882; i++) 
		{
			Point pt1 = new Point(i, 497);
			Point pt2 = new Point(i, (int)TerrainOutln[i]);
			g.DrawLine(pen, pt1, pt2);
		}
		return bmp;
	}
}