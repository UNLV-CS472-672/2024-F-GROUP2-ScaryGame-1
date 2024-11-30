using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class PressTheButtonMinigame : MonoBehaviour, IMiniGame
{
    public Button[] buttons; // Array of UI Buttons in the mini-game
    public GameObject miniGameCanvas; // Reference to the mini-game Canvas or parent GameObject to deactivate
    public GameObject failCanvas;
    public GameObject successCanvas;
    public GameObject particleRed;
    public GameObject particleGreen;
    public Color flashColor = Color.yellow; // Color to flash the buttons
    public float flashDuration = 0.5f; // Duration each button flashes
    public float delayBetweenFlashes = 0.5f; // Delay between each button flash
    public float initialDelay = 0.5f; // Initial delay before starting the flashing sequence
    public int maxSequenceLength = 5; // Length of how long the sequence generated will be

    private Color[] originalColors; // Store the original colors of the buttons
    private int[] correctSequence; // Randomly generated sequence of button indices
    private int currentStep = 0; // Track the player's progress in the sequence
    private bool playerCanInteract = false; // Allow player interaction only after flashing sequence
    private bool isGameCompleted = false; // Track if the game has already been completed
    
    void Start()
    {
        originalColors = new Color[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            originalColors[i] = buttons[i].GetComponent<Image>().color;
        }
    }

    // Called anytime the GameObject is enabled, starts a new sequence
    void OnEnable()
    {
        // reset colors
        for (int i = 0; originalColors != null && i < buttons.Length; ++i)
        {
            buttons[i].GetComponent<Image>().color = originalColors[i];
        }

        StartNewSequence();
    }

    // Generate and display a new sequence
    void StartNewSequence()
    {
        if (isGameCompleted) return;

        GenerateRandomSequence();

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.RemoveAllListeners(); // Only want one listener
            buttons[i].onClick.AddListener(() => OnButtonPressed(index));
        }

        StartCoroutine(FlashSequence());
    }

    void GenerateRandomSequence()
    {
        int sequenceLength = Mathf.Min(buttons.Length, maxSequenceLength); // Change how long the sequence is
        correctSequence = Enumerable.Range(0, buttons.Length)
                                    .OrderBy(x => Random.value)
                                    .Take(sequenceLength)
                                    .ToArray();
    }

    // Display sequence
    IEnumerator FlashSequence()
    {
        if (isGameCompleted) yield break;

        playerCanInteract = false;
        yield return new WaitForSeconds(initialDelay);

        // temporarily change color of each button in sequence
        for (int i = 0; i < correctSequence.Length; i++)
        {
            int buttonIndex = correctSequence[i];
            buttons[buttonIndex].GetComponent<Image>().color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            buttons[buttonIndex].GetComponent<Image>().color = originalColors[buttonIndex];
            yield return new WaitForSeconds(delayBetweenFlashes);
        }

        playerCanInteract = true;
        currentStep = 0;
    }

    public void OnButtonPressed(int buttonIndex)
    {
        if (!playerCanInteract || isGameCompleted) return;

        if (buttonIndex == correctSequence[currentStep])
        {
            currentStep++;

            // Display win message if player remembers sequence
            if (currentStep >= correctSequence.Length)
            {
                Debug.Log("Correct sequence completed!");
                isGameCompleted = true;
                CompleteMiniGame();
            }
        }
        else
        {
            Debug.Log("Wrong sequence!");
            playerCanInteract = false;
            currentStep = 0;
            StartCoroutine(LoseGame());
        }
    }

    private void CloseMiniGame()
    {
        playerCanInteract = false;
        miniGameCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void TryOpenMiniGame()
    {
        if (isGameCompleted)
        {
            Debug.Log("Mini-game has already been completed and cannot be reopened.");
            return;
        }

        miniGameCanvas.SetActive(true);
        StartCoroutine(FlashSequence());
    }

    public void CompleteMiniGame()
    {
        // display win message
        miniGameCanvas.SetActive(false);
        successCanvas.SetActive(true);
        failCanvas.SetActive(false);
        particleRed.SetActive(false);
        particleGreen.SetActive(true);
        MinigameManager.instance.MiniGameCompleted();
    }

    public IEnumerator LoseGame()
    {
        // temporarily show lose message
        miniGameCanvas.SetActive(false);
        successCanvas.SetActive(false);
        failCanvas.SetActive(true);
        yield return new WaitForSeconds(1f);

        // start a new game
        miniGameCanvas.SetActive(true);
        successCanvas.SetActive(false);
        failCanvas.SetActive(false);
        StartNewSequence();
    }
}
