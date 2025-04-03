using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commands;

public class CMD_DatabaseExtension_Examples : CMD_DatabaseExtension
{
    new public static void Extend(CommandDatabase database)
    {
        // add parameterless command
        database.AddCommand("print_default", new Action(printDefaultMessage));

        // add Action with 1 parameter
        database.AddCommand("print_1_arg", new Action<string>(PrintUserMessage));

        // add Action with mutliple parameters 
        database.AddCommand("print_n_args", new Action<string[]>(PrintLines));



        // add lambda with no params
        database.AddCommand("lambda", new Action(() => { Debug.Log("Lambda Message"); }));

        // add lambda with 1 param
        database.AddCommand("lambda_1_arg", new Action<string>((string arg1) => { Debug.Log($"Lambda 1 Arg Message: {arg1}"); }));

        // add lambda with multiple parameters
        database.AddCommand("lambda_n_args", new Action<string[]>((string[] args) => { Debug.Log($"Lambda multi-arg {string.Join(" ", args)}"); }));



        // Add couroutine with no params
        database.AddCommand("process", new Func<IEnumerator>(SimpleProcess));

        // Add couroutine with 1 params
        database.AddCommand("process_1_arg", new Func<string, IEnumerator>(SimpleProcessUserMessage));

        // Add couroutine with multiple params
        database.AddCommand("process_n_args", new Func<string[], IEnumerator>(SimpleProcessMultiline));


        // sample movement
        database.AddCommand("moveCharDemo", new Func<string, IEnumerator>(MoveCharacter));
    }

    private static void printDefaultMessage()
    {
        Debug.Log("Default message to console");
    }

    private static void PrintUserMessage(string message)
    {
        Debug.Log($"User Message: '{message}'");
    }

    private static void PrintLines(string[] lines)
    {
        int i = 1;
        foreach (string line in lines)
        {
            Debug.Log($"{i++}: '{line}'");
        }
    }

    private static IEnumerator SimpleProcess()
    {
        for (int i = 1; i <= 5; i++)
        {
            Debug.Log($"Process Running... {i}");
            yield return new WaitForSeconds(1);
        }
    }
    private static IEnumerator SimpleProcessUserMessage(string data)
    {
        if (int.TryParse(data, out int num))
        {
            for (int i = 1; i <= num; i++)
            {
                Debug.Log($"Process Running... {i}");
                yield return new WaitForSeconds(1);
            }
        }
    }
    private static IEnumerator SimpleProcessMultiline(string[] data)
    {
        foreach (string line in data)
        {
            Debug.Log($"Process Message: '{line}'");
            yield return new WaitForSeconds(0.5f);
        }
    }

    private static IEnumerator MoveCharacter(string direction)
    {
        bool left = direction.ToLower() == "left";

        // get variables needed
        Transform character = GameObject.Find("TestCharacter").transform;
        float moveSpeed = 15;

        // calc targ pos
        float targetX = left ? -8 : 8;

        // Get cur pos 
        float currentX = character.position.x;

        // move char
        while (Mathf.Abs(targetX - currentX) > 0.1f)
        {
            // Debug.Log($"Moving character to {(left ? "left" : "right")} [{currentX}/{targetX}]"); 
            currentX = Mathf.MoveTowards(currentX, targetX, moveSpeed * Time.deltaTime);
            character.position = new Vector3(currentX, character.position.y, character.position.z);
            yield return null;
        }
    }
}
