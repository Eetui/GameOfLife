using UnityEngine;
using TMPro;

public class TickTextUpdater : MonoBehaviour
{
    private TMP_Text tickText;
    private GameTimer gameTimer;

    private void Start()
    {
        tickText = GetComponent<TMP_Text>();

        gameTimer = GameTimer.Instance;
        gameTimer.OnTickRateChanged += UpdateText;
    }

    private void UpdateText()
    {
        tickText.text = $"Tick rate: {1/gameTimer.tickInterval}/s";
    }
}
