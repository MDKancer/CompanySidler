namespace Building
{
    public class BuildingWorkers<T,V>
    {
        private T worker;
        private V workerType;
        
        public BuildingWorkers(V WorkerType)
        {
            this.workerType = WorkerType;
        }

        public BuildingWorkers(){}

        public T Worker
        {
            get => worker;
            set => worker = value;
        }

        public V WorkerType
        {
            get => workerType;
            set => workerType = value;
        }
    }
}