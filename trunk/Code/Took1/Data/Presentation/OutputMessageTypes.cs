using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Took1.Data.Presentation
{
	/// <summary>
	/// Tipos de mensagem de saída
	/// </summary>
    public enum OutputMessageTypes : int
    {
        Info = 0, 
        Warning = 1,
        Error = 2,
        Verbose = 3,
        Event = 4

    }
}
