using GridSystem;
using VContainer;
using VContainer.Unity;

public class GameManager : IInitializable
{
    private GridManager _gridManager;

    [Inject]
    public GameManager(GridManager gridManager)
    {
        _gridManager = gridManager;
    }
    
    public void Initialize()
    {
        
    }
}