using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    // Initializing Variables

    // Slider inside of "HealthSlider"
    public Slider healthSlider; 
    // Maximum Health Value
    public float maxHealth = 100f; 
    // Current Hleath Value
    public float currentHealth; 

    //  ##########################   Initializing Function  ##################################
    void Start()
    {
        // Starting Game at 100 Health
        currentHealth = maxHealth;

        // Setting Max on Slider (Should Always be 100, otherwise player will never be at max health)
        healthSlider.maxValue = maxHealth;

        // Setting Slider Current Value to Initialized Value 100
        healthSlider.value = currentHealth;

    }
    //  #######################################################################################


    //  #################   Function for Reducing Health, Give Int to Reduce ##################
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthSlider.value = currentHealth;
    }
    //  #######################################################################################


    //  #################  Function for Increasing Health, Give Int to Increase ############### 
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Clamp health between 0 and maxHealth
        healthSlider.value = currentHealth;
    }
    //  #################################################################


    //  ###### Function to Allow Manual Health Changes in "Health Slider" for Debug ##########
    void Update()
    {
        healthSlider.value = currentHealth;
    }
    //  #######################################################################################

}
