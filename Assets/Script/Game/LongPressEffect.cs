using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongPressEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{
  public float PressDownTimer; //按下幾秒觸發
  private bool PressDown; //按下
  public UnityEvent onLongClick; //開啟Inspector觸發事件

  [SerializeField]
  public float HoldTime;

  //按下按鈕
  public void OnPointerDown(PointerEventData eventData){
    PressDown = true;
    Debug.Log("PressDown");
  }

  //按鈕彈起
  public void OnPointerUp(PointerEventData eventData){
    Reset();
    Debug.Log("PressUp");
  }

  //當按下按鈕 PressDown = true 時計時
  void Update(){
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

  //當PressUp的時候重製計算時間
  private void Reset(){
    PressDown = false;
    PressDownTimer = 0;
  }
  
  //觸發後執行的功能
  public void LongPressFuntion() { 
    Debug.Log("觸發後執行的功能");
  }
}