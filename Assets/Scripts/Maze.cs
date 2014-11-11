using UnityEngine;
using System.Collections;

public class Maze : MonoBehaviour {

    // Based on http://catlikecoding.com/unity/tutorials/maze/

    public IntVector2 size;

    //public int sizeX, sizeZ;

    public MazeCell cellPrefab;

    private MazeCell[,] cells;

    public float generationStepDelay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public MazeCell GetCell(IntVector2 coordinates) {
        return cells[coordinates.x, coordinates.z];
    }

    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];

        IntVector2 coordinates = RandomCoordinates;
        while (ContainsCoordinates(coordinates) && GetCell(coordinates) == null)
        {
            yield return delay;
            CreateCell(coordinates);
            coordinates += MazeDirections.RandomValue.ToIntVector2();
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

    private void CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
    }

    public IntVector2 RandomCoordinates {
        get { 
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinates) {
        return coordinates.x >= 0 && coordinates.x < size.x && coordinates.z >= 0 && coordinates.z < size.z;
    }

}
