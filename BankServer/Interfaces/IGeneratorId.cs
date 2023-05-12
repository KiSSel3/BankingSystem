﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Interfaces
{
    public interface IGeneratorId
    {
        public ulong Next();
        public ulong Current();
    }
}