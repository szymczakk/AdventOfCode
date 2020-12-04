using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Day16.csharp
{
    class Program
    {
        private static readonly int[] _firstErrors = new []{157};

        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines("input.txt");
            if (input.Length % 4 != 0)
            {
                return;
            }

            var w = new W();
            var result = 0;
            for (int i = 0; i < input.Length/4; i++)
            {
                var before = input[i].Replace("Before: [", "").Replace("]", "");
                var action = input[++i];
                var after = input[++i].Replace("After:  [", "").Replace("]", "");
                i++;

                if (w.CanBeCalculatedByXOrMoreInstruction(3, before, after, action))
                {
                    result++;
                }
            }

            if (_firstErrors.Contains(result)) //over 500
            {
                return;
            }

            Console.WriteLine(result);
            Console.ReadLine();

            //result2 300 - 500
        }
    }

    public class W
    {
        public bool CanBeCalculatedByXOrMoreInstruction(int x, string before, string after, string @params)
        {
            var device = new Device();
            var passedInstructions = new ConcurrentBag<Device.Instructions>();
            var registerBefore = before.Split(new[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var registerAfter = after.Split(new[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var actionParams = @params.Split(' ').Select(int.Parse).ToArray();

            foreach (var instruction in (Device.Instructions[]) Enum.GetValues(typeof(Device.Instructions)))
            {
                Array.Copy(registerBefore, device.Registers, registerBefore.Length);
                device.Run(instruction, actionParams[1], actionParams[2], actionParams[3]);
                Debug.WriteLine(instruction + ": " + string.Join(" ",device.Registers));
                if (device.Registers.SequenceEqual(registerAfter))
                {
                    passedInstructions.Add(instruction);
                }
            }

            return passedInstructions.Count >= x;
        }
    }

    public class Device
    {
        public int[] Registers { get; set; }

        public enum Instructions
        {
            Addr,
            Addi,
            Mulr,
            Muli,
            Banr,
            Bani,
            Borr,
            Bori,
            Setr,
            Seti,
            Gtir,
            Gtri,
            Gtrr,
            Eqir,
            Eqri,
            Eqrr,
        }

        private readonly Dictionary<Instructions, Action<int, int, int>> _instructions;

        public Device()
        {
            Registers = new int[4];

            _instructions = new Dictionary<Instructions, Action<int, int, int>>(16)
            {
                {Instructions.Addr, (a, b, c) => Registers[c] = Registers[a] + Registers[b]},
                {Instructions.Addi, (a, b, c) => Registers[c] = Registers[a] + b},
                {Instructions.Mulr, (a, b, c) => Registers[c] = Registers[a] * Registers[b]},
                {Instructions.Muli, (a, b, c) => Registers[c] = Registers[a] * b},
                {Instructions.Banr, (a, b, c) => Registers[c] = Registers[a] & Registers[b]},
                {Instructions.Bani, (a, b, c) => Registers[c] = Registers[a] & b},
                {Instructions.Borr, (a, b, c) => Registers[c] = Registers[a] | Registers[b]},
                {Instructions.Bori, (a, b, c) => Registers[c] = Registers[a] | b},
                {Instructions.Setr, (a, b, c) => Registers[c] = Registers[a]},
                {Instructions.Seti, (a, b, c) => Registers[c] = a},
                {Instructions.Gtir, (a, b, c) =>
                {
                    if (a > Registers[b])
                    {
                        Registers[c] = 1;
                    }
                    else
                    {
                        Registers[c] = 0;
                    }
                }},
                {Instructions.Gtri, (a, b, c) =>
                {
                    if (Registers[a] > b)
                    {
                        Registers[c] = 1;
                    }
                    else
                    {
                        Registers[c] = 0;
                    }
                }},
                {Instructions.Gtrr, (a, b, c) =>
                {
                    if (Registers[a] > Registers[b])
                    {
                        Registers[c] = 1;
                    }
                    else
                    {
                        Registers[c] = 0;
                    }
                }},
                {Instructions.Eqir, (a, b, c) =>
                {
                    if (a == Registers[b])
                    {
                        Registers[c] = 1;
                    }
                    else
                    {
                        Registers[c] = 0;
                    }
                }},
                {Instructions.Eqri, (a, b, c) =>
                {
                    if (Registers[a] == b)
                    {
                        Registers[c] = 1;
                    }
                    else
                    {
                        Registers[c] = 0;
                    }
                }},
                {Instructions.Eqrr, (a, b, c) =>
                {
                    if (Registers[a] == Registers[b])
                    {
                        Registers[c] = 1;
                    }
                    else
                    {
                        Registers[c] = 0;
                    }
                }}
            };
        }

        public void Run(Instructions instruction, int a, int b, int c)
        {
            try
            {
                _instructions[instruction](a, b, c);
            }
            catch (IndexOutOfRangeException e)
            {
                return;
            }
        }
    }
}
