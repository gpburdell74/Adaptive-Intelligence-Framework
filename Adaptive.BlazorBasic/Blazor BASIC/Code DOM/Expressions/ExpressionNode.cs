using Adaptive.Intelligence.BlazorBasic.CodeDom.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptive.Intelligence.BlazorBasic.CodeDom;


public class ExpressionNode
{
    public BasicExpression Expression { get; set; }
    public BlazorBasicMathOperators Operator { get; set; } = BlazorBasicMathOperators.Add;
    public ExpressionNode? Prev { get; set; }
    public ExpressionNode? Next { get; set; }
}
