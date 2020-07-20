// Author: 	Liang Zhinian
// On:		2020/5/23
using System;
using System.Threading.Tasks;

namespace Sequence.Services
{
    public interface IKeyAllocService
    {
        ValueTask<long> GetKey(string seqName);
    }
}
