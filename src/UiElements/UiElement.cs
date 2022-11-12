using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KouCoCoa {
    internal abstract class UiWindow {
        public bool Visible;

        public abstract void Update();
    }
}
