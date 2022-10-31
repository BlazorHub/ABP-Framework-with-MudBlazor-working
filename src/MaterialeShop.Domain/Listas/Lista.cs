using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace MaterialeShop.Listas
{
    public class Lista : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Titulo { get; set; }

        public Lista()
        {

        }

        public Lista(Guid id, string titulo)
        {

            Id = id;
            Check.NotNull(titulo, nameof(titulo));
            Titulo = titulo;
        }

    }
}