// Include namespace system
using System;
using System.IO;
public class JohnsonsAlgorithm
{
	private int SOURCE_NODE;
	private int numberOfNodes;
	private int[,] augmentedMatrix;
	private int[] potential;
	private BellmanFord bellmanFord;
	private DijkstraShortesPath dijsktraShortesPath;
	private int[,] allPairShortestPath;
	public
	const int MAX_VALUE = 999;
	public JohnsonsAlgorithm(int numberOfNodes)
	{
		this.numberOfNodes = numberOfNodes;
		this.augmentedMatrix = new int[numberOfNodes + 2, numberOfNodes + 2];
		this.SOURCE_NODE = numberOfNodes + 1;
		this.potential = new int[numberOfNodes + 2];
		this.bellmanFord = new BellmanFord(numberOfNodes + 1);
		this.dijsktraShortesPath = new DijkstraShortesPath(numberOfNodes);
		this.allPairShortestPath = new int[numberOfNodes + 1, numberOfNodes + 1];
	}
	public void johnsonsAlgorithms(int[,] adjacencyMatrix)
	{
		this.computeAugmentedGraph(adjacencyMatrix);
		this.bellmanFord.BellmanFordEvaluation(this.SOURCE_NODE, this.augmentedMatrix);
		this.potential = this.bellmanFord.getDistances();
		int[,] reweightedGraph = this.reweightGraph(adjacencyMatrix);
		for (var i = 1; i <= this.numberOfNodes; i++)
		{
			for (var j = 1; j <= this.numberOfNodes; j++)
			{
				Console.Write(reweightedGraph[i, j].ToString() + "\t");
			}
			Console.WriteLine();
		}
		for (var source = 1; source <= this.numberOfNodes; source++)
		{
			this.dijsktraShortesPath.dijkstraShortestPath(source, reweightedGraph);
			int[] result = this.dijsktraShortesPath.getDistances();
			for (var destination = 1; destination <= this.numberOfNodes; destination++)
			{
				this.allPairShortestPath[source, destination] = result[destination] + this.potential[destination] - this.potential[source];
			}
		}
		Console.WriteLine();
		for (var i = 1; i <= this.numberOfNodes; i++)
		{
			Console.Write("\t" + i.ToString());
		}
		Console.WriteLine();
		for (var source = 1; source <= this.numberOfNodes; source++)
		{
			Console.Write(source.ToString() + "\t");
			for (var destination = 1; destination <= this.numberOfNodes; destination++)
			{
				Console.Write(this.allPairShortestPath[source, destination].ToString() + "\t");
			}
			Console.WriteLine();
		}
	}
	private void computeAugmentedGraph(int[,] adjacencyMatrix)
	{
		for (var source = 1; source <= this.numberOfNodes; source++)
		{
			for (var destination = 1; destination <= this.numberOfNodes; destination++)
			{
				this.augmentedMatrix[source, destination] = adjacencyMatrix[source, destination];
			}
		}
		for (var destination = 1; destination <= this.numberOfNodes; destination++)
		{
			this.augmentedMatrix[this.SOURCE_NODE, destination] = 0;
		}
	}
	private int[,] reweightGraph(int[,] adjacencyMatrix)
	{
		int[,] result = new int[this.numberOfNodes + 1, this.numberOfNodes + 1];
		for (var source = 1; source <= this.numberOfNodes; source++)
		{
			for (var destination = 1; destination <= this.numberOfNodes; destination++)
			{
				result[source, destination] = adjacencyMatrix[source, destination] + this.potential[source] - this.potential[destination];
			}
		}
		return result;
	}

public class BellmanFord
{
	private int[] distances;
	private int numberofvertices;
	public
	const int MAX_VALUE = 999;
	public BellmanFord(int numberofvertices)
	{
		this.numberofvertices = numberofvertices;
		this.distances = new int[numberofvertices + 1];
	}
	public void BellmanFordEvaluation(int source, int[,] adjacencymatrix)
	{
		for (var node = 1; node <= this.numberofvertices; node++)
		{
			this.distances[node] = BellmanFord.MAX_VALUE;
		}
		this.distances[source] = 0;
		for (var node = 1; node <= this.numberofvertices - 1; node++)
		{
			for (var sourcenode = 1; sourcenode <= this.numberofvertices; sourcenode++)
			{
				for (var destinationnode = 1; destinationnode <= this.numberofvertices; destinationnode++)
				{
					if (adjacencymatrix[sourcenode, destinationnode] != BellmanFord.MAX_VALUE)
					{
						if (this.distances[destinationnode] > this.distances[sourcenode] + adjacencymatrix[sourcenode, destinationnode])
						{
							this.distances[destinationnode] = this.distances[sourcenode] + adjacencymatrix[sourcenode, destinationnode];
						}
					}
				}
			}
		}
		for (var sourcenode = 1; sourcenode <= this.numberofvertices; sourcenode++)
		{
			for (var destinationnode = 1; destinationnode <= this.numberofvertices; destinationnode++)
			{
				if (adjacencymatrix[sourcenode, destinationnode] != BellmanFord.MAX_VALUE)
				{
					if (this.distances[destinationnode] > this.distances[sourcenode] + adjacencymatrix[sourcenode, destinationnode])
					{
						Console.WriteLine("The Graph contains negative egde cycle");
					}
				}
			}
		}
	}
	public int[] getDistances()
	{
		return this.distances;
	}
}
	public class DijkstraShortesPath
	{
		private bool[] settled;
		private bool[] unsettled;
		private int[] distances;
		private int[,] adjacencymatrix;
		private int numberofvertices;
		public
		const int MAX_VALUE = 999;
		public DijkstraShortesPath(int numberofvertices)
		{
			this.numberofvertices = numberofvertices;
		}
		public void dijkstraShortestPath(int source, int[,] adjacencymatrix)
		{
			this.settled = new bool[this.numberofvertices + 1];
			this.unsettled = new bool[this.numberofvertices + 1];
			this.distances = new int[this.numberofvertices + 1];
			this.adjacencymatrix = new int[this.numberofvertices + 1, this.numberofvertices + 1];
			int evaluationnode;
			for (var vertex = 1; vertex <= this.numberofvertices; vertex++)
			{
				this.distances[vertex] = DijkstraShortesPath.MAX_VALUE;
			}
			for (var sourcevertex = 1; sourcevertex <= this.numberofvertices; sourcevertex++)
			{
				for (var destinationvertex = 1; destinationvertex <= this.numberofvertices; destinationvertex++)
				{
					this.adjacencymatrix[sourcevertex, destinationvertex] = adjacencymatrix[sourcevertex, destinationvertex];
				}
			}
			this.unsettled[source] = true;
			this.distances[source] = 0;
			while (this.getUnsettledCount(this.unsettled) != 0)
			{
				evaluationnode = this.getNodeWithMinimumDistanceFromUnsettled(this.unsettled);
				this.unsettled[evaluationnode] = false;
				this.settled[evaluationnode] = true;
				this.evaluateNeighbours(evaluationnode);
			}
		}
		public int getUnsettledCount(bool[] unsettled)
		{
			var count = 0;
			for (var vertex = 1; vertex <= this.numberofvertices; vertex++)
			{
				if (unsettled[vertex] == true)
				{
					count++;
				}
			}
			return count;
		}
		public int getNodeWithMinimumDistanceFromUnsettled(bool[] unsettled)
		{
			var min = DijkstraShortesPath.MAX_VALUE;
			var node = 0;
			for (var vertex = 1; vertex <= this.numberofvertices; vertex++)
			{
				if (unsettled[vertex] == true && this.distances[vertex] < min)
				{
					node = vertex;
					min = this.distances[vertex];
				}
			}
			return node;
		}
		public void evaluateNeighbours(int evaluationNode)
		{
			var edgeDistance = -1;
			var newDistance = -1;
			for (var destinationNode = 1; destinationNode <= this.numberofvertices; destinationNode++)
			{
				if (this.settled[destinationNode] == false)
				{
					if (this.adjacencymatrix[evaluationNode, destinationNode] != DijkstraShortesPath.MAX_VALUE)
					{
						edgeDistance = this.adjacencymatrix[evaluationNode, destinationNode];
						newDistance = this.distances[evaluationNode] + edgeDistance;
						if (newDistance < this.distances[destinationNode])
						{
							this.distances[destinationNode] = newDistance;
						}
						this.unsettled[destinationNode] = true;
					}
				}
			}
		}
		public int[] getDistances()
		{
			return distances;
		}
	}
}