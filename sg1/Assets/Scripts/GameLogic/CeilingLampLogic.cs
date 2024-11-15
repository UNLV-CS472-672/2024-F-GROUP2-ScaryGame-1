using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CeilingLampLogic : MonoBehaviour
{
    public Transform AntagonistTransform;

    private const float LIGHTOFFRADIUS = 1.6f;
    private const float OFFTIME = 2.0f;
    private const float INTENSITY = 0.7f;
    private const bool ENABLED = true;

    private float gametime_at_off = 0f;
    private bool light_on = true;
    private Light ThisLight;
    private MeshRenderer ThisRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize light object
        ThisLight = this.GetComponent<Light>();
        // Initialize renderer object
        ThisRenderer = this.GetComponent<MeshRenderer>();
        // Sets light intensity based on variable
        ThisLight.intensity = INTENSITY;

        if (!ENABLED) this.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        // Check if the light is off or on
        if (light_on)
        {
            // Light is on - check that the antagonist is nearby, then flicker the light off
            if (Vector3.Distance(this.gameObject.transform.position, AntagonistTransform.position) < LIGHTOFFRADIUS)
            {
                LightFlickerOff();
            }
        }
        else
        {
            // Light is off - check that it has been at least x seconds since it turned off
            if ((Time.realtimeSinceStartup - gametime_at_off) > OFFTIME)
            {
                // Light was turned off over x seconds ago, check that the antagonist is not near. then turn the light back on
                if (Vector3.Distance(this.gameObject.transform.position, AntagonistTransform.position) > LIGHTOFFRADIUS)
                {
                    LightFlickerOn();
                }
            }
        }
    }

    // A function that is called to turn the light object on
    private void LightFlickerOff()
    {
        light_on = false;
        gametime_at_off = Time.realtimeSinceStartup;
        StartCoroutine(FlickerOffTimedActions());
    }

    // A time-based, blocking function that turns the light object of the lamp off, on, then off to emulate "flickering" off
    private IEnumerator FlickerOffTimedActions()
    {
        ThisLight.enabled = false;
        ThisRenderer.materials[1].SetVector("_EmissionColor", new Vector4(0.1f, 0.1f, 0.1f));
        yield return new WaitForSeconds(0.1f);
        ThisLight.enabled = true;
        ThisRenderer.materials[1].SetVector("_EmissionColor", new Vector4(0.5f, 0.5f, 0.5f));
        yield return new WaitForSeconds(0.1f);
        ThisLight.enabled = false;
        ThisRenderer.materials[1].SetVector("_EmissionColor", new Vector4(0.1f, 0.1f, 0.1f));

    }

    // A function that is called to turn the light object off
    private void LightFlickerOn()
    {
        light_on = true;
        StartCoroutine(FlickerOnTimedActions());
    }
    // A time-based, blocking function that turns the light object of the lamp on, off, then on to emulate "flickering" on
    private IEnumerator FlickerOnTimedActions()
    {
        ThisLight.enabled = true;
        ThisRenderer.materials[1].SetVector("_EmissionColor", new Vector4(0.5f, 0.5f, 0.5f));
        yield return new WaitForSeconds(0.1f);
        ThisLight.enabled = false;
        ThisRenderer.materials[1].SetVector("_EmissionColor", new Vector4(0.1f, 0.1f, 0.1f));
        yield return new WaitForSeconds(0.1f);
        ThisLight.enabled = true;
        ThisRenderer.materials[1].SetVector("_EmissionColor", new Vector4(0.5f, 0.5f, 0.5f));

    }
}
