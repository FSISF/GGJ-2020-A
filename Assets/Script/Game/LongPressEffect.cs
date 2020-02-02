using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//修理item
public class LongPressEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{
  public Sprite lv0; //item未破壞
  public Sprite lv1;
  public Sprite lv2;
  public Sprite lv3;

  public GameObject hammer;

  public int level = 0; //item狀態

  void CheckLevel(){
    if(level > 3){ //如果破壞程度大於3
      GameStateManager.Instance.SetGameState(eGameState.GameOver);
    }
  }

  void Start(){
    GetComponent<SpriteRenderer>().sprite = lv0;

    GameEvent.CheckObjectLevel += CheckLevel;

  }

  public float PressDownTimer; //按下幾秒觸發
  private bool PressDown; //按下
  public UnityEvent onLongClick; //開啟Inspector觸發事件

  [SerializeField]
  public float HoldTime;

  //按下按鈕
  public void OnPointerDown(PointerEventData eventData){
    PressDown = true;
    hammer.SetActive(true);
    //Debug.Log("PressDown");
  }

  //按鈕彈起
  public void OnPointerUp(PointerEventData eventData){
    Reset();
    //Debug.Log("PressUp");
  }

  //當按下按鈕 PressDown = true 時計時
  void Update(){
    if (level == 3)
    { //如果破壞程度為lv3
        GetComponent<SpriteRenderer>().sprite = lv3;
    }
    else if (level == 2)
    { //如果破壞程度為lv2
        GetComponent<SpriteRenderer>().sprite = lv2;
    }
    else if (level == 1)
    { //如果破壞程度為lv1
        GetComponent<SpriteRenderer>().sprite = lv1;
    }
    else if (level == 0)
    { //如果破壞程度為lv1
        GetComponent<SpriteRenderer>().sprite = lv0;
    }

    if (PressDown == true){
      PressDownTimer += Time.deltaTime;
      if (PressDownTimer >= HoldTime){
        if (onLongClick != null){
          onLongClick.Invoke();
        }
        Reset();
      }
    } 
  }

  //當PressUp的時候重置計算時間
  private void Reset(){
    PressDown = false;
    PressDownTimer = 0;
    hammer.SetActive(false);
  }
  
  //觸發後執行的功能
  public void LongPressFuntion() { 
    Debug.Log("修復item");
    hammer.SetActive(false);

    //切換不同的level圖
    if(level == 3){ //如果破壞程度為lv3
      GetComponent<SpriteRenderer>().sprite = lv2;
    } else if(level == 2){ //如果破壞程度為lv2
      GetComponent<SpriteRenderer>().sprite = lv1;
    } else if(level == 1){ //如果破壞程度為lv1
      GetComponent<SpriteRenderer>().sprite = lv0;
    }

    //更新level狀態
    if(level > 0){ //需有損壞才更新
      level -= 1; //item狀態回復到上一個level
    }
  }
}