using Zenject;
using UnityEngine;
using System.Collections;

public class ZenjectInstaller : MonoInstaller
{
    public GameObject enemyPrefab;
    public GameObject bloodSplash;
    public GameObject bloodFlaque;
    public override void InstallBindings()
    {
        Container.Bind<string>().FromInstance("Hello World!");
        Container.Bind<Greeter>().AsSingle().NonLazy();
        Container.Bind<PlayerScript>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Enemy>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ActivateActivePauseHUD>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerSpells>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameObject>().WithId("BloodFlaque").FromInstance(bloodFlaque);
        Container.Bind<GameObject>().WithId("BloodSplash").FromInstance(bloodSplash);
        Container.BindFactory<Enemy, Enemy.Factory>().FromComponentInNewPrefab(enemyPrefab).AsTransient();
        Container.BindFactory<OutlineScript, OutlineScript.Factory>().FromComponentInNewPrefab(bloodFlaque).AsTransient();
    }
}

public class Greeter
{
    string _message;
    [Inject]
    public void Ini(string message)
    {
        _message = message;
        Debug.Log(message);
    }

}