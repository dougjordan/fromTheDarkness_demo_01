using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

    //public int sizeX, sizeZ;

    public MazeCell cellPrefab;

    private MazeCell[,] cells;

    public float generationStepDelay;

    public IntVector2 size;

    public MazeCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }

    public IEnumerator Generate()
    {
        // Fix for broken game manager item. Not picking up int from screen
        // =================================================================
        size.x = 20;
        size.z = 20;
        // =================================================================

        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];

        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            yield return delay;
            DoNextGenerationStep(activeCells);
        }

        //for (int x = 0; x < size.x; x++)
        //{
        //    for (int z = 0; z < size.z; z++)
        //    {
        //        yield return delay;
        //        CreateCell(new IntVector2(x, z));
        //    }
        //}
    }

    private MazeCell CreateCell(IntVector2 coordinates)
    {
        Debug.Log("Create cell x: " + coordinates.x.ToString());
        Debug.Log("Create cell z: " + coordinates.z.ToString());

        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition =
            new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);

        return newCell;
    }

    public IntVector2 RandomCoordinates
    {
        get
        {
            Debug.Log("RandomCoordinates");
            Debug.Log("Size X: " + size.x.ToString());
            Debug.Log("Size Y: " + size.z.ToString());

            int randX = Random.Range(0, size.x);
            int randY = Random.Range(0, size.z);
            Debug.Log("Rand Size X: " + randX.ToString());
            Debug.Log("Rand Size Y: " + randY.ToString());
            return new IntVector2(randX, randY);
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        bool containsCoordinates = false;

        if (coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z)
        {
            containsCoordinates = true;
        }

        Debug.Log(containsCoordinates.ToString());

        return containsCoordinates;
    }

    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        activeCells.Add(CreateCell(RandomCoordinates));
    }

    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        MazeDirection direction = MazeDirections.RandomValue;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (ContainsCoordinates(coordinates) && GetCell(coordinates) == null)
        {
            activeCells.Add(CreateCell(coordinates));
        }
        else
        {
            activeCells.RemoveAt(currentIndex);
        }
    }
}