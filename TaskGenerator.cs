namespace comp_sys_lab1
{
    class TaskGenerator(Tuple<uint, uint> borders, uint probability, uint count)
    {
        private readonly List<List<uint>> processorCombo = GetCombinations(count);
        private readonly double probability = Convert.ToDouble(probability) / 100.0;
        private readonly Random r = new();

        private Task GenerateTask() => new(processorCombo[r.Next(1, processorCombo.Count - 1)],
            Convert.ToUInt32(r.Next(Convert.ToInt32(borders.Item1), Convert.ToInt32(borders.Item2))));

        public List<Task> GetTasks(uint count)
        {
            List<Task> tasks = [];
            for (uint i = 0; i < count; i++)
                if (r.NextDouble() < probability)
                {
                    Task gen = GenerateTask();
                    tasks.Add(gen);
                }
            return tasks;
        }

        private static List<List<uint>> GetCombinations(uint count)
        {
            List<uint> elements = [];
            for (uint i = 0; i < count; i++)
                elements.Add(i);

            List<List<uint>> result = [];
            for (int subsetSize = 0; subsetSize <= elements.Count; subsetSize++)
                result.AddRange(GetCombinations(elements, subsetSize));

            return result;
        }

        private static List<List<uint>> GetCombinations(List<uint> elements, int subsetSize)
        {
            if (subsetSize == 0)
                return [[]];

            if (elements.Count == 0)
                return [];

            var firstElement = elements[0];
            var restElements = elements.Skip(1).ToList();
            var combinationsWithFirst = GetCombinations(restElements, subsetSize - 1);

            foreach (var combination in combinationsWithFirst)
                combination.Insert(0, firstElement);

            var combinationsWithoutFirst = GetCombinations(restElements, subsetSize);

            return [.. combinationsWithFirst, .. combinationsWithoutFirst];
        }
    }
}
