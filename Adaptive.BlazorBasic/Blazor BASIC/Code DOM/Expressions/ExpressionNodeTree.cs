using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;

public class ExpressionNodeTree
{
    public ExpressionNode? Root { get; set; }

    public ExpressionNode? Current { get; set; }

    public ExpressionNode? MoveLast()
    {
        ExpressionNode? ptr = Root;

        if (ptr != null)
        {
            while (ptr.Next != null)
                ptr = ptr.Next;
        }
        return ptr;
    }
}
