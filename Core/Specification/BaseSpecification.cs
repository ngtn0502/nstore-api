using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specification
{
   public class BaseSpecification<T> : ISpecification<T>
   {
      public BaseSpecification()
      {
      }

      public BaseSpecification(Expression<Func<T, bool>> criteria)
      {
         Criteria = criteria;
      }

      public Expression<Func<T, bool>> Criteria { get; private set; }

      public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

      public Expression<Func<T, object>> OrderBy { get; private set; }
      public Expression<Func<T, object>> OrderByDescending { get; private set; }

      public int Take { get; private set; }

      public int Skip { get; private set; }

      public bool PaginationEnable { get; private set; }

      protected void AddInclude(Expression<Func<T, object>> includeExpression)
      {
         Includes.Add(includeExpression);
      }

      protected void AddOrderBy(Expression<Func<T, object>> orderExpression)
      {
         OrderBy = orderExpression;
      }

      protected void AddOrderByDescending(Expression<Func<T, object>> orderExpression)
      {
         OrderByDescending = orderExpression;
      }

      protected void ApplyPaging(int skip, int take)
      {
         PaginationEnable = true;
         Take = take;
         Skip = skip;
      }
   }
}