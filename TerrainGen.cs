using Multiplayer_game_met_bois;
using System;

public class TerrainGen
{
	Point[] TerrainOutln = new Point[832];

	public TerrainGen()
	{
		for (int i = 0; i < 833; i++)
		{
			Point Coords = new Point(i, (int)Math.Sin(60));
			TerrainOutln[i] = Coords;
		}
	}
}
