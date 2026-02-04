using UnityEngine;

public class ActivateActivePauseHUD : MonoBehaviour
{
    [SerializeField] private Transform[] _objectsToActivate;
    private bool _active;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < _objectsToActivate.Length; i++)
        {
            _objectsToActivate[i].gameObject.SetActive(false);
        }
        _active = false;
        InputManager.Instance.OnPauseActive += PauseActiveActivation;
    }
    void PauseActiveActivation()
    {
        if (_active == false)
        {
            ToggleActive(true, 0.3f);
        }
        else
        {
            ToggleActive(false, 1.0f);
        }
    }

    void ToggleActive(bool trueOrFalse, float timeScale)
    {
        for (int i = 0; i < _objectsToActivate.Length; i++)
        {
            _objectsToActivate[i].gameObject.SetActive(trueOrFalse);
        }
        _active = trueOrFalse;
        Time.timeScale= timeScale;
    }
}
