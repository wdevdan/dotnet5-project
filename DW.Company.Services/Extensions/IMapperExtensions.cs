using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DW.Company.Services.Extensions
{
    public static class IMapperExtensions
    {
        public static TSource ForMember<TSource, TMember>(this TSource item, Expression<Func<TSource, TMember>> destinationMember, Action<TSource, TMember> action)
        {
            var _fn = destinationMember.Compile();
            var _member = _fn(item);
            action(item, _member);
            return item;
        }

        public static IEnumerable<TSource> ForMember<TSource, TMember>(this IEnumerable<TSource> items, Expression<Func<TSource, TMember>> destinationMember, Action<TSource, TMember> action)
        {
            var _fn = destinationMember.Compile();
            foreach (var _item in items)
            {
                var _member = _fn(_item);
                action(_item, _member);
            }
            return items;
        }
    }
}
