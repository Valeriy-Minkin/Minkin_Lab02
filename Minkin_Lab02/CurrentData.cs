using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minkin_Lab02
{
    class Cache
    {
        private static Cache instance;
        private int _count;

        private Cache()
        {
            CurrentConfig = new Config();
            CurrentLog = new CurrentFilesCondition();
            ChangedFiles = new CurrentFilesCondition();
            HasChanges = false;
            _count = 0;
        }

        public static Cache getInstance()
        {
            if (instance == null)
                instance = new Cache();
            return instance;
        }

        public Config CurrentConfig { get; set; }
        public CurrentFilesCondition CurrentLog { get; set; }
        public CurrentFilesCondition ChangedFiles { get; set; }
        public bool HasChanges { get; set; }

        public long GetCount()
        {
            return _count++;
        }
    }
}
