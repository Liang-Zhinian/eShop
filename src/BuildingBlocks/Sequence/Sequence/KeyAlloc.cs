// Author: 	Liang Zhinian
// On:		2020/5/23
using System;
namespace Sequence
{
    public class KeyAlloc
    {
        public long Id { get; set; }
        public string SequenceName { get; set; }
        public long IncrementSize { get; set; }
        public long MinValue { get; set; }
        public long MaxValue { get; set; }
        public long NextValue { get; set; }

        public KeyAlloc()
        {
        }
    }
}
