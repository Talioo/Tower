using UnityEngine;
using Zenject;

public class Installer : MonoInstaller
{
    [SerializeField] private GameObject movingBlockPrefab;
    [SerializeField] private GameObject stayingBlock;


    public override void InstallBindings()
    {
        InstallBlock();
        InstallCanvas();
        Container.Bind<CameraController>().FromInstance(Camera.main.GetComponent<CameraController>()).AsSingle().NonLazy();
        Container.Bind<RoundParamsContainer>().AsSingle().NonLazy();
        Container.BindInterfacesTo<GameController>().AsSingle().NonLazy();

        Container.BindFactory<StayingBlock, StayingBlock.Factory>().FromComponentInNewPrefab(stayingBlock);

        Container.Bind<BlockState>().AsSingle().NonLazy();
    }

    void InstallBlock()
    {
        MovingBlock movingBlock = Instantiate(movingBlockPrefab).GetComponent<MovingBlock>();
        Container.Bind<MovingBlock>().FromInstance(movingBlock).AsSingle().NonLazy();

        Container.Bind<BlockStateFactory>().AsSingle().NonLazy();


        Container.BindFactory<BlockWait, BlockWait.Factory>().NonLazy();
        Container.BindFactory<BlockMoving, BlockMoving.Factory>().NonLazy();
        Container.BindFactory<BlockBeforeRound, BlockBeforeRound.Factory>().NonLazy();
        Container.BindFactory<BlockAfterRound, BlockAfterRound.Factory>().NonLazy();
    }

    void InstallCanvas()
    {
        Container.Bind<CanvasManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<UIBeforeGame>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<UIGame>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<UIAfterGame>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}