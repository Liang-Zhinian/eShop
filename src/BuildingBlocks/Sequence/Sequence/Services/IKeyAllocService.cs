// Author: 	Liang Zhinian
// On:		2020/5/23
using System;
using System.Threading.Tasks;

namespace Sequence.Services
{
    public interface IKeyAllocService
    {
        Task<long> GetKey(string seqName);
    }
}
