using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 추가
using UnityEngine.UI; // UI 요소 사용을 위한 네임스페이스 추가

// ProDialogue 클래스 정의 (프롤로그 대사 저장)
public class ProDialogue_yj
{
    public int id; // 번호
    public string name; // 인물
    public string line; // 대사

    public ProDialogue_yj(int id, string name, string line)
    {
        this.id = id;
        this.name = name;
        this.line = line;
    }
}


public class TalkManager_yj : MonoBehaviour
{
    /*   Dictionary<int, string[]> talkData_yj;
       Dictionary<int, Sprite> portraitData_yj;

       public Sprite[] portraitArr_yj;
       // Start is called before the first frame update
       void Awake()
       {
           talkData_yj = new Dictionary<int, string[]>();
           portraitData_yj = new Dictionary<int, Sprite>();
           GenerateData();
       }

       void GenerateData()
       {
           // 힌트 아이템(없어질 수도 있음. 사실 왜 만들었는지 나도몰루겠다)
           talkData_yj.Add(100, new string[] { "단서다. 조사해 보자()" });
           // 훈련단장 만났을 때
           talkData_yj.Add(1000, new string[] { "Hello?:0", "It's your first time here, right?:1" });
           // 기사단장 만났을 때
           talkData_yj.Add(2000, new string[] { "Let's exercise!:1", "Muscle! hustle!:2" });

           // quest talk
           talkData_yj.Add(10 + 1000, new string[] { "Welcome!:0", "Talk to KightsJJang! :1" });
           talkData_yj.Add(11 + 2000, new string[] { "Hey!:0", "Give me THAT coin! :1" });
           talkData_yj.Add(20 + 1000, new string[] { "coin?:0", "I found it! :3" });
           talkData_yj.Add(20 + 5000, new string[] { "I found THAT coin! :1" });
           talkData_yj.Add(21 + 2000, new string[] { "Thanks! :2"});

           portraitData_yj.Add(1000 + 0, portraitArr_yj[0]);
           portraitData_yj.Add(1000 + 1, portraitArr_yj[1]);
           portraitData_yj.Add(1000 + 2, portraitArr_yj[2]);
           portraitData_yj.Add(1000 + 3, portraitArr_yj[3]);

           portraitData_yj.Add(2000 + 0, portraitArr_yj[4]);
           portraitData_yj.Add(2000 + 1, portraitArr_yj[5]);
           portraitData_yj.Add(2000 + 2, portraitArr_yj[6]);
           portraitData_yj.Add(2000 + 3, portraitArr_yj[7]);
       }

       public string GetTalk_yj(int id_yj, int talkIndex_yj)
       {
           if (talkIndex_yj == talkData_yj[id_yj].Length)
               return null;
           else
               return talkData_yj[id_yj][talkIndex_yj];
       }

       public Sprite GetPortait_yj(int id_yj, int portraitIndex_yj)
       {
           return portraitData_yj[id_yj + portraitIndex_yj];
       }
       void Start()
       {

       }

       // Update is called once per frame
       void Update()
       {

       }
   }
   */
    // 기본 활동 정도는 스트립트 내에서 전부 대사 처리(어차피 5개밖에 없음)
    // 아이디 설정 설명 : 6000번대부터 시작함
    // 6001 : 훈련대장, 6002 : 기사단장, 6003 : 단서


    // 기본활동1 : 훈련대장 기본 대사1
    ProDialogue_yj serif1_1 = new ProDialogue_yj(6001, "훈련대장","여어-말라깽이! 훈련할 준비는 됐나?");
    // 기본활동1 : 훈련대장 기본 대사2
    ProDialogue_yj serif1_2 = new ProDialogue_yj(6001, "훈련대장", "안되면 될때까지! 훈련 시작이다!");

    // 기본활동2 : 기사단장 기본 대사1
    ProDialogue_yj serif2_1 = new ProDialogue_yj(6002, "기사단장", "뭉치면 살고 흩어지면 죽는다! \n단합훈련 시작이다!!");
    // 기본활동2 : 기사단장 기본 대사2
    ProDialogue_yj serif2_2 = new ProDialogue_yj(6002, "기사단장", "3 -1 = 0! 우리는 하나다!  \n단합훈련 시작이다!!");

    // 기본활동3 : 단서 칮았을 때 기본 대사
    ProDialogue_yj serif3 = new ProDialogue_yj(6003, "단서", "단서를 찾았다. 내용을 살펴보자.");

    // 대사들을 저장할 리스트
    private List<ProDialogue> proDialogue;

