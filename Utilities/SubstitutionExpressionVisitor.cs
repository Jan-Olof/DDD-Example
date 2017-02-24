using System.Linq.Expressions;

namespace Utilities
{
    public class SubstitutionExpressionVisitor : ExpressionVisitor
    {
        private Expression before, after;

        public SubstitutionExpressionVisitor(Expression before, Expression after)
        {
            this.before = before;
            this.after = after;
        }

        public override Expression Visit(Expression node)
        {
            return node == before ? after : base.Visit(node);
        }
    }
}