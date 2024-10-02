namespace comp_sys_lab1
{
    class ProcessorUnit
    {
        private uint performanceClass = 1;
        private uint completedTasksCount = 0;
        private uint completedOperationsCount = 0;
        private uint elapsedOnScheduler = 0;

        public void SetPerformance(uint performanceClass) => this.performanceClass = performanceClass;
        public uint GetPerformance() => performanceClass;

        public uint GetCompletedTasksCount() => completedTasksCount;

        public void IncCTC() => completedTasksCount++;

        public uint GetCompletedOperationsCount() => completedOperationsCount;

        public void IncCOC() => completedOperationsCount++;

        public void Clear()
        {
            completedOperationsCount = 0;
            completedTasksCount = 0;
            elapsedOnScheduler = 0;
        }

        public void IncSchedulTime() => elapsedOnScheduler++;

        public uint GetTimeElapsedOnScheduler() => elapsedOnScheduler;
    }
}
