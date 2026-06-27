using UnityEngine;
using UnityEngine.UI;
using CanvasGroup = UnityEngine.CanvasGroup;

public class UIResultScreen : MonoBehaviour
{
    [Header("Game Over Panel:")]
    [SerializeField] private CanvasGroup gameoverPanel;
    [SerializeField] private Button gameOverPlayAgainButton;
    [SerializeField] private Button gameOverMainMenuButton;

    [Header("Victory Panel:")]
    [SerializeField] private CanvasGroup victoryPanel;
    [SerializeField] private Button victoryPlayAgainButton;
    [SerializeField] private Button victoryMainMenuButton;

    [Header("Manager:")]
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        SetCanvasGroup(gameoverPanel, false);
        SetCanvasGroup(victoryPanel, false);
        AddButtonsListeners();
    }

    private void OnEnable()
    {
        if (gameManager == null) return;
        gameManager.OnGameOver += ShowGameOverScreen;
        gameManager.OnVictory += ShowVictoryScreen;
    }

    private void OnDisable()
    {
        if (gameManager == null) return;
        gameManager.OnGameOver -= ShowGameOverScreen;
        gameManager.OnVictory -= ShowVictoryScreen;
    }

    private void OnDestroy()
    {
        RemoveButtonsListeners();
    }

    private void AddButtonsListeners()
    {
        gameOverPlayAgainButton.onClick.AddListener(OnPlayAgainClicked);
        gameOverMainMenuButton.onClick.AddListener(OnExitGameClicked);
        victoryPlayAgainButton.onClick.AddListener(OnPlayAgainClicked);
        victoryMainMenuButton.onClick.AddListener(OnExitGameClicked);
    }

    private void RemoveButtonsListeners()
    {
        gameOverPlayAgainButton.onClick.RemoveAllListeners();
        gameOverMainMenuButton.onClick.RemoveAllListeners();
        victoryPlayAgainButton.onClick.RemoveAllListeners();
        victoryMainMenuButton.onClick.RemoveAllListeners();
    }

    private void SetCanvasGroup(CanvasGroup canvasGroup, bool state)
    {
        canvasGroup.alpha = state ? 1 : 0;
        canvasGroup.interactable = state;
        canvasGroup.blocksRaycasts = state;
    }

    public void ShowGameOverScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SetCanvasGroup(gameoverPanel, true);
    }

    public void ShowVictoryScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SetCanvasGroup(victoryPanel, true);
    }

    private void OnPlayAgainClicked()
    {
        gameManager.Resume();
        if (CustomSceneManager.Instance)
            CustomSceneManager.Instance.GoToGameplayImmediate();
    }

    private void OnExitGameClicked()
    {
        gameManager.Resume();
        if (CustomSceneManager.Instance)
            CustomSceneManager.Instance.GoToMainMenuImmediate();
    }
}
