﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SaaSEqt.eShop.Services.IdentityManagement.API.Models;
using UserManagement.Models;

namespace UserManagement.ViewModels
{
    public class SetCustomPropsVm
    {
        [Required]
        public Continents Continent { get; set; }

        [Required]
        public ExperienceLevels ExperienceLevel { get; set; }
    }
}