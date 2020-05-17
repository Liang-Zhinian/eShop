using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentServiceProvider.Areas.Payment.Controllers
{
    [Area("Payment")]
    public class HomeController : Controller
    {
        //private static Timer timer;

        // GET: /<controller>/
        public IActionResult Index()
        {
            var timerState = new TimerState { Counter = 0 };

            var callbackUrl = Request.Query["callback"];
            //Response.Headers.Add("REFRESH", "3;URL=" + callbackUrl);
            //return Redirect(callbackUrl);
            //timer = new Timer((state) =>
            //{
            //    //Response.Redirect(callbackUrl);
            //}, null, 3000, Timeout.Infinite);

            //timer = new Timer(
            //    callback: new TimerCallback(TimerTask),
            //    state: timerState,
            //    dueTime: 1000,
            //    period: 2000);

            //while (timerState.Counter <= 10)
            //{
            //    Task.Delay(4000).Wait();
            //}

            //timer.Dispose();

            ViewBag.CallBackUrl = callbackUrl;
            return View();
        }

        private static void TimerTask(object timerState)
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff}: starting a new callback.");
            var state = timerState as TimerState;
            Interlocked.Increment(ref state.Counter);
        }
    }

    class TimerState
    {
        public int Counter;
    }
}
