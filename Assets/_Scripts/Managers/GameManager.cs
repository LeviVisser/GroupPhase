using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Bootstrapper")] [SerializeField]
    private Bootstrapper _bootstrapper;

    private void Awake()
    {
        _bootstrapper.Initialize();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (!_bootstrapper.IsInitialized) return;
    }

    private void FixedUpdate()
    {
        if (!_bootstrapper.IsInitialized) return;
    }

    private void LateUpdate()
    {
        if (!_bootstrapper.IsInitialized) return;
    }
}