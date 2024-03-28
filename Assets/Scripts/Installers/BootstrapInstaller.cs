using Input;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private InputPresenter m_InputPresenter;

        public override void InstallBindings()
        {
            Container.Bind<InputPresenter>().FromInstance(m_InputPresenter).AsSingle().NonLazy();
        }
    }
}
