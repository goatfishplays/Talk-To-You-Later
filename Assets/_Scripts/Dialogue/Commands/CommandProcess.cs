using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Commands
{
    public class CommandProcess
    {
        public Guid ID;
        public string processName;
        public Delegate command;
        public CoroutineWrapper runningProcess;
        public string[] args;
        public UnityEvent onTerminateAction;

        public CommandProcess(Guid id, string processName, Delegate command, CoroutineWrapper runningProcess, string[] args, UnityEvent onTerminateAction)
        {
            ID = id;
            this.processName = processName;
            this.command = command;
            this.runningProcess = runningProcess;
            this.args = args;
            this.onTerminateAction = onTerminateAction;
        }

    }
}
