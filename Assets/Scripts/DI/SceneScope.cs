using GridSystem;
using Ui;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class SceneScope : LifetimeScope
    {
        [SerializeField] private GridData gridData;
        [SerializeField] private GridUiResources gridUiResources;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterEntryPoint<GameManager>();

            builder.RegisterInstance(gridData);
            builder.RegisterComponent(gridUiResources);

            builder.Register<GridManager>(Lifetime.Singleton);
            builder.Register<InputManager>(Lifetime.Singleton);
        }
    }
}
