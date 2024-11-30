using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuTitle : MonoBehaviour
{
    // Public variables for UI components
    public GameObject optionsCanvas;
    public GameObject mainPanel;
    public GameObject sensitivityPanel;
    public GameObject volumePanel;
    public Button sensitivityButton;
    public Button volumeButton;
    public Button backButtonMainPanel;
    public Button backButtonSensitivityPanel;
    public Button backButtonVolumePanel;
    public Slider sensitivityXSlider;
    public Slider sensitivityYSlider;
    public Slider volumeSlider;
    public InputField sensitivityXInputField;
    public InputField sensitivityYInputField;
    public InputField volumeInputField;
    public Button sensitivityApplyButton;
    public Button sensitivitySetToDefaultButton;
    public Button volumeApplyButton;

    // Default sensitivity values
    private float defaultPitchSensitivity = 1.5f;
    private float defaultYawSensitivity = 1.5f;

    // Current and temporary sensitivity values
    private float currentPitchSensitivity;
    private float currentYawSensitivity;
    private float tempPitchSensitivity;
    private float tempYawSensitivity;

    // Volume values
    private const float defaultVolume = 1.0f;
    private float currentVolume;

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
        InitializeVolumeSettings();
        AddButtonListeners();
        // Hide all panels initially
        HideAllPanels();
        // Update the state of the apply button
        UpdateApplyButtonState();
    }

    public void Initialize()
    {
        InitializeSensitivitySettings();
        InitializeVolumeSettings();
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
        volumeButton.onClick.AddListener(ShowVolumePanel);
        backButtonVolumePanel.onClick.AddListener(BackToMainPanelFromVolume);
        volumeApplyButton.onClick.AddListener(ApplyVolumeSettings);
    }

    void HideAllPanels()
    {
        // Hide all panels
        optionsCanvas.SetActive(false);
        mainPanel.SetActive(false);
        sensitivityPanel.SetActive(false);
        sensitivitySetToDefaultButton.gameObject.SetActive(false);
        volumePanel.SetActive(false);
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

    void ShowVolumePanel()
    {
        // Hide the main panel
        mainPanel.SetActive(false);

        // Reload the saved volume setting
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", defaultVolume);
        AudioSettingsManager.MasterVolume = savedVolume;

        // Update the slider and input field to reflect the saved volume
        volumeSlider.value = savedVolume;
        volumeInputField.text = (savedVolume * 100).ToString("F0");

        // Disable the Apply button (no changes yet)
        volumeApplyButton.interactable = false;

        // Show the volume panel
        volumePanel.SetActive(true);
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

    void BackToMainPanelFromVolume()
    {
        // Check if the Apply button is still interactable (indicating unsaved changes)
        if (volumeApplyButton.interactable)
        {
            // Revert the slider and input field to the saved volume
            float savedVolume = PlayerPrefs.GetFloat("MasterVolume", defaultVolume);
            volumeSlider.value = savedVolume;
            volumeInputField.text = (savedVolume * 100).ToString("F0");

            // Revert the global master volume
            AudioSettingsManager.MasterVolume = savedVolume;
        }

        // Disable the Apply button (no changes pending)
        volumeApplyButton.interactable = false;

        // Hide the volume panel and show the main panel
        volumePanel.SetActive(false);
        mainPanel.SetActive(true);
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
        var playerMovement = FindFirstObjectByType<Movement>();
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

    void InitializeVolumeSettings()
    {
        // Load saved volume or use default
        AudioSettingsManager.MasterVolume = PlayerPrefs.GetFloat("MasterVolume", defaultVolume);
        currentVolume = PlayerPrefs.GetFloat("MasterVolume", defaultVolume);

        // Configure slider
        volumeSlider.minValue = 0.0f;
        volumeSlider.maxValue = 1.0f;
        volumeSlider.value = currentVolume;

        // Configure input field
        volumeInputField.text = (currentVolume * 100).ToString("F0");

        // Disable Apply button initially
        volumeApplyButton.interactable = false;

        // Add listeners
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);
        volumeInputField.onEndEdit.AddListener(OnVolumeInputChanged);
    }

    void OnVolumeSliderChanged(float value)
    {
        // Update input field to reflect slider value
        volumeInputField.text = (value * 100).ToString("F0");

        // Update all AudioSources in real time
        AudioSettingsManager.MasterVolume = value; 

        // Enable the Apply button if the value has changed
        volumeApplyButton.interactable = !Mathf.Approximately(value, PlayerPrefs.GetFloat("MasterVolume", defaultVolume));
    }

    void OnVolumeInputChanged(string value)
    {
        if (float.TryParse(value, out float parsedValue))
        {
            // Clamp value to 0-100 and set slider
            parsedValue = Mathf.Clamp(parsedValue, 0, 100);
            volumeSlider.value = parsedValue / 100f;

            // Update global master volume
            AudioSettingsManager.MasterVolume = volumeSlider.value;

            // Enable Apply button if the new value differs from saved value
            volumeApplyButton.interactable = !Mathf.Approximately(volumeSlider.value, PlayerPrefs.GetFloat("MasterVolume", defaultVolume));
        }
    }

    void ApplyVolumeSettings()
    {
        // Save the new volume setting
        float newVolume = volumeSlider.value;
        AudioSettingsManager.MasterVolume = newVolume;
        PlayerPrefs.SetFloat("MasterVolume", newVolume);
        PlayerPrefs.Save();

        // Synchronize the UI
        volumeSlider.value = newVolume;
        volumeInputField.text = (newVolume * 100).ToString("F0");
        volumeApplyButton.interactable = false;
    }



}
