namespace Minkin_Lab02
{
    class Cache
    {
        public Config CurrentConfig { get; set; }
        public CurrentFilesCondition CurrentLog { get; set; }
        public CurrentFilesCondition ChangedFiles { get; set; }
        public bool HasChanges { get; set; }

        private int count;
        private static readonly Cache instance = new Cache();

        private Cache()
        {
            CurrentConfig = new Config();
            CurrentLog = new CurrentFilesCondition();
            ChangedFiles = new CurrentFilesCondition();
            HasChanges = false;
            count = 0;
        }

        public static Cache Instance
        {
            get
            {
                return instance;
            }
        }

        public long GetCount()
        {
            return count++;
        }
    }
}
