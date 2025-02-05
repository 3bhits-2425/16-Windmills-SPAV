using UnityEngine;
using UnityEngine.UI;

public class WindmillLockSystem : MonoBehaviour
{
    [SerializeField] private Button lockButton;
    [SerializeField] private GameObject[] windmills;
    [SerializeField] private Slider[] windmillSliders;
    private bool[] isLocked;
    private int cIndex = 0;
    private float[] lockedSpeeds;

    private void Start()
    {
        isLocked = new bool[windmills.Length];
        lockedSpeeds = new float[windmills.Length];

        if (lockButton != null)
            lockButton.onClick.AddListener(LockCurrentWindmill);

        ActivateCurrentWindmill();
    }

    private void LockCurrentWindmill()
    {
        if (cIndex < windmills.Length && !isLocked[cIndex])
        {
            isLocked[cIndex] = true;

            WindmillDynamicSpeed windmillSpeed = windmills[cIndex].GetComponent<WindmillDynamicSpeed>();
            if (windmillSpeed != null)
            {
                lockedSpeeds[cIndex] = windmillSpeed.currentSpeed;
                windmillSpeed.enabled = false;
            }

            if (cIndex < windmills.Length - 1)
            {
                cIndex++;
                ActivateCurrentWindmill();
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < windmills.Length; i++)
        {
            WindmillDynamicSpeed windmillSpeed = windmills[i].GetComponent<WindmillDynamicSpeed>();
            if (windmillSpeed != null)
            {
                if (isLocked[i])
                {
                    windmills[i].transform.Rotate(Vector3.forward * lockedSpeeds[i] * Time.deltaTime);
                }
                else if (i == cIndex && !isLocked[i])
                {
                    windmillSpeed.enabled = true;
                }
            }
        }
    }

    private void ActivateCurrentWindmill()
    {
        for (int i = 0; i < windmills.Length; i++)
        {
            WindmillDynamicSpeed windmillSpeed = windmills[i].GetComponent<WindmillDynamicSpeed>();
            if (windmillSpeed != null)
            {
                windmillSpeed.enabled = (i == cIndex && !isLocked[i]);
            }
        }
    }
}
