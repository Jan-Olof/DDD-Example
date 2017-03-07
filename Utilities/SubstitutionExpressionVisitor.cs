using System.Linq.Expressions;

namespace Utilities
{
    /// <summary>
    /// Represents a visitor or rewriter for expression trees.
    /// </summary>
    public class SubstitutionExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _after;
        private readonly Expression _before;

        public SubstitutionExpressionVisitor(Expression before, Expression after)
        {
            _before = before;
            _after = after;
        }

        public override Expression Visit(Expression node)
        {
            return node == _before ? _after : base.Visit(node);
        }
    }
}