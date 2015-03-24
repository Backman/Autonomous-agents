using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Autonomous_Agents
{
	public class BaseNode : Priority_Queue.PriorityQueueNode, INode
	{
		public Vector2 Position { get; set; }
	}
}
