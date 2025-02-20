using UnityEngine;
using UnityEngine.UI;

public class WindmillLockSystem : MonoBehaviour
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
                Debug.Log("Windmühle " + cIndex + "geblockt");
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
        if (colorCube != null)
        {
            float windmillSlider1 = windmills[0].GetComponent<WindmillDynamicSpeed>().speedSlider.value;
            float windmillSlider2 = windmills[1].GetComponent<WindmillDynamicSpeed>().speedSlider.value;
            float windmillSlider3 = windmills[2].GetComponent<WindmillDynamicSpeed>().speedSlider.value;

            colorCube.GetComponent<Renderer>().material.color = new Color(
                Mathf.Clamp01(windmillSlider1 / 255f),
                Mathf.Clamp01(windmillSlider2 / 255f),
                Mathf.Clamp01(windmillSlider3 / 255f)
            );
            Debug.Log("Cube eingefärbt mit Farbe: " + colorCube.GetComponent<Renderer>().material.color);
        }
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
                }
                else
                {
                    windmillSpeed.enabled = false;
                }
            }
        }
        Debug.Log("Windmühle " + cIndex + " ist jetzt aktiv");
    }

}