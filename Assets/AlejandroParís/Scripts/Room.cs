using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlejandroParis
{
	public class Room /*: MonoBehaviour*/
	{
		public Vector2 gridPos;
		public Vector2 realGridPos;
		//public int type;
		public LevelGeneration lvlg;
		public Vector3 worldPosition;
		public bool end = false;
		public bool shop = false;
		public bool doorTop, doorBot, doorLeft, doorRight;
		
		public List<GameObject> SpawnPoints = new List<GameObject>();
		public Room(Vector2 _gridPos, LevelGeneration _lvlg, Vector3 _worldPosition) {
			gridPos = _gridPos;
			//type = _type;
			lvlg = _lvlg;
			worldPosition = _worldPosition;
		}
    }
}
