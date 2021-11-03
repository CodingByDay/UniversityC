using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Race
{
	class Program
	{
		public const int INF = int.MaxValue;
		static void Main(string[] args)
		{

			int[,] adjacency_matrix;
			int number_of_vertices;
			var scan = "Inputs";
			try
			{
				Console.WriteLine("Enter the number of vertices");
				//number_of_vertices = (int)Convert.ToInt64(Console.ReadLine());
				number_of_vertices = 4;
				adjacency_matrix = new int[number_of_vertices + 1, number_of_vertices + 1];
				//Console.WriteLine("Enter the Weighted Matrix for the graph");
				//for (var i = 1; i <= number_of_vertices; i++)
				//{
				//	for (var j = 1; j <= number_of_vertices; j++)
				//	{
				//		adjacency_matrix[i, j] = (int)Convert.ToInt64(Console.ReadLine());
				//		if (i == j)
				//		{
				//			adjacency_matrix[i, j] = 0;
				//			continue;
				//		}
				//		if (adjacency_matrix[i, j] == 0)
				//		{
				//			adjacency_matrix[i, j] = JohnsonsAlgorithm.MAX_VALUE;
				//		}
				//	}
				//}
			int[,] adjacency_matrixs =  {
					{ 0,   5,  INF, 10 },
            { INF, 0,   3, INF },
            { INF, INF, 0,   1 },
            { INF, INF, INF, 0 }
				};
				var johnsonsAlgorithm = new JohnsonsAlgorithm(number_of_vertices);
				johnsonsAlgorithm.johnsonsAlgorithms(adjacency_matrixs);
			}
			catch (Exception e)
			{

			}


            //int[,] graphFloyd = {
            //{ 0,   5,  INF, 10 },
            //{ INF, 0,   3, INF },
            //{ INF, INF, 0,   1 },
            //{ INF, INF, INF, 0 }
            //};

            //	FloydWarshall(graphFloyd, 4);


            Console.ReadLine();
					//}



				
			

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
