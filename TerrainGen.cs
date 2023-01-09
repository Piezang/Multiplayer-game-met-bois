using Multiplayer_game_met_bois;
using System;

public class TerrainGen
{
	double[] TerrainOutln = new double[832]; 

	public TerrainGen()
	{
		for (int i = 0; i < 833; i++)
		{
			TerrainOutln[i] = 200 * (2 + (-1 * Math.Sin(i)));
		}
	}
}
