using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cet.Core.Expression
{
    /// <summary>
    /// Rappresenta la base per un token, cioè la caratterizzazione di un nodo
    /// in un albero che rappresenta un'espressione
    /// </summary>
    /// <remarks>
    /// E' importante che le derivazioni finali di un token risultino immutabili.
    /// Il motivo sta essenzialmente nella possibilità di fare riferimento alle istanze
    /// senza problemi a seguito di clonazioni dei nodi.
    /// </remarks>
    public abstract class XToken
    {
        protected XToken() : this(0, 0, null) { }

        protected XToken(object? data) : this(0, 0, data) { }

        protected XToken(
            int arity,
            int prio,
            object? data
            )
        {
            this.Arity = arity;
            this.Prio = prio;
            this.Data = data;
        }

        /// <summary>
        /// Numero argomenti nercessari all'operatore
        /// </summary>
        public int Arity { get; }

        /// <summary>
        /// Priorità dell'operatore
        /// Più alto è il valore e più è prioritario
        /// </summary>
        public int Prio { get; }
        public object? Data { get; }


        public abstract XSolverResult Resolve(IXSolverContext context, XTreeNodeBase? na, XTreeNodeBase? nb, XTreeNodeBase? nc);


        public override string ToString()
        {
            return $"{this.GetType().Name}: {this.Data}";
        }
    }
}
