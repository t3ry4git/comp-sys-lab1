namespace comp_sys_lab1
{
    class SchedulerSPUaS
    {
        public SchedulerSPUaS(List<ProcessorUnit> processorUnits, List<Task> tasks, uint time) {
            processorUnits.MinBy(x => x.GetPerformance())!.EnableSchedulerMode();
            
        }

    }
}
