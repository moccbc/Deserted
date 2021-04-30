using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashBar : MonoBehaviour
{
    private Slider slider;
    public TrashSpawn spawnedTrash;

    public float numTrashPickedUp = 0.0f;
    public float trashBarValue = 0.0f;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        numTrashPickedUp = 1.0f;
        if (spawnedTrash.trashCount > 0)
        {
            trashBarValue = numTrashPickedUp / spawnedTrash.trashCount;
        }
        slider.value = trashBarValue;
    }

}
