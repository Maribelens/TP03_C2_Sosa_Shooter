using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CanvasGroup = UnityEngine.CanvasGroup;

public class UIResultScreen : MonoBehaviour
{
    [Header("Game Over panel")]
    [SerializeField] private CanvasGroup gameoverPanel;
    [SerializeField] private Button gameOverPlayAgainButton;
    [SerializeField] private Button gameOverMainMenuButton;

    [Header("Victory panel")]
    [SerializeField] private CanvasGroup victoryPanel;
    [SerializeField] private Button victoryPlayAgainButton;
    [SerializeField] private Button victoryMainMenuButton;

    [Header("Manager")]
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        SetCanvasGroup(gameoverPanel, false);
        SetCanvasGroup(victoryPanel, false);
        AddButtonsListeners();
    }

    private void OnEnable()
    {
        // Suscribe los eventos del GameManager.
        if (gameManager != null)
        {
            gameManager.OnGameOver += ShowGameOverScreen;
            gameManager.OnVictory += ShowVictoryScreen;
        }
    }

    private void OnDisable()
    {
        // Desuscribe los eventos para evitar errores.
        gameManager.OnGameOver -= ShowGameOverScreen;
        gameManager.OnVictory -= ShowVictoryScreen;
    }

    private void OnDestroy()
    {
        RemoveButtonsListeners();
    }

    private void AddButtonsListeners()
    {
        // Asigna funciones a los botones de ambos paneles.
        gameOverPlayAgainButton.onClick.AddListener(OnPlayAgainClicked);
        gameOverMainMenuButton.onClick.AddListener(OnExitGameClicked);
        victoryPlayAgainButton.onClick.AddListener(OnPlayAgainClicked);
        victoryMainMenuButton.onClick.AddListener(OnExitGameClicked);
    }
    private void RemoveButtonsListeners()
    {
        // Elimina los listeners para prevenir referencias colgantes.
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
        SetCanvasGroup(gameoverPanel, true);
    }
    public void ShowVictoryScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        SetCanvasGroup(gameoverPanel, true);
    }
    private void OnPlayAgainClicked()
    {
        gameManager.Resume();
        CustomSceneManager.Instance.GoToGameplayImmediate();
        //SceneManager.LoadScene(1);
    }
    private void OnExitGameClicked()
    {
        gameManager.Resume();
        CustomSceneManager.Instance.GoToMainMenuImmediate();
        //SceneManager.LoadScene(0);
    }
}
