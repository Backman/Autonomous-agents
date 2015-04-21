#region Using Statements

using Bitmap = System.Drawing.Bitmap;
#endregion

namespace BachelorsProject
{
	public class AStarGrid : BaseGrid<AStarNode>
	{
		public AStarGrid() 
			: base()
		{

		}

		public override void FromBitmap(Bitmap bitmap)
		{
			int w = bitmap.Width / _gridWidth;
			int h = bitmap.Height / _gridHeight;
			int black = System.Drawing.Color.Black.ToArgb();

			for (int y = 0; y < _gridHeight; ++y)
			{
				for (int x = 0; x < _gridWidth; ++x)
				{
					int pX = x * w;
					int pY = y * h;
					System.Drawing.Color pixel = bitmap.GetPixel(pX, pY);

					var node = _grid[x, y];

					if (!pixel.ToArgb().Equals(black))
					{
						node.Walkable = true;
					}
					else
					{
						node.Walkable = false;
					}
				}
			}
		}
	}
}
