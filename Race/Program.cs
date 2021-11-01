using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Race
{
	class Program
	{
		public const int INF = 99999;
		static void Main(string[] args)
		{

			int vertices = 4;
			int[,] matrix = {
				{ 0, 0, -2, 0},
				{ 4, 0, 3, 0 },
				{ 0, 0, 0, 2 },
				{ 0, -1, 0, 0}
			};

			// Initialization
			Graph graph = new Graph(vertices, matrix);
		
			// Function Call
			int[][] distances = graph.johnsons();

			if (distances == null)
			{
				Console.WriteLine(
					"Negative weight cycle detected.");
				return;
			}

			// The code fragment below outputs
			// an formatted distance matrix.
			// Its first row and first
			// column represent vertices
			Console.WriteLine("Distance matrix:");

			Console.Write("   \t");
			for (int i = 0; i < vertices; i++)
				Console.Write($"{i}\t", i);

			for (int i = 0; i < vertices; i++)
			{
				Console.WriteLine();
				Console.Write($"{i}\t", i);
				for (int j = 0; j < vertices; j++)
				{
					if (distances[i][j] == int.MaxValue)
					{

						Console.Write("X\t");
					}

					else
					{
						Console.Write($"{distances[i][j]}\t");


					}


					//	int[,] graphFloyd = {
					//{ 0,   5,  INF, 10 },
					//{ INF, 0,   3, INF },
					//{ INF, INF, 0,   1 },
					//{ INF, INF, INF, 0 }
					//};

					//	FloydWarshall(graphFloyd, 4);


					Console.ReadLine();
					//}



				}
			}
		}
		public static void FloydWarshall(int[,] graph, int verticesCount)
		{
			int[,] distance = new int[verticesCount, verticesCount];

			for (int i = 0; i < verticesCount; ++i)
				for (int j = 0; j < verticesCount; ++j)
					distance[i, j] = graph[i, j];

			for (int k = 0; k < verticesCount; ++k)
			{
				for (int i = 0; i < verticesCount; ++i)
				{
					for (int j = 0; j < verticesCount; ++j)
					{
						if (distance[i, k] + distance[k, j] < distance[i, j])
							distance[i, j] = distance[i, k] + distance[k, j];
					}
				}
			}

			Print(distance, verticesCount);
		}



		private static void Print(int[,] distance, int verticesCount)
		{
			Console.WriteLine("Shortest distances between every pair of vertices:");

			for (int i = 0; i < verticesCount; ++i)
			{
				for (int j = 0; j < verticesCount; ++j)
				{
					if (distance[i, j] == INF)
						Console.Write("INF".PadLeft(7));
					else
						Console.Write(distance[i, j].ToString().PadLeft(7));
				}

				Console.WriteLine();
			}
		}
	}
}
