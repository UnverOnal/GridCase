using GridSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class SceneScope : LifetimeScope
    {
        [SerializeField] private GridData gridData;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterEntryPoint<GameManager>();

            builder.RegisterInstance(gridData);

            builder.Register<GridManager>(Lifetime.Singleton);
        }
    }
}
