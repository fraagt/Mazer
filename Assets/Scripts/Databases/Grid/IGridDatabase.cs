using Core.Models;

namespace Databases.Grid
{
	public interface IGridDatabase
	{
		Cell Cell { get; }

		int Width { get; }
		int Height { get; }
	}
}
