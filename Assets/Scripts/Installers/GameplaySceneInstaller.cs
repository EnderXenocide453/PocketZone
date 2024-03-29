using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameplaySceneInstaller>().FromInstance(this).AsSingle().NonLazy();
        }

        public T Instantiate<T>(Object prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            return Container.InstantiatePrefabForComponent<T>(prefab, position, rotation, parent);
        }

        public new GameObject Instantiate(Object prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            return Container.InstantiatePrefab(prefab, position, rotation, parent);
        }
    }
}

