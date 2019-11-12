using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Events;

namespace MatrixHammingCipher.Core
{
    public class EnterTextEncodedEvent : PubSubEvent<byte[,]>
    {
        public EnterTextEncodedEvent()
        {

        }
    }
}
