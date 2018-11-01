using System;
using Microsoft.AspNetCore.Http;

namespace Identity.API.ViewModels
{
    public class UploadUserAvatarVm
    {
        public UploadUserAvatarVm()
        {
		}

        public UploadUserAvatarVm(Guid id, IFormFile image)
        {
            UserId = id;
            Image = image;
        }

        public Guid UserId { get; set; }

        public IFormFile Image { get; set; }
    }
}
