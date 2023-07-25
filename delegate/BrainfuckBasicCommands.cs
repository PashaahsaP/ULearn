using System;
namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		static string defaultSigns = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {

            InitDefaultSigns(vm);
            vm.RegisterCommand('-', b =>
            {
                int temp = Mod(b.Memory[b.MemoryPointer] - 1, 256);
                b.Memory[b.MemoryPointer] = (byte)temp;
            });
            vm.RegisterCommand('+', b =>
            {
                int temp = Mod(b.Memory[b.MemoryPointer]+1,256);
                b.Memory[b.MemoryPointer] = (byte)temp;
            });
            vm.RegisterCommand('.', b => write((char)b.Memory[b.MemoryPointer]));
            vm.RegisterCommand(',', b => b.Memory[b.MemoryPointer] = (byte)read());
            vm.RegisterCommand('<', b =>
            {
                b.MemoryPointer = Mod(b.MemoryPointer - 1, b.Memory.Length);
            });
			vm.RegisterCommand('>', b =>
            {
                b.MemoryPointer = Mod(b.MemoryPointer + 1, b.Memory.Length);
            });
        }

        static int Mod(int val, int mod) => (val % mod + mod) % mod;
        static void InitDefaultSigns(IVirtualMachine vm)
        {
            for (int i = 0; i < defaultSigns.Length; i++)
            {
                int temp = defaultSigns[i];
                vm.RegisterCommand(defaultSigns[i], b => b.Memory[b.MemoryPointer] = (byte)temp);
            }
        }
    }
}