// Author: 	Liang Zhinian
// On:		2020/5/23
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sequence.Services
{
    public class KeyAllocService: IKeyAllocService
    {
        private SeqContext _seqContext;

        public KeyAllocService(SeqContext seqContext)
        {
            _seqContext = seqContext;
        }

        public async ValueTask<long> GetKey(string seqName)
        {
            var value = 1L;
            var strategy = _seqContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _seqContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var seq = _seqContext.KeyAlloc.SingleOrDefault(s => s.SequenceName.Equals(seqName));
                        if (seq == null)
                        {
                            seq = new KeyAlloc()
                            {
                                SequenceName = seqName,
                                IncrementSize = 1,
                                MinValue = 1,
                                MaxValue = 9999,
                                NextValue = 1
                            };
                        }

                        value = seq.NextValue;
                        seq.NextValue = value + seq.IncrementSize;
                        _seqContext.Attach(seq);
                        await _seqContext.SaveChangesAsync();

                        // Commit transaction if all commands succeed, transaction will auto-rollback
                        // when disposed if either commands fails
                        transaction.Commit();

                    }
                    catch (Exception)
                    {
                        // TODO: Handle failure
                        transaction.Rollback();
                    }
                }
            });
            return value;
        }
    }
}
