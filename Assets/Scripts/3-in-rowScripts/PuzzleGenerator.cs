using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGenerator : MonoBehaviour
{
    public Texture[] elements;
    [SerializeField] int totalColumns = 9;
    [SerializeField] int totalRows = 9;
    public UnityEngine.UI.Slider scoreSlider;
    public float sliderValue = 100f;

    [System.Serializable]
    public class PuzzleElement
    {
        public Texture texture;
        public Vector3 position;
    }
    private bool isCheckingCombos = false;

    List<List<PuzzleElement>> columns = new List<List<PuzzleElement>>();

    int selectedColumn = -1;
    int selectedRow = -1;
    int score;
    [SerializeField] float zOffset = 0f;
    void Start()
    {
        Time.timeScale = 1; //Треба щоб працював час при рестарті
        for(int x = 0; x < totalColumns; x++)
        {
            List<PuzzleElement> column = new List<PuzzleElement>();
            for (int y = 0; y < totalRows; y++)
            {
                column.Add(new PuzzleElement());
            }
            columns.Add(column);
        }
        scoreSlider.value = sliderValue; 
        StartCoroutine(RestockEnumrator());
        StartCoroutine(DecreaseSliderOverTime());
    }

    IEnumerator DecreaseSliderOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            sliderValue -= 2f; 
            sliderValue = Mathf.Clamp(sliderValue, 0, 100);
            scoreSlider.value = sliderValue;
       }
    }

    void OnGUI()
    {

        for (int x = 0; x < columns.Count; x++)
        {
            for (int y = 0; y < columns[x].Count; y++)
            {
                if (columns[x][y].texture)
                    {
                    columns[x][y].position = Vector3.Lerp(new Vector3(columns[x][y].position.x, columns[x][y].position.y, zOffset),new Vector3(
                    (Screen.width / 2 - (columns.Count * 64) / 2) + x * 64,
                    (Screen.height / 2 - (columns[x].Count * 64) / 2) + y * 64 + 30,zOffset),Time.deltaTime * 7);
                    Rect elementRect = new Rect(columns[x][y].position.x, columns[x][y].position.y, 64, 64);
                    if ((x == selectedColumn && (y == selectedRow - 1 || y == selectedRow + 1)) || ((x == selectedColumn - 1 || x == selectedColumn + 1) && y == selectedRow))
                    {
                        if (GUI.Button(elementRect, columns[x][y].texture))
                        {
                            PuzzleElement tmpElement = columns[x][y];
                            columns[x][y] = columns[selectedColumn][selectedRow];
                            columns[selectedColumn][selectedRow] = tmpElement;
                            selectedColumn = -1;
                            selectedRow = -1;
                            StopCoroutine(DetectCombos());
                            StartCoroutine(DetectCombos());
                        }
                    }
                    else
                    {
                        if (elementRect.Contains(Event.current.mousePosition))
                        {
                            GUI.enabled = false;
                            if (Input.GetMouseButtonDown(0))
                            {
                                selectedColumn = x;
                                selectedRow = y;
                            }
                        }
                        if (x == selectedColumn && y == selectedRow)
                        {
                            GUI.enabled = false;
                        }
                        GUI.Box(elementRect, columns[x][y].texture);
                    }

                    GUI.enabled = true;
                }
            }
        }
    }

    IEnumerator CompressElements()
    {
        bool compressionNeeded = false;
        for (int x = 0; x < columns.Count; x++)
        {
            for (int y = 1; y < columns[x].Count; y++)
            {
                if(!columns[x][y].texture && columns[x][y - 1].texture)
                {
                    compressionNeeded = true;
                }
            }
        }

        if (compressionNeeded)
        {
            yield return new WaitForSeconds(0.25f);

            for (int x = 0; x < columns.Count; x++)
            {
                int referenceIndex = -1;
                for (int y = columns[x].Count - 1; y >= 0; y--)
                {
                    if (!columns[x][y].texture)
                    {
                        if (referenceIndex == -1)
                        {
                            referenceIndex = y;
                        }
                    }
                    else
                    {
                        if (referenceIndex != -1)
                        {
                            columns[x][referenceIndex].texture = columns[x][y].texture;
                            columns[x][referenceIndex].position = columns[x][y].position;
                            columns[x][y].texture = null;
                            referenceIndex--;
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

        for (int x = 0; x < columns.Count; x++)
        {
            for (int y = 0; y < columns[x].Count; y++)
            {
                if (!columns[x][y].texture)
                {
                    int randomElement = Random.Range(0, (elements.Length - 1) * 2);
                    if (randomElement > elements.Length - 1)
                    {
                        randomElement -= elements.Length - 1;
                    }
                    PuzzleElement element = new PuzzleElement();
                    element.texture = elements[randomElement];
                    element.position = new Vector3((Screen.width / 2 - (totalColumns * 64) / 2) + x * 64,(-Screen.height - (totalRows * 64) / 2) + y * 64,zOffset);
                    columns[x][y] = element;
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
        List<List<int>> combinedLines = new List<List<int>>();
        bool combosDetected = false;

        for (int x = 0; x < columns.Count; x++)
        {
            combinedLines.Add(new List<int>());
            List<int> line = new List<int>();
            for (int y = 0; y < columns[x].Count; y++)
            {
                if(line.Count == 0)
                {
                    line.Add(y);
                }
                else
                {
                    if(columns[x][line[0]].texture == columns[x][y].texture)
                    {
                        line.Add(y);
                    }
                    if (columns[x][line[0]].texture != columns[x][y].texture || y == columns[x].Count - 1)
                    {
                        if(line.Count >= 3)
                        {
                            combinedLines[x].AddRange(line);
                        }
                        line.Clear();
                        line.Add(y);
                    }
                }
            }
        }

        for (int x = 0; x < combinedLines.Count; x++)
        {
            for (int y = 0; y < combinedLines[x].Count; y++)
            {
                columns[x][combinedLines[x][y]].texture = null;
                sliderValue += 5f;
                sliderValue = Mathf.Clamp(sliderValue, 0, 100);
                scoreSlider.value = sliderValue;
                combosDetected = true;
            }
        }
        combinedLines = new List<List<int>>();
        for (int y = 0; y < columns[0].Count; y++)
        {
            combinedLines.Add(new List<int>());
            List<int> line = new List<int>();
            for (int x = 0; x < columns.Count; x++)
            {
                if (line.Count == 0)
                {
                    line.Add(x);
                }
                else
                {
                    if (columns[line[0]][y].texture == columns[x][y].texture)
                    {
                        line.Add(x);
                    }
                    if (columns[line[0]][y].texture != columns[x][y].texture || x == columns.Count - 1)
                    {
                        if (line.Count >= 3)
                        {
                            combinedLines[y].AddRange(line);
                        }
                        line.Clear();
                        line.Add(x);
                    }
                }
            }
        }

        for (int x = 0; x < combinedLines.Count; x++)
        {
            for (int y = 0; y < combinedLines[x].Count; y++)
            {
                columns[combinedLines[x][y]][x].texture = null;

                sliderValue += 5f;
                sliderValue = Mathf.Clamp(sliderValue, 0, 100);
                scoreSlider.value = sliderValue;
                combosDetected = true;
            }
        }

        if (combosDetected)
        {
            StartCoroutine(CompressElements());
        }
        isCheckingCombos = false;
    }
}