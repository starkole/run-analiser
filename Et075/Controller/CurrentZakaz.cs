using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Et075.Model;

namespace Et075.Controller
{
    public class CurrentZakaz : BindingList<Etyketka>
    {
        public CurrentZakaz()
            : base() { }

        public CurrentZakaz(IList<Etyketka> collection)
            : base(collection) { }

        public Zakaz ToZakaz()
        { 
            return new Zakaz(this); 
        }

    }
}
