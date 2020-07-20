// Author: 	Liang Zhinian
// On:		2020/5/26
using System;
namespace PaymentServiceProvider
{
    public class AppSettings
    {

        public string EventBusConnection { get; set; }

        public bool UseCustomizationData { get; set; }
        public bool AzureStorageEnabled { get; set; }
    }
}
