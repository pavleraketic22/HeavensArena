using UnityEngine;

public class TimeAbility : MonoBehaviour, IAbility
{
    public float slowDuration = 5f;
    public float slowFactor = 0.5f;

    public void Activate(GameObject user)
    {
        Debug.Log($"{user.name} activated TIME ability!");
        Time.timeScale = slowFactor;
        user.GetComponent<MonoBehaviour>().StartCoroutine(RestoreTimeAfterDelay());
    }

    private System.Collections.IEnumerator RestoreTimeAfterDelay()
    {
        yield return new WaitForSecondsRealtime(slowDuration);
        Time.timeScale = 1f;
    }
}