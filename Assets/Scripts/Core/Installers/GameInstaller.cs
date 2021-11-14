using Core.Managers.GridManager.Impls;
using Zenject;

namespace Core.Installers
{
	public class GameInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<GridManager>().AsSingle();
		}
	}
}
