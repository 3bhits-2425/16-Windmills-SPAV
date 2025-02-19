using UnityEngine;
using UnityEngine.UI;

public class WindmillLockSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] windmills;
    [SerializeField] private Slider[] windmillSliders;
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
            else
            {
                Debug.Log("Windmühle " + cIndex + " kann nicht gelockt werden, da Geschwindigkeit 0 ist.");
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
        if (colorCube != null)
        {
            colorCube.GetComponent<Renderer>().material.color = new Color(
                Mathf.Clamp01(windmillSliders[0].value / 255f),
                Mathf.Clamp01(windmillSliders[1].value / 255f),
                Mathf.Clamp01(windmillSliders[2].value / 255f)
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
