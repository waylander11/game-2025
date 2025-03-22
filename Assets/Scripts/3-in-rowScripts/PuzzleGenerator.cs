using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGenerator : MonoBehaviour
{
    public Texture[] elements;
    public int totalColumns = 9;
    public int totalRows = 9;
    public Slider scoreSlider;
    private float sliderValue = 100f;
    private bool isCheckingCombos = false;

    [System.Serializable]
    public class PuzzleElement
    {
        public Texture texture;
        public Vector2 position;
    }
    
    private List<List<PuzzleElement>> columns = new List<List<PuzzleElement>>();
    private int selectedColumn = -1, selectedRow = -1;

    void Start()
    {
        InitializeGrid();
        StartCoroutine(RestockEnumrator());
        StartCoroutine(DecreaseSliderOverTime());
    }
    void Update()
    {
    
    }

    void InitializeGrid()
    {
        for (int x = 0; x < totalColumns; x++)
        {
            List<PuzzleElement> column = new List<PuzzleElement>();
            for (int y = 0; y < totalRows; y++)
            {
                column.Add(new PuzzleElement());
            }
            columns.Add(column);
        }
    }

    IEnumerator DecreaseSliderOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            sliderValue = Mathf.Clamp(sliderValue, 0, 100);
            scoreSlider.value = sliderValue - 1f;
            scoreSlider.value = Mathf.Lerp(scoreSlider.value, sliderValue, Time.deltaTime * 5);
        }
    }

    void OnGUI()
    {
        for (int x = 0; x < columns.Count; x++)
        {
            for (int y = 0; y < columns[x].Count; y++)
            {
                if (columns[x][y].texture != null)
                {
                    Vector2 targetPos = new Vector2((Screen.width / 2 - (columns.Count * 64) / 2) + x * 64, (Screen.height / 2 - (columns[x].Count * 64) / 2) + y * 64 + 30);
                    columns[x][y].position = Vector2.Lerp(columns[x][y].position, targetPos, Time.deltaTime * 7);
                    Rect elementRect = new Rect(columns[x][y].position.x, columns[x][y].position.y, 64, 64);

                    if ((x == selectedColumn && (y == selectedRow - 1 || y == selectedRow + 1)) ||
                        ((x == selectedColumn - 1 || x == selectedColumn + 1) && y == selectedRow))
                    {
                        if (GUI.Button(elementRect, columns[x][y].texture))
                        {
                            SwapElements(x, y);
                        }
                    }
                    else
                    {
                        if (elementRect.Contains(Event.current.mousePosition) && Input.GetMouseButtonDown(0))
                        {
                            selectedColumn = x;
                            selectedRow = y;
                        }
                        GUI.Box(elementRect, columns[x][y].texture);
                    }
                }
            }
        }
    }

    void SwapElements(int x, int y)
    {
        PuzzleElement temp = columns[x][y];
        columns[x][y] = columns[selectedColumn][selectedRow];
        columns[selectedColumn][selectedRow] = temp;
        selectedColumn = -1;
        selectedRow = -1;
        StartCoroutine(DetectCombos());
    }

    IEnumerator CompressElements()
    {
        yield return new WaitForSeconds(0.25f);
        foreach (var column in columns)
        {
            for (int y = column.Count - 1; y >= 0; y--)
            {
                if (column[y].texture == null)
                {
                    for (int above = y - 1; above >= 0; above--)
                    {
                        if (column[above].texture != null)
                        {
                            column[y].texture = column[above].texture;
                            column[above].texture = null;
                            break;
                        }
                    }
                }
            }
        }
        StartCoroutine(RestockEnumrator());
    }

    IEnumerator RestockEnumrator()
    {
        yield return new WaitForSeconds(0.25f);
        foreach (var column in columns)
        {
            for (int y = 0; y < column.Count; y++)
            {
                if (column[y].texture == null)
                {
                    column[y].texture = elements[Random.Range(0, elements.Length)];
                }
            }
        }
        StartCoroutine(DetectCombos());
    }

    IEnumerator DetectCombos()
    {
        if (isCheckingCombos) yield break;
        isCheckingCombos = true;
        yield return new WaitForSeconds(0.25f);
        
        bool combosDetected = false;
        HashSet<Vector2Int> toRemove = new HashSet<Vector2Int>();
        
        for (int x = 0; x < totalColumns; x++)
        {
            for (int y = 0; y < totalRows - 2; y++)
            {
                if (columns[x][y].texture != null &&
                    columns[x][y].texture == columns[x][y + 1].texture &&
                    columns[x][y].texture == columns[x][y + 2].texture)
                {
                    toRemove.Add(new Vector2Int(x, y));
                    toRemove.Add(new Vector2Int(x, y + 1));
                    toRemove.Add(new Vector2Int(x, y + 2));
                }
            }
        }
        
        for (int y = 0; y < totalRows; y++)
        {
            for (int x = 0; x < totalColumns - 2; x++)
            {
                if (columns[x][y].texture != null &&
                    columns[x][y].texture == columns[x + 1][y].texture &&
                    columns[x][y].texture == columns[x + 2][y].texture)
                {
                    toRemove.Add(new Vector2Int(x, y));
                    toRemove.Add(new Vector2Int(x + 1, y));
                    toRemove.Add(new Vector2Int(x + 2, y));
                }
            }
        }
        
        foreach (var pos in toRemove)
        {
            columns[pos.x][pos.y].texture = null;
            sliderValue = Mathf.Clamp(sliderValue + 5f, 0, 100);
        }
        
        if (toRemove.Count > 0)
        {
            StartCoroutine(CompressElements());
            combosDetected = true;
        }
        
        isCheckingCombos = false;
    }
}
