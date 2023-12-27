using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// FPSDisplay is a base class that shows what the FPS of the game is.
/// </summary>
public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI FPSText;

    private float pollingTime = 1.0f;
    private float time;
    private int frameCount;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        frameCount++;

        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            FPSText.text = frameRate.ToString() + "FPS";

            time -= pollingTime;
            frameCount = 0;
        }
    }
}
