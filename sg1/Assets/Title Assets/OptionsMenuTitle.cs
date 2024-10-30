using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuTitle : MonoBehaviour
{
    // Public variables for UI components
    public GameObject optionsCanvas;
    public GameObject mainPanel;
    public GameObject sensitivityPanel;
    public Button sensitivityButton;
    public Button backButtonMainPanel;
    public Button backButtonSensitivityPanel;
    public Slider sensitivityXSlider;
    public Slider sensitivityYSlider;
    public InputField sensitivityXInputField;
    public InputField sensitivityYInputField;
    public Button sensitivityApplyButton;
    public Button sensitivitySetToDefaultButton;

    // Default sensitivity values
    private float defaultPitchSensitivity = 1.5f;
    private float defaultYawSensitivity = 1.5f;

    // Current and temporary sensitivity values
    private float currentPitchSensitivity;
    private float currentYawSensitivity;
    private float tempPitchSensitivity;
    private float tempYawSensitivity;

    // Flag to track if changes were applied
    private bool changesApplied = false;

    void OnEnable()
    {
        // Subscribe to the OnEscapeKeyPressed event
        KeyEventManager.OnEscapeKeyPressed += ToggleOptionsCanvas;
    }

    void OnDisable()
    {
        // Unsubscribe from the OnEscapeKeyPressed event
        KeyEventManager.OnEscapeKeyPressed -= ToggleOptionsCanvas;
    }

    void Start()
    {
        // Initialize sensitivity settings and add button listeners
        InitializeSensitivitySettings();
        AddButtonListeners();
        // Hide all panels initially
        HideAllPanels();
        // Update the state of the apply button
        UpdateApplyButtonState();
    }

    public void Initialize()
    {
        InitializeSensitivitySettings();
        AddButtonListeners();
        HideAllPanels();
        UpdateApplyButtonState();
    }

    void InitializeSensitivitySettings()
    {
        // Load sensitivity settings from PlayerPrefs or use default values
        currentPitchSensitivity = PlayerPrefs.GetFloat("PitchSensitivity", defaultPitchSensitivity);
        currentYawSensitivity = PlayerPrefs.GetFloat("YawSensitivity", defaultYawSensitivity);
        // Set the min and max values for the sliders
        sensitivityYSlider.minValue = 0.1f;
        sensitivityYSlider.maxValue = 5f;
        sensitivityXSlider.minValue = 0.1f;
        sensitivityXSlider.maxValue = 5f;
        // Set the slider values to the current sensitivity
        sensitivityYSlider.value = currentPitchSensitivity;
        sensitivityXSlider.value = currentYawSensitivity;
        // Set the input field text to the current sensitivity
        sensitivityYInputField.text = currentPitchSensitivity.ToString();
        sensitivityXInputField.text = currentYawSensitivity.ToString();
        // Add listeners for slider value changes
        sensitivityXSlider.onValueChanged.AddListener(OnSensitivityChanged);
        sensitivityYSlider.onValueChanged.AddListener(OnSensitivityChanged);
        // Add listeners for input field value changes
        sensitivityXInputField.onEndEdit.AddListener(OnSensitivityInputChanged);
        sensitivityYInputField.onEndEdit.AddListener(OnSensitivityInputChanged);
    }

    void AddButtonListeners()
    {
        // Add listeners for button clicks
        sensitivityButton.onClick.AddListener(ShowSensitivityPanel);
        sensitivityApplyButton.onClick.AddListener(ApplySensitivitySettings);
        sensitivitySetToDefaultButton.onClick.AddListener(SetSensitivityToDefault);
        backButtonMainPanel.onClick.AddListener(HideOptionsCanvas);
        backButtonSensitivityPanel.onClick.AddListener(BackToMainPanel);
    }

    void HideAllPanels()
    {
        // Hide all panels
        optionsCanvas.SetActive(false);
        mainPanel.SetActive(false);
        sensitivityPanel.SetActive(false);
        sensitivitySetToDefaultButton.gameObject.SetActive(false);
    }

    void ToggleOptionsCanvas()
    {
        // Toggle the options canvas visibility
        bool isActive = optionsCanvas.activeSelf;
        optionsCanvas.SetActive(!isActive);
        if (isActive)
        {
            // Hide all panels if the options canvas is deactivated
            HideAllPanels();
        }
        else
        {
            // Show the main panel if the options canvas is activated
            mainPanel.SetActive(true);
        }
    }

    void ShowSensitivityPanel()
    {
        // Show the sensitivity panel and store current sensitivity values temporarily
        mainPanel.SetActive(false);
        sensitivityPanel.SetActive(true);
        tempPitchSensitivity = currentPitchSensitivity;
        tempYawSensitivity = currentYawSensitivity;
        changesApplied = false; // Reset the changesApplied flag when entering the panel
        UpdateApplyButtonState();
    }

    void BackToMainPanel()
    {
        // Revert changes if they were not applied
        if (!changesApplied)
        {
            sensitivityYSlider.value = tempPitchSensitivity;
            sensitivityXSlider.value = tempYawSensitivity;
            sensitivityYInputField.text = tempPitchSensitivity.ToString();
            sensitivityXInputField.text = tempYawSensitivity.ToString();
        }
        // Show the main panel
        sensitivityPanel.SetActive(false);
        mainPanel.SetActive(true);
        UpdateApplyButtonState();
    }

    void HideOptionsCanvas()
    {
        // Hide the options canvas
        optionsCanvas.SetActive(false);
    }

    void OnSensitivityChanged(float value)
    {
        // Update input field text when slider value changes
        sensitivityXInputField.text = sensitivityXSlider.value.ToString();
        sensitivityYInputField.text = sensitivityYSlider.value.ToString();
        UpdateApplyButtonState();
        CheckIfSensitivityChanged(); // Ensure the Set to Default button appears/disappears correctly
    }

    void OnSensitivityInputChanged(string value)
    {
        // Update slider value when input field value changes
        if (float.TryParse(sensitivityXInputField.text, out float xValue))
        {
            sensitivityXSlider.value = xValue;
        }
        if (float.TryParse(sensitivityYInputField.text, out float yValue))
        {
            sensitivityYSlider.value = yValue;
        }
        UpdateApplyButtonState();
        CheckIfSensitivityChanged(); // Ensure the Set to Default button appears/disappears correctly
    }

    void ApplySensitivitySettings()
    {
        // Update the current sensitivity values to the slider values.
        currentPitchSensitivity = sensitivityYSlider.value;
        currentYawSensitivity = sensitivityXSlider.value;

        // Save the current sensitivity values to PlayerPrefs.
        PlayerPrefs.SetFloat("PitchSensitivity", currentPitchSensitivity);
        PlayerPrefs.SetFloat("YawSensitivity", currentYawSensitivity);
        PlayerPrefs.Save(); // Ensure the changes are saved.

        // Disable the apply button after the changes have been applied.
        sensitivityApplyButton.interactable = false;

        // Indicate that the changes have been applied.
        changesApplied = true;

        // Notify the Movement script to update sensitivity settings
        var playerMovement = FindObjectOfType<Movement>();
        if (playerMovement != null)
        {
            playerMovement.UpdateSensitivitySettings();
        }
    }

    void SetSensitivityToDefault()
    {
        // Set the sensitivity slider values to the default settings.
        sensitivityXSlider.value = defaultYawSensitivity;
        sensitivityYSlider.value = defaultPitchSensitivity;

        // Automatically apply the default settings.
        ApplySensitivitySettings();
    }

    void CheckIfSensitivityChanged()
    {
        // If either of the sensitivity values is different from the defaults, show the set-to-default button.
        if (sensitivityXSlider.value != defaultYawSensitivity || sensitivityYSlider.value != defaultPitchSensitivity)
        {
            sensitivitySetToDefaultButton.gameObject.SetActive(true);
        }
        else
        {
            // Otherwise, hide the set-to-default button.
            sensitivitySetToDefaultButton.gameObject.SetActive(false);
        }
    }

    void UpdateApplyButtonState()
    {
        // If the current slider values are different from the saved sensitivity values, enable the apply button.
        if (sensitivityXSlider.value != currentYawSensitivity || sensitivityYSlider.value != currentPitchSensitivity)
        {
            sensitivityApplyButton.interactable = true;
        }
        else
        {
            // Otherwise, disable the apply button.
            sensitivityApplyButton.interactable = false;
        }
        
        // Check if the sensitivity values have changed to update the set-to-default button.
        CheckIfSensitivityChanged();
    }

}
