using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public Action OnCreateEnemy;
    public Action OnAttack;
    public Action OnPauseActive;
    public Action OnRadialMenu;
    public event Action OnSacrifice;
    public event Action OnExplosion;

    ActivateActivePauseHUD activateActivePauseHUD;
    PlayerSpells playerSpells;
    [Inject]
    public void Construct(ActivateActivePauseHUD _hud,PlayerSpells _playerSpells)
    {
        activateActivePauseHUD = _hud;
        playerSpells = _playerSpells;
    }
    private void Awake()
    {
        if (Instance != null) Destroy(Instance);
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnSacrifice?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnAttack?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && activateActivePauseHUD._active == true && playerSpells.CheckFlaque() == true)
        {
            OnRadialMenu?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnCreateEnemy?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnExplosion?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            OnPauseActive?.Invoke();
        }
    }
}
