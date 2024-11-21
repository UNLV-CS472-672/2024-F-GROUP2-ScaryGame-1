using UnityEngine;
using UnityEngine.SceneManagement;

public class Damager : MonoBehaviour
{
    public float firstFrameDamage = 1.0f;
    public float nextFrameDamage = 0.5f;
    public Transform playerTransform;
    public HealthSlider healthSlider;
    private bool damagedLastFrame = false;

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        healthSlider = GameObject.Find("Overlay/HealthSlider").GetComponent<HealthSlider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Because of the autostopping distance on the NavMeshAgent, I am just doing a distance check, not collision
        // Could probably be improved
        if(Vector3.Distance(transform.position, playerTransform.position) < 1.0f)
        {
            // Do more damage on first frame of contact
            if (!damagedLastFrame)
            {
                FirstContactDamage();
            } else
            {
                ContinuedDamage();
            }

            damagedLastFrame = true;
        } else
        {
            damagedLastFrame = false;
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
        healthSlider.TakeDamage(nextFrameDamage);
    }
}
