using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPlayer : MonoBehaviour
{


    public int level = 1;
    RaycastHit hit;

    public GameObject arme;
    [HideInInspector]
    
    //[HideInInspector]
    

    public static InventoryPlayer instance;

    public float interactionDistance;

   


    LayerMask cachette;
    Ray ray;

    //public bool isHidden = false;

    public bool isReading = false;

    public DisplayDossier displayInfo;

    public Transform actionRay;

    public bool discoverHidding =false;

    public bool isTouched =false;

    bool firstTrigger =false;

    bool onObjectTrigger = false;

    GameObject actualObject = null;

    // Start is called before the first frame update
    void Start()
    {
        
        //anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        instance = this;
        
    }

    private void Update()
    {
        if (GameManager._instance.gotKnife == false)
        {
            arme.SetActive(false);
        }
        else if (GameManager._instance.gotKnife == true)
        {
            arme.SetActive(true);
        }




        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ray = new Ray(actionRay.position, transform.forward);

        Debug.DrawRay(actionRay.position, transform.forward, Color.green);
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            Debug.Log("hit : " + hit.collider.gameObject.name);
            //si le tag de l'bojet touché avec le raycast est "casier"
            if (hit.collider.gameObject.CompareTag("casier"))
            {
                if (!GameManager._instance.isHidden)
                {

                    // Afficher UI Correspondante



                    //Si on appuie sur le clique gauche
                    if (Input.GetMouseButtonDown(0))
                    {
                        Cachette cachette = hit.collider.gameObject.GetComponent<Cachette>();
                        if(cachette)
                        GameManager._instance.HideToSpot(cachette.cam);
                        SoundManager.instance.Play("CasierOpen");
                    }
                }

            }

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {

                if (hit.collider.gameObject.CompareTag("Dossier"))
                {
                    // Afficher UI correspondante 




                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        //Debug.Log("Press E");
                        displayInfo = hit.transform.gameObject.GetComponent<DisplayDossier>();
                        UIManager._instance.NearToDossier();
                        Debug.Log("c'est un dossier");



                    }
                }
            }
        }

        if (onObjectTrigger == true)
        {
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (actualObject.GetComponent<Animator>().GetBool("Open"))
                {
                    actualObject.GetComponent<Animator>().SetBool("Open", false);
                    //SoundManager.instance.Play("Grincement");
                    actualObject.GetComponent<AudioSource>().Play();

                }
                else if (GameManager._instance.gotKey == true)
                {
                    Debug.Log("toctoc");
                    actualObject.GetComponent<AudioSource>().Play();
                    //SoundManager.instance.Play("TocToc");
                    actualObject.GetComponent<Animator>().SetBool("Open", true);

                }
                else
                {
                    //SoundManager.instance.Play("TocToc");
                    actualObject.GetComponent<GetSound>().Play("TocToc");
                }
            }

            if (actualObject.tag == "Porte" || actualObject.tag == "Porte2")
            {
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Ouverture porte");
                    //FindObjectOfType<SoundManager>().Play("TocToc");
                }
            }
            else if (actualObject.tag == "casier")
            {
                
                if (Input.GetKeyDown(KeyCode.H))
                {
                    Debug.Log("Ouverture casier");
                    SoundManager.instance.Play("CasierClose");
                }
            }

        }


    }

    //Fonction pour ouvrir une porte si le joueur a la clé
    /*public void CheckOpenDoor()
    {
        if (gotKey == true)
        {
            Debug.Log("Ouverture");
            PorteSystem.instance.OpenDoor();
        }
    }*/

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Porte")
        {
            Debug.Log("Appuie E");
           // UIManager.Instance.InteractionDossier();
        }
    }*/

        
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Porte")
        {
            onObjectTrigger = true;
            actualObject = other.gameObject;
            UIManager._instance.isDoor = true;
            UIManager._instance.InteractionTrigger();
            /*if (Input.GetKeyDown(KeyCode.E))
            {
                if (other.GetComponent<Animator>().GetBool("Open"))
                {
                    other.GetComponent<Animator>().SetBool("Open", false);
                    //SoundManager.instance.Play("Grincement");
                    other.GetComponent<GetSound>().Play("Grincement");

                }
                else if (GameManager._instance.gotKey == true)
                {
                    Debug.Log("toctoc");
                    other.GetComponent<GetSound>().Play("Grincement");
                    //SoundManager.instance.Play("TocToc");
                    other.GetComponent<Animator>().SetBool("Open", true);
                    
                }
                else
                {
                    //SoundManager.instance.Play("TocToc");
                    other.GetComponent<GetSound>().Play("TocToc");
                }
            }*/
        }
        else if(other.tag == "casier")
        {
            UIManager._instance.isCasier = true;
            UIManager._instance.InteractionTrigger();
            onObjectTrigger = true;
            actualObject = other.gameObject;
        }
        else if (other.tag == "Porte2")
        {
            UIManager._instance.isDoor = true;
            UIManager._instance.InteractionTrigger();
            /*if (Input.GetKeyDown(KeyCode.E))
            {

                if (other.GetComponent<Animator>().GetBool("Open"))
                {
                    other.GetComponent<Animator>().SetBool("Open", false);
                    //other.GetComponent<Animator>().Play("Close");
                }
                else if (GameManager._instance.gotKey == true)
                {
                    Debug.Log("toctoc");
                    //SoundManager.instance.Play("TocToc");
                    other.GetComponent<Animator>().SetBool("Open", true);
                }

            }*/
            onObjectTrigger = true;
            actualObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        onObjectTrigger = false;
        actualObject = null;
        if (other.tag == "Porte" || other.tag == "Porte2")
        {
            UIManager._instance.isDoor = false;
            UIManager._instance.panelPorte.SetActive(false);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Ouverture porte");
                //FindObjectOfType<SoundManager>().Play("TocToc");
            }
        }
        else if (other.tag == "casier")
        {
            UIManager._instance.isCasier = false;
            UIManager._instance.panelCasier.SetActive(false);
            if (Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log("Ouverture casier");
            }
        }
    }

    






}
