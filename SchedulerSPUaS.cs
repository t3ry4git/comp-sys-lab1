namespace comp_sys_lab1
{
    class SchedulerSPUaS
    {
        public SchedulerSPUaS(List<ProcessorUnit> processorUnits, List<Task> tasks, uint time)
        {
            while (tasks.Count > 0 && time > 0)
            {
                processorUnits.MinBy(x => x.GetPerformance())!.EnableSchedulerMode();
                List<ProcessorUnit> emptyQueueUnits = processorUnits.FindAll(x => x.IsQueueEmpty() && !x.workingAsScheduler);
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
            }
        }

    }
}
