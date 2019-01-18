using System.Collections.Generic;

namespace slx.mvc
{
    interface IViewThingy
    {
        string Name { get; set; }
        List<object> Views { get; set; }
        IController Controller { get; set; }
    }


}