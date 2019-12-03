using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Enemies;
    public GameObject Player;
    public GameObject EndScene;
    public GameObject ClearScene;
    public GameObject btn;
    public Canvas UI;
    bool end = false;
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (!end && Player.GetComponent<mario_script>().health == 0)
        {
            end = true;
            GameObject endScene = Instantiate(EndScene, UI.transform);
            GameObject Btn = Instantiate(btn, UI.transform);
            Debug.Log("Player dead !");
        } else if (!end && Enemies.transform.childCount == 0)
        {
            end = true;
            GameObject endScene = Instantiate(ClearScene, UI.transform);
            GameObject Btn = Instantiate(btn, UI.transform);
            Debug.Log("Stage cleared!");

        }

    }
}
