using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commands;

public class TestCommands : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        yield return CommandManager.instance.Execute("print_default");
        yield return CommandManager.instance.Execute("print_1_arg", "Hello, World :3");
        yield return CommandManager.instance.Execute("print_n_args", "line 1", "line 2", "line 3");

        yield return CommandManager.instance.Execute("lambda");
        yield return CommandManager.instance.Execute("lambda_1_arg", "Hello, lambda :3");
        yield return CommandManager.instance.Execute("lambda_n_args", "lambda 1", "lambda 2", "lambda 3");

        yield return CommandManager.instance.Execute("process");
        yield return CommandManager.instance.Execute("process_1_arg", "Hello, process :3");
        yield return CommandManager.instance.Execute("process_n_args", "process 1", "process 2", "process 3");

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CommandManager.instance.Execute("moveCharDemo", "left");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CommandManager.instance.Execute("moveCharDemo", "right");
        }
    }
}
