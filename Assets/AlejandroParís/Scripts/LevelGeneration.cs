using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AlejandroParis
{
	public class LevelGeneration : MonoBehaviour
	{
		Room[,] rooms;
		int gridSizeY = 20;
		int gridSizeX = 20;
		public float gridsizeX = 28.78f;
		public float gridsizeY = 17.95f;
		public int numberRooms = 10;
		int roomnumber = 0;
		bool end = false;

		List<Room> tempendRooms = new List<Room>();
		List<Room> tempShopRooms = new List<Room>();
		List<GameObject> allRooms = new List<GameObject>();

		public GameObject grid;
		List<Vector2> takenPositions = new List<Vector2>();
		public GameObject[] roomsPrefabs;
		public GameObject[] endRooms;
        // Start is called before the first frame update
        void Start()
        {
			CreateRooms();
			CheckSides();
			CheckTypes();
			DrawAll();
			BakeAll();
			//rooms[gridSize / 2, gridSize / 2] = new Room(new Vector2(gridSize / 2, gridSize / 2), this, new Vector3(0, 0, 0));
		}

        // Update is called once per frame
        void Update()
        {
			
        }
		void DrawAll()
        {
			for (int x = 0; x < gridSizeX * 2; x++)
			{
				for (int y = 0; y < gridSizeY * 2; y++)
				{
					if (rooms[x, y] != null)
					{
						DrawMap(rooms[x, y]);
					}
				}
			}
		}
		
		void CreateRooms()
		{
			Vector3 worldPos = new Vector3(0, 0, 0);
			//setup
			rooms = new Room[gridSizeX * 2, gridSizeY * 2];
			rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, this,worldPos);
			takenPositions.Insert(0, Vector2.zero);
			Vector2 checkPos = Vector2.zero;
			//magic numbers
			float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;
			//add rooms
			for (int i = 0; i < numberRooms - 1; i++)
			{
				float randomPerc = ((float)i) / (((float)numberRooms - 1));
				randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
				//grab new position
				checkPos = NewPosition();
				//test new position
				if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare)
				{
					int iterations = 0;
					do
					{
						checkPos = SelectiveNewPosition();
						iterations++;
					} while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
					if (iterations >= 50)
						print("error: could not create with fewer neighbors than : " + NumberOfNeighbors(checkPos, takenPositions));
				}
				//finalize position
				worldPos = new Vector3((int)checkPos.x * gridsizeX, 0, (int)checkPos.y * gridsizeY);
				rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, this,worldPos);
				rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY]. realGridPos = new Vector2((int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY);
				takenPositions.Insert(0, checkPos);
			}
		}
		Vector2 NewPosition()
		{
			int x = 0, y = 0;
			Vector2 checkingPos = Vector2.zero;
			do
			{
				int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1)); // pick a random room
				x = (int)takenPositions[index].x;//capture its x, y position
				y = (int)takenPositions[index].y;
				bool UpDown = (Random.value < 0.5f);//randomly pick wether to look on hor or vert axis
				bool positive = (Random.value < 0.5f);//pick whether to be positive or negative on that axis
				if (UpDown)
				{ //find the position bnased on the above bools
					if (positive)
					{
						y += 1;
					}
					else
					{
						y -= 1;
					}
				}
				else
				{
					if (positive)
					{
						x += 1;
					}
					else
					{
						x -= 1;
					}
				}
				checkingPos = new Vector2(x, y);
			} while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY); //make sure the position is valid
			return checkingPos;
		}
		Vector2 SelectiveNewPosition()
		{ // method differs from the above in the two commented ways
			int index = 0, inc = 0;
			int x = 0, y = 0;
			Vector2 checkingPos = Vector2.zero;
			do
			{
				inc = 0;
				do
				{
					//instead of getting a room to find an adject empty space, we start with one that only 
					//as one neighbor. This will make it more likely that it returns a room that branches out
					index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
					inc++;
				} while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);
				x = (int)takenPositions[index].x;
				y = (int)takenPositions[index].y;
				bool UpDown = (Random.value < 0.5f);
				bool positive = (Random.value < 0.5f);
				if (UpDown)
				{
					if (positive)
					{
						y += 1;
					}
					else
					{
						y -= 1;
					}
				}
				else
				{
					if (positive)
					{
						x += 1;
					}
					else
					{
						x -= 1;
					}
				}
				checkingPos = new Vector2(x, y);
			} while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
			if (inc >= 100)
			{ // break loop if it takes too long: this loop isnt garuanteed to find solution, which is fine for this
				print("Error: could not find position with only one neighbor");
			}
			return checkingPos;
		}
		int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
		{
			int ret = 0; // start at zero, add 1 for each side there is already a room
			if (usedPositions.Contains(checkingPos + Vector2.right))
			{ //using Vector.[direction] as short hands, for simplicity
				ret++;
			}
			if (usedPositions.Contains(checkingPos + Vector2.left))
			{
				ret++;
			}
			if (usedPositions.Contains(checkingPos + Vector2.up))
			{
				ret++;
			}
			if (usedPositions.Contains(checkingPos + Vector2.down))
			{
				ret++;
			}
			return ret;
		}
		void CheckSides()
        {
			for (int x = 0; x < ((gridSizeX * 2)); x++)
			{
				for (int y = 0; y < ((gridSizeY * 2)); y++)
				{
					if (rooms[x, y] == null)
					{
						continue;
					}
					Vector2 gridPosition = new Vector2(x, y);
					if (y - 1 < 0)
					{ //check above
						rooms[x, y].doorBot = false;
					}
					else
					{
						rooms[x, y].doorBot = (rooms[x, y - 1] != null);
					}
					if (y + 1 >= gridSizeY * 2)
					{ //check bellow
						rooms[x, y].doorTop = false;
					}
					else
					{
						rooms[x, y].doorTop = (rooms[x, y + 1] != null);
					}
					if (x - 1 < 0)
					{ //check left
						rooms[x, y].doorLeft = false;
					}
					else
					{
						rooms[x, y].doorLeft = (rooms[x - 1, y] != null);
					}
					if (x + 1 >= gridSizeX * 2)
					{ //check right
						rooms[x, y].doorRight = false;
					}
					else
					{
						rooms[x, y].doorRight = (rooms[x + 1, y] != null);
					}
					CheckEnd(x, y);
				}
			}
		}

		void CheckEnd(int x, int y)
        {
			Room r = rooms[x, y];
			if (!r.doorTop)
			{
				if (!r.doorBot)
				{
					if (!r.doorRight)
					{
						if (r.doorLeft)
						{
							tempendRooms.Add(r);
						}
					}
					else if (!r.doorLeft)
					{
						tempendRooms.Add(r);
					}
				}
				else if (!r.doorLeft && !r.doorRight)
				{
					tempendRooms.Add(r);
				}
			}
			else if (!r.doorLeft && !r.doorRight && !r.doorBot)
			{
				tempendRooms.Add(r);
			}
		}
		void CheckTypes()
		{
			for (int x = 0; x < tempendRooms.Count; x++)
			{
				float temprand = Random.Range(0, 10);
				if (!end)
				{
					if (temprand < 2 || roomnumber >= tempendRooms.Count-1)
					{
						rooms[(int)tempendRooms[x].realGridPos.x, (int)tempendRooms[x].realGridPos.y].end = true;
						end = true;
					}
					roomnumber++;
				}
			}
		}

		public void DrawMap(Room r)
		{
			GameObject temp = null;
			if (r.doorTop)
			{
				if (r.doorBot)
				{
					if (r.doorRight)
					{
						if (r.doorLeft)
						{
							temp = Instantiate(roomsPrefabs[0], r.worldPosition, Quaternion.identity);
							temp.transform.parent = grid.transform;
						}
						else
						{
							temp = Instantiate(roomsPrefabs[14], r.worldPosition, Quaternion.identity);
							temp.transform.parent = grid.transform;
							//rend.sprite = spDRU;
						}
					}
					else if (r.doorLeft)
					{
						temp = Instantiate(roomsPrefabs[13], r.worldPosition, Quaternion.identity);
						temp.transform.parent = grid.transform;
						//rend.sprite = spULD;
					}
					else
					{
						temp = Instantiate(roomsPrefabs[12], r.worldPosition, Quaternion.identity);
						temp.transform.parent = grid.transform;
						//rend.sprite = spUD;
					}
				}
				else
				{
					if (r.doorRight)
					{
						if (r.doorLeft)
						{
							temp = Instantiate(roomsPrefabs[9], r.worldPosition, Quaternion.identity);
							temp.transform.parent = grid.transform;
							//rend.sprite = spRUL;
						}
						else
						{
							temp = Instantiate(roomsPrefabs[10], r.worldPosition, Quaternion.identity);
							temp.transform.parent = grid.transform;
							//rend.sprite = spUR;
						}
					}
					else if (r.doorLeft)
					{
						temp = Instantiate(roomsPrefabs[4], r.worldPosition, Quaternion.identity);
						temp.transform.parent = grid.transform;
						//rend.sprite = spUL;
					}
					else
					{
						if (r.end)
						{
							temp = Instantiate(endRooms[3], r.worldPosition, Quaternion.identity);
							temp.transform.parent = grid.transform;
						}
						else
						{
							temp = Instantiate(roomsPrefabs[11], r.worldPosition, Quaternion.identity);
							temp.transform.parent = grid.transform;
						}
						//rend.sprite = spU;
					}
				}
				allRooms.Add(temp);
				return;
			}
			if (r.doorBot)
			{
				if (r.doorRight)
				{
					if (r.doorLeft)
					{
						temp = Instantiate(roomsPrefabs[8], r.worldPosition, Quaternion.identity);
						temp.transform.parent = grid.transform;
						//rend.sprite = spLDR;
					}
					else
					{
						temp = Instantiate(roomsPrefabs[6], r.worldPosition, Quaternion.identity);
						temp.transform.parent = grid.transform;
						//rend.sprite = spDR;
					}
				}
				else if (r.doorLeft)
				{
					temp = Instantiate(roomsPrefabs[3], r.worldPosition, Quaternion.identity);
					temp.transform.parent = grid.transform;
					//rend.sprite = spDL;
				}
				else
				{
					if(r.end)
                    {
						temp = Instantiate(endRooms[0], r.worldPosition, Quaternion.identity);
						temp.transform.parent = grid.transform;
					}        
                    else
                    {
						temp = Instantiate(roomsPrefabs[1], r.worldPosition, Quaternion.identity);
						temp.transform.parent = grid.transform;
					}

					//rend.sprite = spD;
				}
				allRooms.Add(temp);
				return;
			}
			if (r.doorRight)
			{
				if (r.doorLeft)
				{
					temp = Instantiate(roomsPrefabs[7], r.worldPosition, Quaternion.identity);
					temp.transform.parent = grid.transform;
					//rend.sprite = spRL;
				}
				else
				{
						if (r.end)
						{
							temp = Instantiate(endRooms[2], r.worldPosition, Quaternion.identity);
							temp.transform.parent = grid.transform;					
						}
						else
						{
							temp = Instantiate(roomsPrefabs[5], r.worldPosition, Quaternion.identity);
							temp.transform.parent = grid.transform;
						}
					//rend.sprite = spR;
				}
			}
			else
			{
				if (r.end)
				{
					temp = Instantiate(endRooms[1], r.worldPosition, Quaternion.identity);
					temp.transform.parent = grid.transform;
				}
				else
                {
					temp = Instantiate(roomsPrefabs[2], r.worldPosition, Quaternion.identity);
					temp.transform.parent = grid.transform;
				}
				allRooms.Add(temp);
			}
		}
		void BakeAll()
		{
			for (int x = 0; x < allRooms.Count; x++)
			{
				allRooms[x].GetComponent<NavMeshSurface>().BuildNavMesh();
			}
		}
	}
}