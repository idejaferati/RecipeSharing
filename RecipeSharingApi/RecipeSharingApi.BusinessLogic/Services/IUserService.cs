﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.BusinessLogic.Services
{
    public interface IUserService
    {
        //string GetMyId();
        Guid GetMyId();
    }
}
