using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XP = System.Linq.Expressions;

namespace Cet.Core.Expression
{
    public static class ExpressionExtensions
    {

        public static XTreeNodeBase? Associate<TOper>(
            this IEnumerable<XTreeNodeBase> source
            )
            where TOper : XToken, ITokenAssociative, new()
        {
            XTreeNodeBase? result = null;
            var iter = source.GetEnumerator();
            while (iter.MoveNext())
            {
                if (result == null)
                {
                    result = iter.Current;
                }
                else
                {
                    result = new XTreeNodeBinary(
                        new TOper(),
                        result,
                        iter.Current
                        );
                }
            }
            return result;
        }


        public static Func<TIn, object?>? GetLambda<TIn>(
            this XTreeNodeBase resolver,
            XReferenceSolverBase<TIn> refres
            )
        {
            if (resolver == null) return null;

            return input =>
            {
                refres.Context = input;
                XSolverResult result = resolver.Resolve(
                   new MyResolverContext() { ReferenceSolver = refres }
                   );

                return result.Error == null
                    ? result.Data
                    : throw result.Error;
            };
        }


        public static Func<TIn, TOut?>? GetLambda<TIn, TOut>(
            this XTreeNodeBase resolver,
            XReferenceSolverBase<TIn> refres
            )
        {
            if (resolver == null) return null;

            var rc = new MyResolverContext() { ReferenceSolver = refres };

            return input =>
            {
                refres.Context = input;
                XSolverResult result = resolver.Resolve(rc);

                return result.Error == null
                    ? (TOut?)result.Data
                    : throw result.Error;
            };
        }


        public static XP.Expression<Func<TIn, TOut>>? GetLinqLambda<TIn, TOut>(
            this XTreeNodeBase resolver
            )
        {
            if (resolver == null) return null;

            var ctx = new XTreeLinqExpressionSerializer.SerializationContext();
            ctx.AddParameter(typeof(TIn));

            XP.Expression? expression = new XTreeLinqExpressionSerializer()
                .Serialize(resolver, ctx);

            if (expression != null)
            {
                return XP.Expression.Lambda<Func<TIn, TOut>>(expression, ctx.Parameters);
            }
            else
            {
                throw new NullReferenceException(nameof(expression));
            }
        }


        public static XTreeNodeBase Clone(
            this XTreeNodeBase source,
            IXTreeMappingVisitor? mapping = null
            )
        {
            XTreeNodeBase visit(XTreeNodeBase node)
            {
                if (node is XTreeNodeTerminal term)
                {
                    return mapping?.MapTerminal(term) ?? new XTreeNodeTerminal(term.Token);
                }
                else if (node is XTreeNodeUnary unary)
                {
                    XTreeNodeUnary? newNode = mapping?.MapUnary(unary);
                    if (newNode == null || newNode == unary)
                    {
                        XTreeNodeBase visitedChild = visit(unary.Child);
                        return new XTreeNodeUnary(unary.Token, visitedChild);
                    }
                    else
                    {
                        return newNode;
                    }
                }
                else if (node is XTreeNodeBinary binary)
                {
                    XTreeNodeBinary? newNode = mapping?.MapBinary(binary);
                    if (newNode == null || newNode == binary)
                    {
                        XTreeNodeBase visitedLeftChild = visit(binary.LeftChild);
                        XTreeNodeBase visitedRightChild = visit(binary.RightChild);
                        return new XTreeNodeBinary(binary.Token, visitedLeftChild, visitedRightChild);
                    }
                    else
                    {
                        return newNode;
                    }
                }
                else
                {
                    throw new NotSupportedException();
                }
            }

            return visit(source);
        }


        //public static XTreeNodeBase RemapReferences(
        //    this XTreeNodeBase source,
        //    IReadOnlyDictionary<string, string> map
        //    )
        //{
        //    XTreeNodeBase projection(XTreeNodeBase origNode)
        //    {
        //        if (origNode is XTreeNodeTerminal term &&
        //            term.Token is XTokenRefId token &&
        //            map.TryGetValue(token.Data as string, out string alias)
        //            )
        //        {
        //            return new XTreeNodeTerminal(
        //                new XTokenRefId(alias)
        //                );
        //        }
        //        return origNode;
        //    }
        //    return Map(source, projection);
        //}


        //public static XTreeNodeBase RemapReferences(
        //    this XTreeNodeBase source,
        //    Func<string, string> map
        //    )
        //{
        //    XTreeNodeBase projection(XTreeNodeBase origNode)
        //    {
        //        if (origNode is XTreeNodeTerminal term &&
        //            term.Token is XTokenRefId token
        //            )
        //        {
        //            string alias = map(token.Data as string);
        //            if (string.IsNullOrEmpty(alias) == false)
        //            {
        //                return new XTreeNodeTerminal(
        //                    new XTokenRefId(alias)
        //                    );
        //            }
        //        }
        //        return origNode;
        //    }
        //    return Map(source, projection);
        //}


        private class MyResolverContext : IXSolverContext
        {
            public IXReferenceSolver? ReferenceSolver { get; set; }
        }

    }
}
