using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Spawn : MonoBehaviour
{
    [SerializeField]
    private Vector3 player1Spawn;
    [SerializeField]
    private Vector3 player2Spawn;
    [SerializeField]
    private Vector3 player3Spawn;
    [SerializeField]
    private Vector3 player4Spawn;

    [SerializeField]
    public GameObject player1Prefab;
    [SerializeField]
    private GameObject player2Prefab;
    [SerializeField]
    private GameObject player3Prefab;
    [SerializeField]
    private GameObject player4Prefab;

    IEnumerator spawnPlayers() {
        player1Prefab.transform.position = player1Spawn;
        player2Prefab.transform.position = player2Spawn;
        player3Prefab.transform.position = player3Spawn;
        player4Prefab.transform.position = player4Spawn;
        var player1 = PlayerInput.Instantiate(player1Prefab, controlScheme: "Player1", pairWithDevice: Keyboard.current);
        yield return new WaitForSeconds(1);
        var player2 = PlayerInput.Instantiate(player2Prefab, controlScheme: "Player2", pairWithDevice: Keyboard.current);
        yield return new WaitForSeconds(1);
        var player3 = PlayerInput.Instantiate(player3Prefab, controlScheme: "Player3", pairWithDevice: Keyboard.current);
        yield return new WaitForSeconds(1);
        var player4 = PlayerInput.Instantiate(player4Prefab, controlScheme: "Player4", pairWithDevice: Keyboard.current);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnPlayers());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
