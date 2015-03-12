package com.rocketmandevelopment.grid {
	import com.rocketmandevelopment.grid.Cell;
	import com.rocketmandevelopment.math.Vector2D;
	
	[SWF(width=510, height=550, backgroundColor=0, fps=30)]
	public class AStar {
		public static var heuristic:Function = manhattan;
		
		public function AStar() {
		}
		
		public static function aStar(start:Vector2D, end:Vector2D):Array {
			Grid.reset();
			var open:Array = [Grid.cellAt(start.x, start.y)];
			open[0].isOpen = true;
			var closed:Array = [];
			var currentCell:Cell;
			var path:Array;
			
			while(true) {
				if(open.length == 0) {
					break;
				}
				currentCell = getLowestF(open);
				if(currentCell.x == end.x && currentCell.y == end.y) {
					path = [currentCell];
					while(true) {
						path.push(currentCell.parent);
						currentCell = currentCell.parent;
						if(!currentCell.parent) {
							path.reverse();
							break;
						}
					}
					break;
				}
				closed.push(currentCell);
				currentCell.isClosed = true;
				var n:Array = currentCell.neighbors;
				for(var i:int = 0; i < n.length; i++) {
					if(n[i] == null || !n[i].isWalkable) {
						continue;
					}
					if(!n[i].isOpen && !n[i].isClosed) {
						open.push(n[i]);
						n[i].isOpen = true;
						if(isDiagonal(currentCell, n[i])) {
							n[i].g = 1.4;
						} else {
							n[i].g = 1;
						}
						n[i].parent = currentCell;
						n[i].g += n[i].parent.g;
						n[i].h = heuristic(n[i], end);
						n[i].f = n[i].g + n[i].h;
					} else {
						var tg:Number;
						if(isDiagonal(currentCell, n[i])) {
							tg = 1.4;
						} else {
							tg = 1
						}
						tg += currentCell.g;
						if(tg < n[i].g) {
							n[i].g = tg;
							n[i].f = n[i].g + n[i].h;
							n[i].parent = currentCell;
						}
					}
					
				}
			}
			return path;
		}
		
		public static function diagonal(current:Cell, end:Vector2D):Number {
			var xDistance:int = Math.abs(current.x - end.x);
			var yDistance:int = Math.abs(current.y - end.y);
			if(xDistance > yDistance) {
				return yDistance + (xDistance - yDistance);
			} else {
				return xDistance + (yDistance - xDistance);
			}
			return 0;
		}
		
		public static function manhattan(current:Cell, end:Vector2D):Number {
			return Math.abs(current.x - end.x) + Math.abs(current.y + end.y);
		}
		
		private static function getLowestF(list:Array):Cell {
			list.sortOn("f", Array.NUMERIC | Array.DESCENDING);
			return list.pop();
		}
		
		private static function isDiagonal(center:Cell, other:Cell):Boolean {
			if(center.x != other.x && center.y != other.y) {
				return true;
			}
			return false;
		}
	}
}