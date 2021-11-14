using Databases.Grid;
using Databases.Grid.Impls;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
	[CreateAssetMenu(fileName = "GameDatabasesInstaller", menuName = "Settings/Installers/GameDatabasesInstaller")]
	public class ScriptableGameDatabasesInstaller : ScriptableObjectInstaller
	{
		[SerializeField] private GridDatabase gridDatabase;
		
		public override void InstallBindings()
		{
			Container.BindFromInstanceSingle<IGridDatabase>(gridDatabase);
		}
	}

	public static class ContainerExtensions
	{
		public static void BindFromInstanceSingle<T>(this DiContainer container, T instance)
			=> container.Bind<T>().FromInstance(instance).AsSingle();
	}
}
