using System;
using System.Collections;
using UnityEngine;

public class DeviceOrientationChange : MonoBehaviour
{
    public static event Action<Vector2> OnResolutionChange;
    public static event Action<DeviceOrientation> OnOrientationChange;
    public static float CheckDelay = 0.4f;        // How long to wait until we check again.

    static Vector2 resolution;                    // Current Resolution
    static DeviceOrientation orientation;        // Current Device Orientation
    static bool isAlive = true;                    // Keep this script running?

    void Start()
    {
        StartCoroutine(CheckForChange());
    }

    IEnumerator CheckForChange()
    {  
        resolution = new Vector2(Screen.width, Screen.height);
        orientation = Input.deviceOrientation;

        while (isAlive)
        {

            // Check for a Resolution Change
            if (resolution.x != Screen.width || resolution.y != Screen.height)
            {
                resolution = new Vector2(Screen.width, Screen.height);
                if (OnResolutionChange != null) OnResolutionChange(resolution);
            }

            // Check for an Orientation Change
            switch (Input.deviceOrientation)
            {
                case DeviceOrientation.Unknown:            // Ignore
                case DeviceOrientation.FaceUp:            // Ignore
                case DeviceOrientation.FaceDown:        // Ignore
                    break;
                default:
                    if (orientation != Input.deviceOrientation)
                    {
                        if (Master.GameLaunched == false)
                        {
                            orientation = Input.deviceOrientation;

                            if (orientation == DeviceOrientation.Portrait)
                            {
                                Screen.orientation = ScreenOrientation.Portrait;
                            }
                            else if (orientation == DeviceOrientation.LandscapeLeft)
                            {
                                Screen.orientation = ScreenOrientation.LandscapeLeft;
                            }
                            else if (orientation == DeviceOrientation.LandscapeRight)
                            {
                                Screen.orientation = ScreenOrientation.LandscapeRight;
                            }


                            EventsProvider.OnOrientationChanged?.Invoke(orientation);
                        }
                    }
                    break;
            }

            yield return new WaitForSeconds(CheckDelay);
        }
    }

    void OnDestroy()
    {
        isAlive = false;
    }

}