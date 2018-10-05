using System;
using Microsoft.AspNetCore.Http;

namespace Sites.API.Requests
{
    public class UploadImageRequest
    {
        public UploadImageRequest()
        {

        }

        public UploadImageRequest(IFormFile image)
        {
            Image = image;
        }


        public IFormFile Image { get; set; }
    }

}
