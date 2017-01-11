using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace FWCards.Config
{
    public class InputService
    {
        public VirtualAxis HorizontalAxis { get; private set; }
        public VirtualAxis VerticalAxis { get; private set; }



        public InputService()
        {
            setupInput();
        }

        private void setupInput()
        {
            HorizontalAxis = new VirtualAxis();
            HorizontalAxis.nodes.Add(new Nez.VirtualAxis.GamePadLeftStickX());
            HorizontalAxis.nodes.Add(new Nez.VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.A, Keys.D));

            VerticalAxis = new VirtualAxis();
            VerticalAxis.nodes.Add(new Nez.VirtualAxis.GamePadLeftStickY());
            VerticalAxis.nodes.Add(new Nez.VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.W, Keys.S));

        }

        public void Dispose()
        {
            HorizontalAxis.deregister();
            VerticalAxis.deregister();
        }
    }
}
