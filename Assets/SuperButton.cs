using UnityEngine.Events;
using UnityEngine.UI;

public class SuperButton : Button
{
    public UnityAction<SuperButton> onButtonClick;

    override protected void Start()
    {
        base.Start();
        onClick.AddListener(onClickHandler);
    }

    private void onClickHandler()
    {
        onButtonClick(this);
    }
}
