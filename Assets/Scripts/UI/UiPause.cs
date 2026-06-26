using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiPause : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private GameManager gameManager;

    [Header("Panels")]
    [SerializeField] private CanvasGroup panelMainPause;
    [SerializeField] private CanvasGroup panelSettings;

    [Header("Buttons")]
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnSettingsBack;

    private void Awake()
    {
        AddButtonsListeners();
        SetStateCanvasGroup(panelMainPause, false);
        SetStateCanvasGroup(panelSettings, false);
    }

    private void OnEnable()
    {
        if (gameManager != null)
        {
            gameManager.OnStateChanged += HandleGameStateChanged;
        }
    }

    private void OnDisable()
    {
        if (gameManager != null)
        {
            gameManager.OnStateChanged -= HandleGameStateChanged;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameManager.CurrentGameState == GameManager.GameState.Playing)
                gameManager.Pause();
            else if (gameManager.CurrentGameState == GameManager.GameState.Paused)
                gameManager.Resume();
        }
    }

    private void HandleGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Paused)
        {
            SetStateCanvasGroup(panelMainPause, true);
            Cursor.lockState = CursorLockMode.None;
        }
        else if (state == GameManager.GameState.Playing)
        {
            SetStateCanvasGroup(panelMainPause, false);
            SetStateCanvasGroup(panelSettings, false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void AddButtonsListeners()
    {
        btnContinue.onClick.AddListener(()=> gameManager.Resume());
        btnSettings.onClick.AddListener(OnOptionsClicked);
        btnExit.onClick.AddListener(OnExitClicked);
        btnSettingsBack.onClick.AddListener(OnSettingsBackClicked);
    }

    private void OnOptionsClicked()
    {
        SetStateCanvasGroup(panelSettings, true);
    }

    private void OnExitClicked()
    {
        gameManager.Resume(); // aseguramos que el estado vuelva a Playing
        CustomSceneManager.Instance.GoToMainMenuImmediate();
    }

    private void OnSettingsBackClicked()
    {
        SetStateCanvasGroup(panelSettings, false);
    }

    private void SetStateCanvasGroup(CanvasGroup canvasGroup, bool state)
    {
        // Activa o desactiva visibilidad e interacción de un panel.
        canvasGroup.alpha = state ? 1 : 0;
        canvasGroup.interactable = state;
        canvasGroup.blocksRaycasts = state;
    }

    private void OnDestroy()
    {
        RemoveButtonsListeners();
    }

    private void RemoveButtonsListeners()
    {
        btnContinue.onClick.RemoveAllListeners();
        btnSettings.onClick.RemoveAllListeners();
        btnExit.onClick.RemoveAllListeners();
        btnSettingsBack.onClick.RemoveAllListeners();
    }
}
