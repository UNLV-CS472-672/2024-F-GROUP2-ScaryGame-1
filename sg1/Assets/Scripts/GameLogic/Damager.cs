using UnityEngine;
using UnityEngine.SceneManagement;

public class Damager : MonoBehaviour
{
    public float firstFrameDamage = 1.0f;
    public float nextFrameDamage = 5f;
    public Transform playerTransform;
    public HealthSlider healthSlider;
    public NewAntagonistController antagonistController;
    private bool isDamaging = false;
    private float damagingTimer = 0f;
    private float damagingTimeLimit = 2f;
    private float cooldownTimer = 2f;
    private float cooldownTimeLimit = 2f;

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        healthSlider = GameObject.Find("Overlay/HealthSlider").GetComponent<HealthSlider>();
        antagonistController = GetComponent<NewAntagonistController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Once the antagonist starts to damage the player, they can continue damaging them for $damagingTimeLimit seconds
        // then it has to wait for $coolDownTimeLimit seconds before damaging again
        if(isDamaging) damagingTimer += Time.deltaTime;
        cooldownTimer += Time.deltaTime;

        if(damagingTimer > damagingTimeLimit)
        {
            cooldownTimer = 0f;
            damagingTimer = 0f;
            isDamaging = false;
            StartCoroutine(antagonistController.Avoid());
        }

        // Because of the autostopping distance on the NavMeshAgent, I am just doing a distance check, not collision
        // Could probably be improved
        if (cooldownTimer > cooldownTimeLimit && Vector3.Distance(transform.position, playerTransform.position) < 1.0f)
        {
            if(!isDamaging)
            {
                damagingTimer = 0f;
                FirstContactDamage();
            } else
            {
                ContinuedDamage();
            }
            isDamaging = true;
        } 

        // If player runs out of health, load the title scene
        if(healthSlider.currentHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("TitleScreen");
        }
    }

    void FirstContactDamage()
    {
        healthSlider.TakeDamage(firstFrameDamage);
    }

    void ContinuedDamage()
    {
        healthSlider.TakeDamage(Time.deltaTime * nextFrameDamage);
    }
}
