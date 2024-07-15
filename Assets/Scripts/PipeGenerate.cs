using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class PipeGenerate : MonoBehaviour
{
    public GameObject pipePrefab;
    private float countdown;
    public float timeDuration;
    public bool enableGeneratePipe;
    private void Awake()
    {
        countdown = 1f;
        enableGeneratePipe = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(enableGeneratePipe){
            countdown -= Time.deltaTime;
            if(countdown <= 0){
                Instantiate(pipePrefab, new Vector3(4.3f, Random.Range(-3.5f, 0.5f), 0), Quaternion.identity);
                countdown = timeDuration;
            }
        }
    }
}
