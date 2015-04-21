#region Using Statements
using Microsoft.Xna.Framework;
#endregion

namespace BachelorsProject
{
	public abstract class BaseNode : Priority_Queue.PriorityQueueNode
	{
		public int GridX;
		public int GridY;
		public Vector2 Position;
		public bool Walkable = true;
	}


	public class AStarNode : BaseNode
	{
		public AStarNode Parent { get; set; }
		public float G { get; set; }
		public float H { get; set; }
		public float F { get { return G + H; } }

		public AStarNode()
		{

		}
	}

	public class FlowFieldNode : BaseNode
	{
		public Vector2 Force;
		public int Cost = -1;

		public FlowFieldNode()
		{

		}
	}
}
