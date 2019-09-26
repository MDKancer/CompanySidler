namespace BuildingPackage.Worker
{
    public class BuildingWorker<T,V>
    {
        private T worker;
        private V workerType;
        
        public BuildingWorker(V WorkerType)
        {
            this.workerType = WorkerType;
        }

        public BuildingWorker()
        {
            
        }

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