namespace comp_sys_lab1
{
    class Task(List<uint> availableProcessors, uint complexity)
    {
        public List<uint> availableProcessors = availableProcessors;
        public uint complexity = complexity;
        public bool status = false; // false - need work, true - done
        public uint balance = 0;
        public bool Tick(uint ticks)
        {
            uint saved = complexity;
            complexity -= ticks;
            if(saved < complexity || complexity == 0)
            {
                status = true;
                balance = saved;
                complexity = 0;
            }
            return status;
        }
        public uint getBalance() => balance;
    }
}
