using System.Collections;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject meteorPF;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private float offset = 7f;
    private float minX, maxX;
    private bool isSpawning = false;
    private Camera camera;
    private void Start()
    {
        camera = Camera.main;
        minX = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + offset;
        maxX = camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - offset;
    }
    private void Update()
    {
        if (!isSpawning)
            StartCoroutine(SpawnRandomMeteor());
    }
    private IEnumerator SpawnRandomMeteor()
    {
        isSpawning = true;
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), transform.position.y);
        yield return new WaitForSeconds(timeBetweenSpawns);
        isSpawning = false;
        GameObject meteor = ObjectPooler.instance.GetPooledObject("Meteor");
        if(meteor != null)
        {
            meteor.transform.position = randomPosition;
            meteor.transform.rotation = Quaternion.identity;
            meteor.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(2 * maxX, 1, 1));
    }
}
