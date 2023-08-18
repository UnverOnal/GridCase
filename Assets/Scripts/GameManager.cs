using GridSystem;
using VContainer;
using VContainer.Unity;

public class GameManager : IInitializable, ITickable
{
    private GridManager _gridManager;
    private readonly InputManager _inputManager;

    [Inject]
    public GameManager(GridManager gridManager, InputManager inputManager)
    {
        _gridManager = gridManager;
        _inputManager = inputManager;
    }
    
    public void Initialize()
    {
        
    }

    public void Tick()
    {
        _inputManager.Update();
    }
}