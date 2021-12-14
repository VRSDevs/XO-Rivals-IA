using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class ScriptHuella : MonoBehaviour
{

    public GameObject sigHuella;

    public GameObject renderX;
    public GameObject renderO;

    

    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
            renderX.SetActive(false);

        

        StartCoroutine(deleteHuella());


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator deleteHuella()
    {
        yield return new WaitForSeconds(15f);

        Destroy(this.gameObject);

    }

}
