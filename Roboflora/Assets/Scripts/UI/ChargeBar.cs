
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public Image lightning;
    void Start(){
        slider.maxValue = 3;
        slider.value = 0;
        fill.color = gradient.Evaluate(0f);
        lightning.enabled = false;
    }
    public void ResetChargeBar() {
        slider.maxValue = 3;
        slider.value = 0;
        fill.color = gradient.Evaluate(0f);
        lightning.enabled = false;
    }

    public void SetCharge(int charge) {
        slider.value = charge;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        if(charge == slider.maxValue){
            lightning.enabled = true;
        }
    }

    public void SetMaxCharge(int charge){
        slider.maxValue = charge;
    }
}
