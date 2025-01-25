using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField]
    private GameObject ui;

    [SerializeField]
    private List<Button> buttons;

    [SerializeField]
    private Transform counterPosition;

    private TaskCompletionSource<int> task;

    void Start()
    {
        Color[] colors = GameManager.getColors();
        for (int i = 0; i < buttons.Count; i++)
        {
            var button = buttons[i];
            int colorIndex = i % colors.Length;
            button.GetComponent<Image>().color = colors[colorIndex];
            var j = i; // avoid closure problem
            button.onClick.AddListener(() => OnClick(j));
        }
    }

    public async Task<int> ShowSelection(){
        ui.SetActive(true);
        task = new TaskCompletionSource<int>();
        return await task.Task;
    }

    public void OnClick(int num)
    {
        ui.SetActive(false);
        task?.SetResult(num);
    }

    public Transform GetPosition() => counterPosition;
}
