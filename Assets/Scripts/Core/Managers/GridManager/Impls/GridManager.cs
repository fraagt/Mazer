using Databases.Grid;
using UnityEngine;
using Zenject;

namespace Core.Managers.GridManager.Impls
{
	public class GridManager : IGridManager, IInitializable
	{
		private readonly IGridDatabase _gridDatabase;

		public GridManager(
			IGridDatabase gridDatabase
		)
		{
			_gridDatabase = gridDatabase;
		}

		public void Initialize()
		{
			for (int i = 0; i < _gridDatabase.Height; i++)
			{
				for (int j = 0; j < _gridDatabase.Width; j++)
				{
					var cell = Object.Instantiate(_gridDatabase.Cell);
					var size = cell.MeshFilter.mesh.bounds.size;
					var positionZ = size.z * i;
					var positionX = size.x * j;

					cell.Root.position = new Vector3(positionX, 0, positionZ);
				}
			}
		}
	}
}
