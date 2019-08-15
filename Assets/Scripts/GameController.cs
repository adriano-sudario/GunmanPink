using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int _bulletsCount;
    int _score;
    List<MosquitoController> mosquitoes;
    Text bulletsInfo;
    Text scoreInfo;
    Text loseInfo;

    public MosquitoController prefab;
    public bool hasBulletsEnded { get; private set; }
    public bool hasLost { get; private set; }
    public bool hasBegun { get; private set; } = false;
    public List<BulletController> bullets = new List<BulletController>();

    public int bulletsCount
    {
        get => _bulletsCount;
        set
        {
            _bulletsCount = value;
            bulletsInfo.text = $"Bullets: {_bulletsCount}";
            hasBulletsEnded = _bulletsCount <= 0;

            if (hasBulletsEnded)
                StartCoroutine(LastChance());
        }
    }

    public int score
    {
        get => _score;
        set
        {
            _score = value;
            scoreInfo.text = $"Score: {_score}";
        }
    }

    void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        Text[] texts = canvas.GetComponentsInChildren<Text>();
        bulletsInfo = texts.Where(t => t.name == "BulletsCount").First();
        scoreInfo = texts.Where(t => t.name == "Score").First();
        loseInfo = texts.Where(t => t.name == "YouLose").First();

        mosquitoes = new List<MosquitoController>();
        loseInfo.text = "Press start to shoot mosquitoes";
    }

    void Initialize()
    {
        hasLost = false;
        loseInfo.text = "";
        loseInfo.gameObject.SetActive(false);
        bulletsCount = 6;
        score = 0;
        StartCoroutine(SpawnMosquito());
    }

    void Update()
    {
        if (!hasBegun)
        {
            hasBegun = Input.GetKeyDown(KeyCode.Return);

            if (hasBegun)
                Initialize();

            return;
        }

        if (!hasLost)
            return;

        if (Input.GetKeyDown(KeyCode.Return))
            Initialize();
    }

    IEnumerator LastChance()
    {
        yield return new WaitForSeconds(3);

        if (hasBulletsEnded && !hasLost)
            Lose();
    }

    IEnumerator SpawnMosquito()
    {
        if (!hasBegun)
            yield return null;

        MosquitoController mosquito = Instantiate(prefab);
        mosquitoes.Add(mosquito);

        yield return new WaitForSeconds(Random.Range(.5f, 1.5f));

        if (!hasLost)
            StartCoroutine(SpawnMosquito());
    }

    void Lose()
    {
        Debug.Log("teste " + bullets.Count);
        hasLost = true;
        loseInfo.gameObject.SetActive(true);
        string[] diseases = new string[]
        {
            "Yellow Fever",
            "Dengue",
            "Chikungunya",
            "Malaria",
            "Zika Virus"
        };

        loseInfo.text = $"You got {diseases[Random.Range(0, diseases.Length)]}";
    }
}
