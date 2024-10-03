namespace comp_sys_lab1
{
    class SchedulerFPUaS
    {
        public SchedulerFPUaS(List<ProcessorUnit> processorUnits, List<Task> tasks, uint time, uint workTime, uint planTime, bool intellectual)
        {
            processorUnits.MaxBy(x => x.GetPerformance())!.EnableSchedulerMode();
            while (tasks.Count > 0 && time > 0)
            {
                
                List<ProcessorUnit> emptyQueueUnits = processorUnits.FindAll(x => x.IsQueueEmpty());
                foreach (ProcessorUnit processorUnit in emptyQueueUnits)
                {
                    Task desiredTask = tasks
        .Where(t => t.availableProcessors.Contains(processorUnit.getMyNumber()))
        .OrderBy(t => t.availableProcessors.Count)
        .FirstOrDefault()!;
                    if (desiredTask != null)
                    {
                        processorUnit.AddTask(desiredTask);
                        tasks.Remove(desiredTask);
                    }
                }
                foreach (ProcessorUnit unit in processorUnits)
                    unit.Tick();

                time--;
                if (processorUnits.MaxBy(x => x.GetPerformance())!.workingInSchedulerModeTime >= planTime)
                {
                    processorUnits.MaxBy(x => x.GetPerformance())!.DisableSchedulerMode();
                }
                if(processorUnits.MaxBy(x => x.GetPerformance())!.workingInDefaultModeTime >= workTime)
                {
                    processorUnits.MaxBy(x => x.GetPerformance())!.EnableSchedulerMode();
                    if (intellectual)
                    {
                        workTime = (workTime + processorUnits.MaxBy(x => x.GetPerformance())!.lastLoadTime)/2;
                    }
                }

            }
        }

    }
}