    public GameObject opening;
    public TextMeshProUGUI openingText; // TextMeshPro UI 텍스트 요소

    public GameObject narration;
    public TextMeshProUGUI narrationText; // TextMeshPro UI 텍스트 요소

    public GameObject dialogue;
    public GameObject nameObj; // 이름 요소
    public TextMeshProUGUI nameText; // TextMeshPro UI 텍스트 요소
    public TextMeshProUGUI descriptionText; // TextMeshPro UI 텍스트 요소

    //public GameObject resultPanel; // 단합 결과를 표시할 패널

    //public GameObject home; // 집 배경 화면

    private int currentDialogueIndex = 0; // 현재 대사 인덱스
    private bool isActivated = false; // TalkManager가 활성화되었는지 여부

    void Awake()
    {
        proDialogue = new List<ProDialogue>();
        // LoadDialogueFromCSV(); // CSV에서 데이터를 로드하는 함수 호출
        LoadDialogueManually(); // CSV 연결 없이 대화 수동 입력
    }

    void Start()
    {
        ActivateTalk(); // 오브젝트 활성화
    }

    void Update()
    {
        /*if (isActivated && currentDialogueIndex == 0)
        {
            PrintProDialogue(currentDialogueIndex);
        }*/
        if (isActivated && Input.GetKeyDown(KeyCode.Space))
        {
            currentDialogueIndex++;
            PrintProDialogue(currentDialogueIndex);
        }
    }

    // 대화 수동 입력
    void LoadDialogueManually()
    {
        // 수동으로 대화 입력
        proDialogue.Add(new ProDialogue(0, "기사단장", "단합하시겠습니까?"));
    }

    void PrintProDialogue(int index)
    {
        if (index >= proDialogue.Count)
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            return; // 대사 리스트를 벗어나면 오브젝트 비활성화 후 리턴
        }

        ProDialogue currentDialogue = proDialogue[index];

        dialogue.SetActive(true);
        nameText.text = currentDialogue.name;
        descriptionText.text = currentDialogue.line;

        // 인물에 따라 대사/나레이션/텍스트 창 활성화

        // 기본활동 1 : 훈련단장 -> 훈련하기
        if (currentDialogue.name == "훈련단장")
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            opening.SetActive(false);
            narrationText.text = currentDialogue.line;
            //ShowConfirmationPanel(); // "예" 또는 "아니오" 선택 패널 표시
        }
        // 기본활동 2 : 기사단장 -> 단합하기
        else if (currentDialogue.name == "기사단장")
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            opening.SetActive(false);
            narrationText.text = currentDialogue.line;
            //ShowConfirmationPanel(); // "예" 또는 "아니오" 선택 패널 표시
        }
        // 기본활동 3 : 정보수집 -> 길에 정보 떨어져 있음
        else if (currentDialogue.name == "information")
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            opening.SetActive(false);
            narrationText.text = currentDialogue.line;
            //ShowConfirmationPanel(); // "예" 또는 "아니오" 선택 패널 표시
        }
        // 아마 스크립트 내에서 처리할 거니까 아이디 체크 안해도 될 거 같긴 함
        // CheckTalk(currentDialogue.id);
    }

    // 선택 패널은 다른 클래스에서 받아올 거 같으니 지울듯
    void ShowConfirmationPanel()
    {
        // 예 또는 아니오 선택 패널을 표시하고 버튼 이벤트 설정
        //resultPanel.SetActive(true);

        // 예 버튼 클릭 시 처리
        /*resultPanel.transform.Find("YesButton").GetComponent<Button>().onClick.AddListener(() => {
            HandleAffiliation(true); // 단합하기 처리
        });

        // 아니오 버튼 클릭 시 처리
        resultPanel.transform.Find("NoButton").GetComponent<Button>().onClick.AddListener(() => {
            HandleAffiliation(false); // 단합 안하기 처리
        });*/
    }

    public void ActivateTalk()
    {
        this.gameObject.SetActive(true);
        isActivated = true;
    }

    void DeactivateTalk()
    {
        this.gameObject.SetActive(false);
        isActivated = false;
    }

    // 단합력 증가 함수
   /*
    public void IncreaseTeamPower(int amount)
    {
        PlayerManager_yj playerManager = FindObjectOfType<PlayerManager_yj>();
        if (playerManager != null)
        {
            playerManager.IncreaseTeamPower(amount);
        }
        else
        {
            Debug.LogError("PlayerManager_yj not found in the scene.");
        }
    }*/
}
