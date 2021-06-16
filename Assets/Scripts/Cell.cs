using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool IsAlive = false;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite AliveSprite;
    [SerializeField] private Sprite DeadSprite;

    private List<Cell> neighbours = new List<Cell>();
    private int generationsAlive;
    private bool newState = false;

    public void CheckNeighbours()
    {
        int aliveNeighbours = 0;

        foreach(Cell neighbour in neighbours)
        {
            if(neighbour.IsAlive)
            {
                aliveNeighbours++;
            }
        }

        if(aliveNeighbours > 1 && aliveNeighbours < 4 && IsAlive) // Live on to the next generation
        {
            newState = true;
        }
        else if(aliveNeighbours == 3 && !IsAlive) // Reproduction
        {
            newState = true;
        }
        else // Dead by over/under population
        {
            newState = false;
        }
    }

    public void UpdateState()
    {
        IsAlive = newState;

        if(IsAlive)
        {
            spriteRenderer.sprite = AliveSprite;

            generationsAlive++;
            float alpha = generationsAlive * 0.1f;
            Mathf.Clamp(alpha, 0.1f, 1f);

            Color currentColor = spriteRenderer.color;
            currentColor.a = alpha;
            spriteRenderer.color = currentColor;
        }
        else
        {
            spriteRenderer.sprite = DeadSprite;
            generationsAlive = 0;

            Color currentColor = spriteRenderer.color;
            currentColor.a = 1f;
            spriteRenderer.color = currentColor;
        }
    }

    public void ForceState(bool state)
    {
        IsAlive = state;
    }

    public void ReceiveNeighbour(Cell neighbour)
    {
        neighbours.Add(neighbour);
    }
}
