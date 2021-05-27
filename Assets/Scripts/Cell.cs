using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public List<Cell> neighbours = new List<Cell>();
    public bool Alive { get; private set; }

    [SerializeField] private int neighboursAlive;
    private SpriteRenderer sprite;

    public bool randomStartState;
    public int alivePercent;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (randomStartState)
        {
            Alive = Random.Range(0, 100) < alivePercent ? true : false;
        }

        sprite.enabled = Alive;
        CalculateAliveNeighbors();
        GameTimer.Instance.OnUpdateTick += CalculateAliveNeighbors;
        GameTimer.Instance.OnLateUpdateTick += CheckNeighbours;
    }

    private void CheckNeighbours()
    {
        if (Alive && neighboursAlive >= 2 && neighboursAlive <= 3)
        {
            Alive = true;

        }
        else if (!Alive && neighboursAlive == 3)
        {
            Alive = true;
        }
        else
        {
            Alive = false;
        }

        sprite.enabled = Alive;
    }

    private void CalculateAliveNeighbors()
    {
        neighboursAlive = 0;

        foreach (var neighbour in neighbours)
        {
            if (neighbour.Alive)
            {
                neighboursAlive++;
            }
        }
    }

    private void OnMouseDown()
    {
        Alive = !Alive;
        sprite.enabled = Alive;
    }
}
