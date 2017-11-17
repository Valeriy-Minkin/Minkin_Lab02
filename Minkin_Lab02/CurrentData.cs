using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minkin_Lab02
{
    class CurrentData
    {
        private static CurrentData instance;
        private int _count;

        private CurrentData()
        {
            _count = 0;
        }

        public static CurrentData getInstance()
        {
            if (instance == null)
                instance = new CurrentData();
            return instance;
        }

        public Config CurrentConfig { get; set; }
        public Log CurrentLog { get; set; }

        public long GetCount()
        {
            return _count++;
        }
    }
}
