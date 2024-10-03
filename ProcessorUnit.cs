namespace comp_sys_lab1
{
    class ProcessorUnit
    {


        private uint myNumber = 0;
        private uint performanceClass = 1;
        private uint completedTasksCount = 0;
        private uint completedOperationsCount = 0;
        private uint elapsedOnScheduler = 0;
        public bool workedAsScheduler = false;
        public bool workingAsScheduler = false;
        public uint workingInSchedulerModeTime = 0;
        public uint workingInDefaultModeTime = 0;
        public uint lastLoadTime = 0;
        private List<Task> queue = [];

        public ProcessorUnit(uint myNumber) { this.myNumber = myNumber; }
        public ProcessorUnit() { }
        public void SetPerformance(uint performanceClass) => this.performanceClass = performanceClass;
        public uint GetPerformance() => performanceClass;

        public uint GetCompletedTasksCount() => completedTasksCount;

        // CTC - completed tasks count
        public void IncCTC() => completedTasksCount++;

        public uint GetCompletedOperationsCount() => completedOperationsCount;

        // COC - completed operations count
        public void IncCOC() => completedOperationsCount += performanceClass;

        public void Clear()
        {
            completedOperationsCount = 0;
            completedTasksCount = 0;
            elapsedOnScheduler = 0;
            queue = [];
            workedAsScheduler = false;
            workingAsScheduler = false;
            workingInSchedulerModeTime = 0;
            workingInDefaultModeTime = 0;
        }

        public void IncSchedulTime() => elapsedOnScheduler++;

        public uint GetTimeElapsedOnScheduler() => elapsedOnScheduler;

        public void AddTask(Task task)
        {
            queue.Add(task);
            lastLoadTime = task.complexity;
        }

        private void RemTask(Task task) => queue.Remove(task);

        public void Tick()
        {
            if (workingAsScheduler)
            {
                IncSchedulTime();
                IncSchModeTime();
            }
            else
            {
                if (queue.Count > 0)
                {
                    IncCOC();
                    IncDefModeTime();
                    if (queue.First().Tick(performanceClass))
                    {
                        completedOperationsCount -= queue.First().getBalance();
                        queue.Remove(queue.First());
                        IncCTC();
                    }
                }
            }
        }

        public bool IsQueueEmpty() => queue.Count == 0;
        public int QueueCount() => queue.Count;

        public bool CanWorkWithTask(Task task) => task.availableProcessors.Contains(myNumber);

        public void EnableSchedulerMode()
        {
            workedAsScheduler = true;
            workingAsScheduler = true;
            workingInDefaultModeTime = 0;
        }

        public void DisableSchedulerMode() 
        {
            workingAsScheduler = false;
            workingInSchedulerModeTime = 0;
        }

        public void IncSchModeTime() => workingInSchedulerModeTime++;
        public void IncDefModeTime() => workingInDefaultModeTime++;

        public uint getMyNumber() => myNumber;
    }
}
