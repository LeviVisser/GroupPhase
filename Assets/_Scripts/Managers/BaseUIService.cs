using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Base class of every major UI screen
/// Call Open(); to open and Close(); to close
/// </summary>
public abstract class BaseUIService : BaseService
{
    public UnityEvent OnStartOpeningScreen, OnFinishedOpeningScreen, OnStartClosingScreen, OnFinishedClosingScreen;
    protected Coroutine NavigationRoutine;

    protected CanvasGroup CanvasGroup;

    protected GraphicRaycaster GraphicRaycaster;

    protected Canvas Canvas;
    protected float OpenAnimTime = 0, CloseAnimTime = 0;
    public bool IsOpen { get; protected set; }

    [SerializeField] private bool overrideSorting;

    private bool screenSetup = false;

    protected override void OnInitialize()
    {
        if (screenSetup) return;
        SetupScreen();
    }

    public void SetupScreen()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }

        GraphicRaycaster = GetComponent<GraphicRaycaster>();
        if (!GraphicRaycaster)
        {
            GraphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
        }

        CanvasGroup = GetComponent<CanvasGroup>();
        if (!CanvasGroup)
        {
            CanvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        Canvas = GetComponent<Canvas>();
        Canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord2;
        Canvas.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord3;
        Canvas.overrideSorting = overrideSorting;
        CanvasGroup.alpha = 0;

        SetCanvasGroupActive(false);
        screenSetup = true;
    }

    /// <summary>
    /// Sets the canvas group to either 0 or 1 and sets it to be interactible or not
    /// </summary>
    /// <param name="active"></param>
    protected void SetCanvasGroupActive(bool active)
    {
        if (!CanvasGroup) return;
        Canvas.ForceUpdateCanvases();

        CanvasGroup.interactable = active;
        GraphicRaycaster.enabled = active;
        CanvasGroup.alpha = active ? 1 : 0;
    } 

    #region Public Methods

    /// <summary>
    /// Open the screen.
    /// Calls OnStartOpeningScreen and OnFinishedOpeningScreen at the beginning and end respectively of the opening sequence.
    /// </summary>
    public void Open(float delay = 0)
    {
        if (IsOpen) return;
        if (NavigationRoutine != null)
        {
            StopCoroutine(NavigationRoutine);
        }

        NavigationRoutine = StartCoroutine(OpenRoutine(delay));
    }

    /// <summary>
    /// Close the screen.
    /// Calls OnStartClosingScreen and OnFinishedOpeningScreen at the beginning and end respectively of the opening sequence.
    /// </summary>
    public void Close(float delay = 0)
    {
        if (!IsOpen) return;
        if (NavigationRoutine != null)
        {
            StopCoroutine(NavigationRoutine);
        }

        NavigationRoutine = StartCoroutine(CloseRoutine(delay));
    }

    #endregion

    #region Private Methods

    private IEnumerator OpenRoutine(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        // Notify start opening
        OnStartOpeningScreen.Invoke();

        // Wait until behaviour is completed

        yield return StartCoroutine(HandleScreenOpening());

        // Notify finished opening
        OnFinishedOpeningScreen.Invoke();
        IsOpen = true;
    }

    private IEnumerator CloseRoutine(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        // Notify start closing
        OnStartClosingScreen.Invoke();

        // Wait until behaviour is completed
        yield return StartCoroutine(HandleScreenClosing());

        // Notify finished closing
        OnFinishedClosingScreen.Invoke();
        IsOpen = false;
    }

    #endregion

    /// <summary>
    /// Implement this with actual opening logic 
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerator HandleScreenOpening();

    /// <summary>
    /// Implement this with actual closing logic
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerator HandleScreenClosing();
}