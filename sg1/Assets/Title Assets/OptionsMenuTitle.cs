using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuTitle : MonoBehaviour
{
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
    private float defaultPitchSensitivity = 1.5f;
    private float defaultYawSensitivity = 1.5f; // Match user's message
    private float currentPitchSensitivity;
    private float currentYawSensitivity;
    private float tempPitchSensitivity;
    private float tempYawSensitivity;
    private bool changesApplied = false;

    void OnEnable()
    {
        KeyEventManager.OnEscapeKeyPressed += ToggleOptionsCanvas;
    }

    void OnDisable()
    {
        KeyEventManager.OnEscapeKeyPressed -= ToggleOptionsCanvas;
    }

    void Start()
    {
        InitializeSensitivitySettings();
        AddButtonListeners();
        HideAllPanels();
        UpdateApplyButtonState();
    }

    void InitializeSensitivitySettings()
    {
        currentPitchSensitivity = PlayerPrefs.GetFloat("PitchSensitivity", defaultPitchSensitivity);
        currentYawSensitivity = PlayerPrefs.GetFloat("YawSensitivity", defaultYawSensitivity);
        sensitivityYSlider.minValue = 0.1f;
        sensitivityYSlider.maxValue = 5f;
        sensitivityXSlider.minValue = 0.1f;
        sensitivityXSlider.maxValue = 5f;
        sensitivityYSlider.value = currentPitchSensitivity;
        sensitivityXSlider.value = currentYawSensitivity;
        sensitivityYInputField.text = currentPitchSensitivity.ToString();
        sensitivityXInputField.text = currentYawSensitivity.ToString();
        sensitivityXSlider.onValueChanged.AddListener(OnSensitivityChanged);
        sensitivityYSlider.onValueChanged.AddListener(OnSensitivityChanged);
        sensitivityXInputField.onEndEdit.AddListener(OnSensitivityInputChanged);
        sensitivityYInputField.onEndEdit.AddListener(OnSensitivityInputChanged);
    }

    void AddButtonListeners()
    {
        sensitivityButton.onClick.AddListener(ShowSensitivityPanel);
        sensitivityApplyButton.onClick.AddListener(ApplySensitivitySettings);
        sensitivitySetToDefaultButton.onClick.AddListener(SetSensitivityToDefault);
        backButtonMainPanel.onClick.AddListener(HideOptionsCanvas);
        backButtonSensitivityPanel.onClick.AddListener(BackToMainPanel);
    }

    void HideAllPanels()
    {
        optionsCanvas.SetActive(false);
        mainPanel.SetActive(false);
        sensitivityPanel.SetActive(false);
        sensitivitySetToDefaultButton.gameObject.SetActive(false);
    }

    void ToggleOptionsCanvas()
    {
        bool isActive = optionsCanvas.activeSelf;
        optionsCanvas.SetActive(!isActive);
        if (isActive)
        {
            HideAllPanels();
        }
        else
        {
            mainPanel.SetActive(true);
        }
    }

    void ShowSensitivityPanel()
    {
        mainPanel.SetActive(false);
        sensitivityPanel.SetActive(true);
        tempPitchSensitivity = currentPitchSensitivity;
        tempYawSensitivity = currentYawSensitivity;
        changesApplied = false; // Reset the changesApplied flag when entering the panel
        UpdateApplyButtonState();
    }

    void BackToMainPanel()
    {
        if (!changesApplied)
        {
            sensitivityYSlider.value = tempPitchSensitivity;
            sensitivityXSlider.value = tempYawSensitivity;
            sensitivityYInputField.text = tempPitchSensitivity.ToString();
            sensitivityXInputField.text = tempYawSensitivity.ToString();
        }
        sensitivityPanel.SetActive(false);
        mainPanel.SetActive(true);
        UpdateApplyButtonState();
    }

    void HideOptionsCanvas()
    {
        optionsCanvas.SetActive(false);
    }

    void OnSensitivityChanged(float value)
    {
        sensitivityXInputField.text = sensitivityXSlider.value.ToString();
        sensitivityYInputField.text = sensitivityYSlider.value.ToString();
        UpdateApplyButtonState();
        CheckIfSensitivityChanged(); // Ensure the Set to Default button appears/disappears correctly
    }

    void OnSensitivityInputChanged(string value)
    {
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
        currentPitchSensitivity = sensitivityYSlider.value;
        currentYawSensitivity = sensitivityXSlider.value;
        PlayerPrefs.SetFloat("PitchSensitivity", currentPitchSensitivity);
        PlayerPrefs.SetFloat("YawSensitivity", currentYawSensitivity);
        PlayerPrefs.Save();
        sensitivityApplyButton.interactable = false; // Disable apply button after changes are applied
        changesApplied = true; // Set the changesApplied flag to true
    }

    void SetSensitivityToDefault()
    {
        sensitivityXSlider.value = defaultYawSensitivity;
        sensitivityYSlider.value = defaultPitchSensitivity;
        ApplySensitivitySettings(); // Automatically apply changes
    }

    void CheckIfSensitivityChanged()
    {
        if (sensitivityXSlider.value != defaultYawSensitivity || sensitivityYSlider.value != defaultPitchSensitivity)
        {
            sensitivitySetToDefaultButton.gameObject.SetActive(true);
        }
        else
        {
            sensitivitySetToDefaultButton.gameObject.SetActive(false);
        }
    }

    void UpdateApplyButtonState()
    {
        if (sensitivityXSlider.value != currentYawSensitivity || sensitivityYSlider.value != currentPitchSensitivity)
        {
            sensitivityApplyButton.interactable = true;
        }
        else
        {
            sensitivityApplyButton.interactable = false;
        }
        CheckIfSensitivityChanged(); // Ensure the Set to Default button state is always updated
    }
}
