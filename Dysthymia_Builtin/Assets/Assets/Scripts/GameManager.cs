using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class PatrolRoute
{
    public string routeName;
    public Transform[] waypoints;
}

public class GameManager : MonoBehaviour
{
    public EnemyAI ai;
    SoundManager sm;
    public Transform[] waypoints1;
    public PatrolRoute[] patrolRoutes;
    public static GameManager _instance;
    public bool DoorGardienClose = false;
    public bool ennemyInZone;

    [HideInInspector]
    public bool isHidden = false;

    //public bool firstTrigger = false;
    //  On stock le player
    public GameObject player;

    public bool gotKey = false;
    public bool gotKnife = false;
    public bool inCinematic = false;

    //On stock la maincam
    public GameObject mainCam;
    public GameObject currentHidingCam;

    public List<Transform> stage;

    int stageIndex;

    public GameObject pauseMenuUI;

    [HideInInspector]
    public static bool gameIsPaused = false;

    public GameObject ennemy;
    public GameObject porteGardien;
    Animator porteAnim;

    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;
        sm = GetComponent<SoundManager>();
    }

    void Start()
    {
        isHidden = false;
        mainCam.SetActive(true);
        stageIndex = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        porteAnim = porteGardien.GetComponent<Animator>();
        
        
    }

    public void AddKey()
    {
        gotKey = true;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }

        if (porteAnim.GetBool("Open") == true)
        {
            DoorGardienClose = false;
        }
        else if (porteAnim.GetBool("Open") == false)
        {
            DoorGardienClose = true;
        }
        


        if (isHidden && Input.GetKeyDown(KeyCode.H))
        {
            HideToSpot(null);

        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void GoToMenu()
    {
        SceneManager.LoadSceneAsync("MenuPrincipal");
        UnloadGame();
    }

    public void UnloadGame()
    {
        SceneManager.UnloadSceneAsync("PlayScene");
    }

    public void ChangeLevel()
    {
        player.transform.position = stage[stageIndex].transform.position;
        stageIndex++;
        Debug.Log("a tous");
    }

    public void HideToSpot(GameObject altCam)
    {
        Debug.Log("Hide Toggle");
        if (isHidden)
        {
            if (currentHidingCam)
                currentHidingCam.SetActive(false);
            player.SetActive(true);
            isHidden = false;
            SoundManager.instance.Play("CasierClose");
        }
        else
        {
            currentHidingCam = altCam;
            currentHidingCam.SetActive(true);
            player.SetActive(false);
            isHidden = true;
            if (InventoryPlayer.instance.discoverHidding == false)
            {
                InventoryPlayer.instance.discoverHidding = true;
            }
        }
        player.GetComponent<Animator>().SetBool("isHidden", isHidden);

    }

    

    


    

    
}
