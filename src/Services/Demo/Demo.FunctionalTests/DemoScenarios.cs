using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Demo.FunctionalTests
{
    public class DemoScenarios
       : DemoScenariosBase
    {


        [Fact]
        public async Task Get_get_next_value_and_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.Key());

                response.EnsureSuccessStatusCode();
                var t = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(t);
            }
        }

        [Fact]
        public async Task Get_get_10000_next_value_and_response_ok_status_code()
        {
            IList<string> list = new List<string>();
            Task.Run(async () =>
            {
                using (var server = CreateServer())
                {
                    for (int i = 0; i < 10000; i++)
                    {
                        await Task.Run(async () =>
                        {
                            var response = await server.CreateClient()
                                .GetAsync(Get.Key());

                            response.EnsureSuccessStatusCode();
                            var t = await response.Content.ReadAsStringAsync();
                            //Console.WriteLine(t);
                            lock (list)
                            {
                                list.Add(t);
                            }
                        });
                    }
                }
            }).Wait();
            list = list.OrderBy(i => i).ToList();
            list.Select(i =>
            {
                Console.WriteLine(i);
                var dup = list.Contains(i);
                Assert.False(dup);
                return i;
            });

            //var dup = list.Any(i => list.Contains(i));
            //Assert.False(dup);
        }

    }
}
