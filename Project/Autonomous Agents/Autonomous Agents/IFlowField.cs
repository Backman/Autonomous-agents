﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Autonomous_Agents
{
	public interface IFlowField
	{
		Vector2 Sample(Vector2 position);
	}
}
