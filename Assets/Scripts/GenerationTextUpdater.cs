using TMPro;
using UnityEngine;

public class GenerationTextUpdater : MonoBehaviour
{
    private TMP_Text genText;
    private GameTimer timer;

    private void Start()
    {
        genText = GetComponent<TMP_Text>();

        timer = GameTimer.Instance;
        timer.OnLateUpdateTick += UpdateText;
    }

    private void UpdateText()
    {
        genText.text = $"Gen: {timer.generation.ToString()}";
    }
}
