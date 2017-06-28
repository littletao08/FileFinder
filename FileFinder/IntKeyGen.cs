using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileFinder
{
    class IntKeyGen
    {
        private int currentKey = 0;

        public int genKey()
        {
            currentKey ++;
            return currentKey;
        }
    }
}
