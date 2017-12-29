using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulus2D.Graphics
{
    public class Renderer2D
    {
        private ITarget target;

        /// <summary>
        /// Render target rendered to
        /// </summary>
        public ITarget Target { get => target; set => target = value; }

        public Renderer2D(ITarget target)
        {
            Target = target;
        }

        public void Render()
        {

        }
    }
}
