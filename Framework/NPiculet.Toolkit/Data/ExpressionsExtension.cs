﻿using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity
{
	public static class ExpressionsExtension
	{
		#region Expression 相关扩展

		/// <summary>
		/// 返回结果为 true 的表达式
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static Expression<Func<T, bool>> True<T>() { return f => true; }

		/// <summary>
		/// 返回结果为 false 的表达式
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static Expression<Func<T, bool>> False<T>() { return f => false; }

		/// <summary>
		/// 返回 Or 运算表达式
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="expr1"></param>
		/// <param name="expr2"></param>
		/// <returns></returns>
		public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
		{
			return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, expr2.Body), expr1.Parameters);
		}

		/// <summary>
		/// 返回 And 运算表达式
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="expr1"></param>
		/// <param name="expr2"></param>
		/// <returns></returns>
		public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
		{
			return Expression.Lambda<Func<T, bool>>(Expression.And(expr1.Body, expr2.Body), expr1.Parameters);
		}

		#endregion
	}
}