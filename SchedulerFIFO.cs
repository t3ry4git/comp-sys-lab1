namespace comp_sys_lab1
{
    class SchedulerFIFO
    {

        public SchedulerFIFO(List<ProcessorUnit> processorUnits, List<Task> tasks, uint time)
        {
            while (tasks.Count > 0 && time > 0)
            {
                while (TryAddTask(processorUnits, tasks)) ;
                foreach (ProcessorUnit unit in processorUnits)
                    unit.Tick();

                time--;
            }
        }

        private bool TryAddTask(List<ProcessorUnit> processorUnits, List<Task> tasks)
        {
            Task current = tasks.First();
            ProcessorUnit unit = processorUnits.Find(x=>x.CanWorkWithTask(current) && x.IsQueueEmpty())!;
            if (unit != null)
            {
                unit.AddTask(current);
                tasks.Remove(current);
                return true;
            }
            return false;
            
        }
    }
}
