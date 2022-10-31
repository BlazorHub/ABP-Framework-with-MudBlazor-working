using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace MaterialeShop.ListaItems
{
    public class ListaItem : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Descricao { get; set; }

        [CanBeNull]
        public virtual string Quantidade { get; set; }

        [CanBeNull]
        public virtual string UnidadeMedida { get; set; }

        public ListaItem()
        {

        }

        public ListaItem(Guid id, string descricao, string quantidade, string unidadeMedida)
        {

            Id = id;
            Check.NotNull(descricao, nameof(descricao));
            Descricao = descricao;
            Quantidade = quantidade;
            UnidadeMedida = unidadeMedida;
        }

    }
}