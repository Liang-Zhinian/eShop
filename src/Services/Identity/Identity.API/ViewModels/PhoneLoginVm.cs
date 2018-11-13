using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Identity.API.ViewModels
{
    public class PhoneLoginVm
    {
        public PhoneLoginVm()
        {
        }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [JsonProperty("phone")]
        public string PhoneNumber { get; set; }
    }
}
