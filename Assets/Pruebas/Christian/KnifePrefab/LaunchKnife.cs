using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LaunchKnife : MonoBehaviour
{
    public GameObject Knife;
    public Transform spawnKnife;

    public float costLaunchKnife = 60.0f;

    public float forceKnife = 1000f;

    public bool canLaunchKnife = true;
    
    public float lifeKnife;

    [SerializeField] private StaminaSistem staminaSistem; //Referencia al sistema de estamina

    private void Start()
    {
        //staminaSistem = GetComponent<StaminaSistem>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) LauncherKnife();
    }
    public void LauncherKnife()
    {
        if (staminaSistem.playerActualStamine >= costLaunchKnife)
        {
            GameObject newKinife;
            newKinife = Instantiate(Knife, spawnKnife.position, spawnKnife.rotation);
            newKinife.GetComponent<Rigidbody>().AddForce(spawnKnife.forward * forceKnife);
            staminaSistem.SubstractStamina(costLaunchKnife);
            Destroy(newKinife, lifeKnife);
        }
    }
    }
