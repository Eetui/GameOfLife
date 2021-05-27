using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance { get; private set; }

    public long generation = 0;

    public delegate void OnUpdateTickDelegate();
    public OnUpdateTickDelegate OnUpdateTick;

    public delegate void OnLateUpdateTickDelegate();
    public OnLateUpdateTickDelegate OnLateUpdateTick;

    public delegate void OnTickRateChangedDelegate();
    public OnTickRateChangedDelegate OnTickRateChanged;

    public float tickInterval;
    private float timer;
    private bool pause = true;

    private void Awake() => Instance = this;

    private void Update()
    {
        if(!pause)
        {
            timer += Time.deltaTime;

            if (timer > tickInterval)
            {
                generation++;

                OnUpdateTick?.Invoke();
                OnLateUpdateTick?.Invoke();
                timer = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            tickInterval += 0.01f;
            OnTickRateChanged?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(tickInterval > 0.02f) tickInterval -= 0.01f;
            OnTickRateChanged?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
            OnTickRateChanged?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}
