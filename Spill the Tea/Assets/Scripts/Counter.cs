using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField]
    private GameObject ui;

    [SerializeField]
    private Transform counterPosition;

    private TaskCompletionSource<int> task;

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
