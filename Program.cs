using System.Text;
namespace comp_sys_lab1
{
    class Program
    {
        static readonly uint taskCount = 3000;
        static readonly uint processorsCount = 5;
        static readonly uint elapsedTime = 10000;
        static readonly uint minimalL = 10;
        static readonly uint maximumH = 200;
        static public void SetProcessorPerfomance(ProcessorUnit processorUnit, uint i)
        {
            Console.WriteLine($"Input processor unit #{i} performance(from 1 to 10): ");
            uint perf;
            do
            {
                perf = Convert.ToUInt32(Console.ReadLine());
                if (perf < 1 || perf > 10)
                    Console.WriteLine("Incorrect perfomance value, try again");
            } while (perf < 1 || perf > 10);
            processorUnit.SetPerformance(perf);
        }
        static public uint SetChance()
        {
            uint probability = 80;
            Console.WriteLine("Set chance? Y?");
            ConsoleKey k = Console.ReadKey().Key;
            Console.WriteLine("\n");
            if (k == ConsoleKey.Y)
            {
                Console.WriteLine("\nInput probability value(from 1 to 100): ");
                do
                {
                    probability = Convert.ToUInt32(Console.ReadLine());
                    if (probability < 1 || probability > 100)
                        Console.WriteLine("Incorrect probability value, try again");
                } while (probability < 1 || probability > 100);
            }
            return probability;
        }
        static public Tuple<uint, uint> SetBorders(uint slowestPerf)
        {
            Console.WriteLine("Set borders? Y?");
            ConsoleKey k = Console.ReadKey().Key;
            Console.WriteLine("\n");
            uint hres;
            uint lres;
            if (k == ConsoleKey.Y)
            {
                Console.WriteLine($"Input minimal complexity(>= {slowestPerf * minimalL})");
                do
                {
                    lres = Convert.ToUInt32(Console.ReadLine());
                    if (slowestPerf * minimalL > lres)
                        Console.WriteLine("Incorrect minimal complexity, try again");
                } while (slowestPerf * minimalL > lres);
                Console.WriteLine($"\nInput maximum complexity(<= {slowestPerf * maximumH} AND >= {lres})");
                do
                {
                    hres = Convert.ToUInt32(Console.ReadLine());
                    if (hres > slowestPerf * maximumH || hres < lres)
                        Console.WriteLine("Incorrect maximum complexity, try again");
                } while (hres > slowestPerf * maximumH || hres < lres);
            }
            else
            {
                lres = slowestPerf * minimalL;
                hres = slowestPerf * maximumH;
            }
            return new Tuple<uint, uint>(lres, hres);
        }

        static public void OutTable(List<ProcessorUnit> processorUnits, uint elapsedTime)
        {
            // Calculating statistics
            ulong completedTasksCount = 0;
            foreach (var processorUnit in processorUnits)
                completedTasksCount += processorUnit.GetCompletedTasksCount();

            ulong potentialOperationsCount = 0;
            foreach (var processorUnit in processorUnits)
                potentialOperationsCount += (processorUnit.GetPerformance() * elapsedTime) - processorUnit.GetTimeElapsedOnScheduler();

            ulong resultOperationsCount = 0;
            foreach (var processorUnit in processorUnits)
                resultOperationsCount += processorUnit.GetCompletedOperationsCount();

            decimal efficiency = Math.Round((decimal)resultOperationsCount / potentialOperationsCount * 100, 2);

            // Out statistics
            Console.WriteLine($"Completed tasks: {completedTasksCount}");
            for (int i = 0; i < processorUnits.Count; i++)
                Console.WriteLine($"Processor unit #{i + 1} have done {processorUnits[i].GetCompletedTasksCount()} tasks");
            
            Console.WriteLine($"Theoretical count of completed operations: {potentialOperationsCount}");
            Console.WriteLine($"Real count of completed operations: {resultOperationsCount}");
            Console.WriteLine($"Efficiency: {efficiency}");
        }
        static public void cleanProcessorUnits(List<ProcessorUnit> processorUnits)
        {
            foreach (var processorUnit in processorUnits)
                processorUnit.Clear();
        }
        static public void OutAndClean(List<ProcessorUnit> processorUnits, uint elapsedTime)
        {
            OutTable(processorUnits, elapsedTime);
            cleanProcessorUnits(processorUnits);
        }

        static void Main()
        {
            // 0. Set UTF-8
            Console.OutputEncoding = Encoding.UTF8;

            // 1. Set processor performance
            List<ProcessorUnit> processorUnits = [];
            for (uint i = 0; i < processorsCount; i++)
            {
                processorUnits.Add(new ProcessorUnit(i));
                SetProcessorPerfomance(processorUnits.Last(), Convert.ToUInt32(processorUnits.Count));
            }
            // 2. Set chance
            uint chance = SetChance();
            // 3. Set complexity borders
            ProcessorUnit slowestOne;
            slowestOne = processorUnits.MinBy(x => x.GetPerformance())!;
            Tuple<uint, uint> borders = SetBorders(slowestOne.GetPerformance());
            // 4. Generate processes
            TaskGenerator taskGenerator = new(borders, chance, processorsCount);
            List<Task> tasks = taskGenerator.GetTasks(taskCount);
            // 5. Set time 
            Console.WriteLine($"Elapsed time: {elapsedTime} ms");
            // 6. First algo
            Console.WriteLine("===FIFO===");
            SchedulerFIFO schedulerFIFO = new(processorUnits, tasks, elapsedTime);
            OutAndClean(processorUnits, elapsedTime);
            // 7. Second algo
            Console.WriteLine("===Slowest processor unit as scheduler===");

            OutAndClean(processorUnits, elapsedTime);
            // 8. Third algo
            Console.WriteLine("===Fastest processor unit as scheduler by interrupt===");

            OutAndClean(processorUnits, elapsedTime);
            // 9. Third algo with best settings
            Console.WriteLine("===Fastest processor unit as scheduler by interrupt(best settings)===");

            OutAndClean(processorUnits, elapsedTime);
            // 10. Exit
            do
            {
                Console.WriteLine("\nPress Q to exit");
            } while (Console.ReadKey().Key != ConsoleKey.Q);
        }
    }
}