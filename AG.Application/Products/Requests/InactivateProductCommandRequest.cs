﻿using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.Application.Products.Requests
{
    public class InactivateProductCommandRequest : IRequest<ErrorOr<bool>>
    {
        public long Id {  get; set; }
    }
}
