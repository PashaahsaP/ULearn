using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
    {
        public string Instructions { get; }
        public int InstructionPointer { get; set; } = 0;
        public byte[] Memory { get; }
        public int MemoryPointer { get; set; } = 0;
        public Dictionary<char, Action<IVirtualMachine>> CommandsActions { get; set; }

        public VirtualMachine(string program, int memorySize)
        {
            Instructions = program;
            Memory = new byte[memorySize];
            CommandsActions = new Dictionary<char, Action<IVirtualMachine>>();
        }

        public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
        {
			if (!CommandsActions.ContainsKey(symbol))
				CommandsActions.Add(symbol, execute);
        }

		public void Run()
		{
            while (InstructionPointer < Instructions.Length)
            {
                if (CommandsActions.ContainsKey(Instructions[InstructionPointer]))
                    CommandsActions[Instructions[InstructionPointer]].Invoke(this);
                InstructionPointer++;
            }
		}
	}
}