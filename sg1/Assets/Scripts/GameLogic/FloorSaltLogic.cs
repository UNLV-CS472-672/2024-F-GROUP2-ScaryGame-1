using UnityEngine;

public class FloorSaltLogic : MonoBehaviour
{
    private const float DISSIPATE_TIME = 5f;
    private float time_since_thrown = 0f;
    private float opacity = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //check if the floor salt was just thrown, then set a timer
        if (this.gameObject.activeSelf == true)
        {
            if (time_since_thrown == 0f)
            {
                time_since_thrown = Time.realtimeSinceStartup;
            }

            // after DISSIPATE_TIME seconds, destroy the object
            if ((Time.realtimeSinceStartup - time_since_thrown) > DISSIPATE_TIME)
            {
                Destroy(this.gameObject);
            }

            // gradually decrease opacity from 1f to 0f based on time / DISSIPATE_TIME
            opacity = 1- ((Time.realtimeSinceStartup - time_since_thrown) / DISSIPATE_TIME);

            // get the material's color, including alpha, and change it to the new alpha value, then reassign
            var cur_color = gameObject.GetComponent<Renderer>().material.color;
            var color_opacity = new Color(cur_color.r, cur_color.g, cur_color.b, opacity);
            this.gameObject.GetComponent<Renderer>().material.color = color_opacity;
        }
    }
}
