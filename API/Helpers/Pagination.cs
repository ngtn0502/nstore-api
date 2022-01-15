using System.Collections.Generic;

namespace API.Helpers
{
   public class Pagination<T> where T : class
   {
      public Pagination(int pageSize, int pageNumber, int totalCount, IReadOnlyList<T> data)
      {
         PageSize = pageSize;
         PageNumber = pageNumber;
         TotalCount = totalCount;
         Data = data;
      }

      public int PageSize { get; set; }
      public int PageNumber { get; set; }
      public int TotalCount { get; set; }
      public IReadOnlyList<T> Data { get; set; }
   }
}