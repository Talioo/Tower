using Zenject;

public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
{
    public BlockMoving.Settings MoveSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(MoveSettings);
    }
}