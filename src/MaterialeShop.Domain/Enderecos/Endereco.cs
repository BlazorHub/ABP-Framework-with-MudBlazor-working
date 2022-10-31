using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace MaterialeShop.Enderecos
{
    public class Endereco : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string EnderecoCompleto { get; set; }

        public Endereco()
        {

        }

        public Endereco(Guid id, string enderecoCompleto)
        {

            Id = id;
            Check.NotNull(enderecoCompleto, nameof(enderecoCompleto));
            EnderecoCompleto = enderecoCompleto;
        }

    }
}