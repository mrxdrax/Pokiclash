using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private int powerupchunks = 10;
    public GameObject poweruppannel;
    public bool juststart=true;
    private int lastChunkShown = -1;
    // References for new powerup functions
    public orbit orbitScript;
    public AreaWeapon areaWeapon;
    public GameObject bombPrefab;
    public Transform bombDropPoint;
    private int bombCount = 1;
    public screenClear screenClearScript;
    
    [SerializeField] private float bombDropInterval = 5f; // Bombs drop every 3 seconds[SerializeField]
private float bombDropTimer = 2f;
    
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.P)) 
        {
            pause();
        }
        
        // Calculate current chunk based on killcount
        int currentChunk = playercontroller.instance.killcount / powerupchunks;
        
        // Show panel only when entering a new chunk (and not at start)
        if(currentChunk > lastChunkShown && !juststart && playercontroller.instance.killcount >= powerupchunks)
        {
            poweruppannel.SetActive(true);
            Time.timeScale = 0f;
            lastChunkShown = currentChunk;
        }
        
        // Resume game time if panel is not active
        if(!poweruppannel.activeSelf)
        {
            Time.timeScale = 1f;
        }

        // Auto-drop bombs on timer
        bombDropTimer -= Time.deltaTime;
        if (bombDropTimer <= 0f)
        {
            bombDropTimer = bombDropInterval;
            DropBombsAuto();
        }
    }
    public void gameover()
    {
       StartCoroutine(showgameover());  
    }
    IEnumerator showgameover()
    {
        yield return new WaitForSeconds(1.5f);
        UIcontroller.instance.gameoverpannel.SetActive(true);
    }
    public void restart()
    {
        SceneManager.LoadScene("GAME");
    }
    public void pause()
    {
        if(UIcontroller.instance.gameoverpannel.activeSelf == false && UIcontroller.instance.pausemenu.activeSelf == false)
        {
            UIcontroller.instance.pausemenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            UIcontroller.instance.pausemenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void mainmenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void maxhealth()
    {
        if (playercontroller.instance != null)
        {
            playercontroller.instance.playerhealth = playercontroller.instance.maxhealth;
            Debug.Log("Max Health Applied: " + playercontroller.instance.playerhealth);
            ClosePowerupPanel();
        }
        else
        {
            Debug.LogError("playercontroller.instance is NULL!");
        }
    }

    // Close panel and resume game
    private void ClosePowerupPanel()
    {
        if (poweruppannel != null)
            poweruppannel.SetActive(false);
        Time.timeScale = 1f;
        juststart = false;
    }

    // --- New powerup functions requested ---
    // 1) Call orbit to add a blade
    public void AddOrbitBlade()
    {
        if (orbitScript != null)
        {
            orbitScript.AddBlade();
        }
        ClosePowerupPanel();
    }

    // 2) Increase area weapon size by +1 unit (delegates to AreaWeapon)
    public void IncreaseKillArea()
    {
        if (areaWeapon != null)
        {
            areaWeapon.IncreaseAreaSize(0.2f);
        }
        else if (AreaWeapon.instance != null)
        {
            AreaWeapon.instance.IncreaseAreaSize(0.2f);
        }
        ClosePowerupPanel();
    }

    // 3) Drop bombs from button click: 1 at a time with 1 second gap
    // Bombs spawn 1 unit away from player towards enemies
    public void DropBombs()
    {
        if (bombPrefab == null) return;
        StartCoroutine(DropBombsCoroutine());
        ClosePowerupPanel();
    }

    // Auto-drop bombs (no panel closing)
    private void DropBombsAuto()
    {
        if (bombPrefab == null) return;
        StartCoroutine(DropBombsAutoCoroutine());
    }

    private IEnumerator DropBombsCoroutine()
    {
        for (int i = 0; i < bombCount; i++)
        {
            SpawnBomb(i, bombCount);
            Debug.Log("Dropped bomb " + (i + 1) + " of " + bombCount);
            yield return new WaitForSeconds(1f); // 1 second gap between bombs
        }
        
        bombCount++; // Increment count after all bombs are dropped
        Debug.Log("All bombs dropped! Next click will drop " + bombCount + " bombs.");
    }

    private IEnumerator DropBombsAutoCoroutine()
    {
        int tempCount = bombCount;
        for (int i = 0; i < tempCount; i++)
        {
            SpawnBomb(i, tempCount);
            yield return new WaitForSeconds(bombDropTimer);
        }
    }

    private void SpawnBomb(int index, int totalCount)
    {
        // Get player position
        Vector3 playerPos = playercontroller.instance.transform.position;
        
        // Spread bombs horizontally, 1 unit away from player
        float spreadWidth = (totalCount - 1) * 0.3f;
        float xOffset = (index - (totalCount - 1) / 2f) * 0.3f;
        Vector3 spawnPos = playerPos + new Vector3(xOffset, -1f, 0f); // 1 unit below player
        
        GameObject bomb =
Instantiate(bombPrefab, spawnPos, Quaternion.identity);

grandeprefab gp =
bomb.GetComponent<grandeprefab>();

GameObject[] enemies =
GameObject.FindGameObjectsWithTag("Enemy");

if(enemies.Length>0)
{
    GameObject nearest = enemies[0];
    float minDist =
    Vector2.Distance(
        spawnPos,
        nearest.transform.position);

    foreach(GameObject e in enemies)
    {
        float d =
        Vector2.Distance(
            spawnPos,
            e.transform.position);

        if(d<minDist)
        {
            minDist=d;
            nearest=e;
        }
    }

    gp.target=nearest.transform;
    Debug.Log(gp.target);
Debug.Log(nearest.name);
}
    }

    // 4) Clear screen via screenClear script
    public void ClearScreen()
    {
        if (screenClearScript != null)
        {
            screenClearScript.ClearNow();
        }
        ClosePowerupPanel();
    }
}