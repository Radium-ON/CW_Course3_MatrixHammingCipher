﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace MatrixHammingCipher.Core
{
    public class RepairedCodeSentEvent:PubSubEvent<List<byte[]>>
    {
    }
}