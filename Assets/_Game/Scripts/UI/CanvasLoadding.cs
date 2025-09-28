using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasLoadding : UICanvas
{
    [SerializeField] private Slider fillImage;
    [SerializeField] private float totalDuration = 1.5f; 
    [SerializeField] private float delayBetweenPhases = 0.2f; 
    [SerializeField] private TextMeshProUGUI text;

    private Coroutine runningCoroutine;

    private void OnEnable()
    {
        StartFill();
    }

    public void StartFill()
    {
        if (runningCoroutine != null) StopCoroutine(runningCoroutine);
        runningCoroutine = StartCoroutine(FillRoutine());
    }

    private IEnumerator FillRoutine()
    {
        fillImage.value = 0f;
        text.text = "0%";

        float firstPhaseDuration = totalDuration * 0.75f;
        float secondPhaseDuration = totalDuration * 0.25f; 
        float elapsed = 0f;
        while (elapsed < firstPhaseDuration)
        {
            elapsed += Time.deltaTime;
            float percent = Mathf.Lerp(0f, 0.75f, elapsed / firstPhaseDuration);
            fillImage.value = percent;
            text.text = Mathf.RoundToInt(percent * 100f) + "%";
            yield return null;
        }
        yield return new WaitForSeconds(delayBetweenPhases);

        elapsed = 0f;
        while (elapsed < secondPhaseDuration)
        {
            elapsed += Time.deltaTime;
            float percent = Mathf.Lerp(0.75f, 1f, elapsed / secondPhaseDuration);
            fillImage.value = percent;
            text.text = Mathf.RoundToInt(percent * 100f) + "%";
            yield return null;
        }

        fillImage.value = 1f;
        text.text = "100%";

        SoundManager.Instance.PlayIngameMusic();
        Close(0f);
    }
}
