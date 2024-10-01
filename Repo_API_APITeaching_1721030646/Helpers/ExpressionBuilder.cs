﻿using System.Linq.Expressions;

namespace Repo_API_1721030646.Helpers
{
    public class ExpressionBuilder
    {
        // Có thể áp dụng lấy column Name dùng cho Order by ColumnName
        public static Expression<Func<T, object>> GetPropertyLambda<T>(string propertyName)
        {
            // Tạo một tham số cho biểu thức (đại diện cho một thực thể kiểu T)
            var parameter = Expression.Parameter(typeof(T), "e");

            // Tạo biểu thức truy cập thuộc tính dựa trên tên thuộc tính
            var property = Expression.Property(parameter, propertyName);

            // Chuyển đổi thuộc tính sang kiểu object
            var converted = Expression.Convert(property, typeof(object));

            // Tạo và trả về biểu thức lambda
            return Expression.Lambda<Func<T, object>>(converted, parameter);
        }
    }
}
