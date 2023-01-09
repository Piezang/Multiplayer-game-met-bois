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
			TerrainOutln[i] = (Math.Sin(i)) + 200;//50 * (2 + (-1 * Math.Sin(i))) + 150;
			t[i] = (int)(Math.Sin(i / 40) * -40) + 100;
        }
    } 

	public Bitmap TerrainImage()
	{
		Bitmap bmp = new Bitmap(883, 497);
		Graphics g = Graphics.FromImage(bmp);
		Pen pen = new Pen(Brushes.Brown);

		for (int i = 0; i < 881; i++) 
		{
			Point pt1 = new Point(i, 497);
			Point pt2 = new Point(i, Convert.ToInt32(TerrainOutln[i]));
			Point pt3 = new Point(i+1, Convert.ToInt32(TerrainOutln[i+1]));
			g.DrawLine(pen, pt2, pt3);
			//bmp.GetPixel(i, Convert.ToInt32(TerrainOutln[i]));
			//bmp.SetPixel(i, Convert.ToInt32(TerrainOutln[i]), Color.AliceBlue);
		}
		return bmp;
	}
}