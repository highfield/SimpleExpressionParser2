using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using XP = System.Linq.Expressions;
using System.Reflection;

namespace Cet.Core.Expression
{
    public class XTreeLinqExpressionSerializer
        : IXTreeSerializer<XP.Expression>
    {
        public XP.Expression? Serialize(XTreeNodeBase xtree, object? context)
        {
            return this.Serialize(
                xtree,
                context as SerializationContext ?? throw new ArgumentNullException(nameof(context))
                );
        }


        private XP.Expression? Serialize(XTreeNodeBase xtree, SerializationContext ctx)
        {
            if (xtree is XTreeNodeTerminal term)
            {
                switch (term.Token)
                {
                    case XTokenNumber _: return XP.Expression.Constant((double)term.Token.Data!);
                    case XTokenString _: return XP.Expression.Constant((string)term.Token.Data!);
                    case XTokenBoolean _: return XP.Expression.Constant((bool)term.Token.Data!);
                    case XTokenTimeSpan _: return XP.Expression.Constant((TimeSpan)term.Token.Data!);
                    case XTokenDateTime _: return XP.Expression.Constant((DateTime)term.Token.Data!);
                    case XTokenDateTimeOffset _: return XP.Expression.Constant((DateTimeOffset)term.Token.Data!);
                    case XTokenNull _: return XP.Expression.Constant(null);
                    case XTokenRefId _:
                        return XP.Expression.PropertyOrField(
                            ctx.Parameters[0],
                            term.Token.Data as string ?? string.Empty
                            );
                    case XTokenMatchParam matchToken:
                        {
                            RegexOptions options = RegexOptions.None;
                            if (matchToken.Flags.Contains('i')) options |= RegexOptions.IgnoreCase;

                            var regex = new Regex((string?)term.Token.Data ?? string.Empty, options);
                            return XP.Expression.Constant(regex);
                        }
                }
            }
            else if (xtree is XTreeNodeUnary unary)
            {
                switch (unary.Token)
                {
                    case XTokenOperUnaryPlus _: return this.Serialize(unary.Child, ctx);
                    case XTokenOperUnaryMinus _:
                        return XP.Expression.Negate(
                            this.Serialize(unary.Child, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );
                    case XTokenOperLogicalNot _:
                        return XP.Expression.Not(
                            this.Serialize(unary.Child, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );
                    case XTokenOperBitwiseNot _:
                        return XP.Expression.Not(
                            this.Serialize(unary.Child, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );
                }
            }
            else if (xtree is XTreeNodeBinary binary)
            {
                switch (binary.Token)
                {
                    case XTokenOperAdd _:
                        return XP.Expression.AddChecked(
                            this.Serialize(binary.LeftChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize)),
                            this.Serialize(binary.RightChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );
                    case XTokenOperSub _:
                        return XP.Expression.SubtractChecked(
                            this.Serialize(binary.LeftChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize)),
                            this.Serialize(binary.RightChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );
                    case XTokenOperMul _:
                        return XP.Expression.MultiplyChecked(
                            this.Serialize(binary.LeftChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize)),
                            this.Serialize(binary.RightChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );
                    case XTokenOperDiv _:
                        return XP.Expression.Divide(
                            this.Serialize(binary.LeftChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize)),
                            this.Serialize(binary.RightChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );
                    case XTokenOperMod _:
                        return XP.Expression.Modulo(
                            this.Serialize(binary.LeftChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize)),
                            this.Serialize(binary.RightChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );
                    case XTokenOperLogicalOr _:
                        return XP.Expression.OrElse(
                            this.Serialize(binary.LeftChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize)),
                            this.Serialize(binary.RightChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );
                    case XTokenOperLogicalAnd _:
                        return XP.Expression.AndAlso(
                            this.Serialize(binary.LeftChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize)),
                            this.Serialize(binary.RightChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );

                    case XTokenOperBitwiseAnd _:
                        return XP.Expression.And(
                            this.Serialize(binary.LeftChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize)),
                            this.Serialize(binary.RightChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );
                    case XTokenOperBitwiseOr _:
                        return XP.Expression.Or(
                            this.Serialize(binary.LeftChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize)),
                            this.Serialize(binary.RightChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );
                    case XTokenOperBitwiseXor _:
                        return XP.Expression.ExclusiveOr(
                            this.Serialize(binary.LeftChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize)),
                            this.Serialize(binary.RightChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );

                    case XTokenOperEqual _:
                        return XP.Expression.Equal(
                            this.Serialize(binary.LeftChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize)),
                            this.Serialize(binary.RightChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );
                    case XTokenOperNotEqual _:
                        return XP.Expression.NotEqual(
                            this.Serialize(binary.LeftChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize)),
                            this.Serialize(binary.RightChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );
                    case XTokenOperLessThan _:
                        return XP.Expression.LessThan(
                            this.Serialize(binary.LeftChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize)),
                            this.Serialize(binary.RightChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );
                    case XTokenOperLessOrEqualThan _:
                        return XP.Expression.LessThanOrEqual(
                            this.Serialize(binary.LeftChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize)),
                            this.Serialize(binary.RightChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );
                    case XTokenOperGreaterThan _:
                        return XP.Expression.GreaterThan(
                            this.Serialize(binary.LeftChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize)),
                            this.Serialize(binary.RightChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );
                    case XTokenOperGreaterOrEqualThan _:
                        return XP.Expression.GreaterThanOrEqual(
                            this.Serialize(binary.LeftChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize)),
                            this.Serialize(binary.RightChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize))
                            );
                    case XTokenOperMatch _:
                        {
                            var xleft = this.Serialize(binary.LeftChild, ctx) ?? throw new NullReferenceException(nameof(this.Serialize));
                            var xright = this.Serialize(binary.RightChild, ctx);
                            if (xright is XP.ConstantExpression xconst &&
                                xconst.Type == typeof(Regex) &&
                                typeof(Regex).GetMethod("IsMatch", new Type[] { typeof(string) }) is MethodInfo methodInfo
                                )
                            {
                                return XP.Expression.Call(xconst, methodInfo, xleft);
                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                }
            }
            throw new NotSupportedException();
        }


        internal class SerializationContext
        {
            private readonly List<XP.ParameterExpression> _parameters = new List<XP.ParameterExpression>();
            internal IReadOnlyList<XP.ParameterExpression> Parameters => this._parameters;

            public void AddParameter(Type type, string? name = null)
            {
                this._parameters.Add(
                    XP.Expression.Parameter(type, name)
                    );
            }
        }

    }
}
