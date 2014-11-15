using UnityEngine;
using System.Collections;
using Jolly;

public class PlaceCollectablesOnTerrain : MonoBehaviour
{
	public GameObject[] CollectablesToPlace;
	public int[] SpawnCounts;
	public GameObject ParentGameObject;
	public float SpawnRadius;
	public float HeightAboveGround;

	void Start ()
	{
		Terrain terrain = this.GetComponent<Terrain>();

		for (int x = 0; x < this.CollectablesToPlace.Length; x++) {
			GameObject CollectableToPlace = this.CollectablesToPlace[x];
			int SpawnCount = this.SpawnCounts[x];
			for (int i = 0; i < SpawnCount; ++i) {
				Vector2 location = Random.insideUnitCircle * this.SpawnRadius;
				Vector3 worldPosition = new Vector3 (location.x, 0.0f, location.y);
				float y = terrain.SampleHeight (worldPosition) + this.HeightAboveGround;
				worldPosition = worldPosition.SetY (y);

				GameObject collectableObject = (GameObject)GameObject.Instantiate (CollectableToPlace, worldPosition, CollectableToPlace.transform.rotation);
				collectableObject.isStatic = true;
				collectableObject.transform.parent = this.ParentGameObject.transform;
			}
		}
	}
}
