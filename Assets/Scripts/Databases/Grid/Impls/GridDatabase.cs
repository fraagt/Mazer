using Core.Models;
using UnityEngine;

namespace Databases.Grid.Impls
{
	[CreateAssetMenu(fileName = "GridDatabase", menuName = "Databases/Game/GridDatabase")]
	public class GridDatabase : ScriptableObject, IGridDatabase
	{
		[SerializeField] private Cell cell;
		[SerializeField] private int width;
		[SerializeField] private int height;

		public Cell Cell => cell;
		
		public int Width => width;
		
		public int Height => height;
	}
}
