﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {

        }

        public NotFoundException(string message) : base(message)
        {

        }
    }
}
