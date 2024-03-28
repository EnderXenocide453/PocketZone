using Input;
using Loot;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private InputPresenter m_InputPresenter;
        [SerializeField] private LootFabric m_LootFabric;

        public override void InstallBindings()
        {
            Container.Bind<InputPresenter>().FromInstance(m_InputPresenter).AsSingle().NonLazy();
            Container.Bind<LootFabric>().FromInstance(m_LootFabric).AsSingle().NonLazy();
            m_LootFabric.GenerateIDs();
            Container.Bind<Inventory>().FromInstance(new Inventory()).AsSingle().NonLazy();
        }
    }
}
