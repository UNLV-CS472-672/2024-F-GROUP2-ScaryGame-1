using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CeilingLampLogic : MonoBehaviour
{
    public Transform AntagonistTransform;

    ///// KILL SWITCH v /////
    private const bool ENABLED = true;

    private const float LIGHTOFFRADIUS = 1.6f;
    private const float OFFTIME = 2.0f;
    private const float INTENSITY = 0.7f;
    private const int FLICKERWAITLOW = 10, FLICKERWAITHIGH = 50;

    private float gametime_at_off = 0f;
    private bool light_on = true;
    private int flicker_wait = 0;
    private bool waiting_to_flicker = false;
    private Light ThisLight;
    private MeshRenderer ThisRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Allows for all lights to be turned off with one line of code. See ENABLED const
        if (!ENABLED) this.enabled = false;

        // Initialize light object
        ThisLight = this.GetComponent<Light>();
        // Initialize renderer object
        ThisRenderer = this.GetComponent<MeshRenderer>();
        // Sets light intensity based on variable
        ThisLight.intensity = INTENSITY;
        //Initialize random flicker wait
        flicker_wait = Random.Range(FLICKERWAITLOW, FLICKERWAITHIGH);

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
            else
            {
                RandomLightFlicker();

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

    // A function that is called to turn the light object off and on quickly
    private void RandomLightFlicker()
    {
        // Ensure that we aren't already waiting for the lightbulb to flicker off and on
        if (waiting_to_flicker == false)
        {
            // Start thread to wait for a flicker event
            waiting_to_flicker = true;
            StartCoroutine(FlickerTimedActions());
        }
    }
    // A time-based, blocking function that turns the light object of the lamp off then on
    private IEnumerator FlickerTimedActions()
    {
        // Wait for predetermined random amount of time before flickering
        yield return new WaitForSeconds(flicker_wait);
        // Turn light off, modify material, wait 0.1s, turn light back on
        ThisLight.enabled = false;
        ThisRenderer.materials[1].SetVector("_EmissionColor", new Vector4(0.1f, 0.1f, 0.1f));
        yield return new WaitForSeconds(0.1f);
        ThisLight.enabled = true;
        ThisRenderer.materials[1].SetVector("_EmissionColor", new Vector4(0.5f, 0.5f, 0.5f));

        // Re-enable flicker countdown
        waiting_to_flicker = false;
        // Generate a new random amount of time to wait
        flicker_wait = Random.Range(FLICKERWAITLOW, FLICKERWAITHIGH);
    }
}
