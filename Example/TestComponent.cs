using Prota2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    class TestComponent : IComponent
    {
        public string mText;

        public TestComponent(string text)
        {
            mText = text;
        }
    }
}
