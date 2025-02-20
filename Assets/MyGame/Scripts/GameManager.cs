using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] windmills;
    [SerializeField] private GameObject colorCube;
    private bool[] isLocked;
    private int cIndex = 0;
    private float[] lockedSpeeds;

    private void Start()
    {
        isLocked = new bool[windmills.Length];
        lockedSpeeds = new float[windmills.Length];

        Debug.Log("Windmühle gestartet");
        ActivateCurrentWindmill();
    }

    public void LockCurrentWindmill()
    {
        if (cIndex < windmills.Length && !isLocked[cIndex])
        {
            WindmillDynamicSpeed windmillSpeed = windmills[cIndex].GetComponent<WindmillDynamicSpeed>();

            if (windmillSpeed.currentSpeed > 0)
            {
                isLocked[cIndex] = true;
                lockedSpeeds[cIndex] = windmillSpeed.currentSpeed;
                windmillSpeed.enabled = false;
                Debug.Log("Windmühle " + cIndex + " gelocked");
            }
            else
            {
                Debug.Log("Windmühle " + cIndex + " kann nicht gelocked werden, weil Geschwindigkeit 0 ist.");
                return;
            }

            if (cIndex < windmills.Length - 1)
            {
                cIndex++;
                ActivateCurrentWindmill();
                Debug.Log("Wechsel zu Windmühle " + cIndex);
            }
            else
            {
                ApplyColorToCube();
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < windmills.Length; i++)
        {
            WindmillDynamicSpeed windmillSpeed = windmills[i].GetComponent<WindmillDynamicSpeed>();
            if (windmillSpeed != null && isLocked[i])
            {
                windmills[i].transform.Rotate(Vector3.forward * lockedSpeeds[i] * Time.deltaTime);
            }
        }
    }

    private void ApplyColorToCube()
    {
        if (colorCube == null)
        {
            Debug.LogError("ColorCube wurde nicht zugewiesen");
            return;
        }

        float red = 0f;
        float green = 0f;
        float blue = 0f;

        for (int i = 0; i < windmills.Length; i++)
        {
            WindmillDynamicSpeed windmillSpeed = windmills[i].GetComponent<WindmillDynamicSpeed>();

            if (windmillSpeed != null && windmillSpeed.speedSlider != null)
            {
                float sliderValue = Mathf.Clamp01(windmillSpeed.speedSlider.value / 255f);

                if (i == 0)
                {
                    red = sliderValue;
                }
                if (i == 1)
                {
                    green = sliderValue;
                }
                if (i == 2)
                {
                    blue = sliderValue;
                }
            }
        }

        Color finalColor = new Color(red, green, blue);
        colorCube.GetComponent<Renderer>().material.color = finalColor;
        Debug.Log("Cube hat jtzt die farbe: " + finalColor);
    }


    private void ActivateCurrentWindmill()
    {
        for (int i = 0; i < windmills.Length; i++)
        {
            WindmillDynamicSpeed windmillSpeed = windmills[i].GetComponent<WindmillDynamicSpeed>();

            if (windmillSpeed != null)
            {
                if (i == cIndex && !isLocked[i])
                {
                    windmillSpeed.enabled = true;
                    Debug.Log("Windmühle " + i + " aktiviert");
                }
                else
                {
                    windmillSpeed.enabled = false;
                    Debug.Log("Windmühle " + i + " deaktiviert");
                }
            }
            else
            {
                Debug.LogError("WindmillDynamicSpeed nicht gefunden bei: " + windmills[i].name);
            }
        }
    }

}
