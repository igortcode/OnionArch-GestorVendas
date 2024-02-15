﻿using Gestao.Core.Entidades;
using Gestao.Core.Interfaces.Repository;
using Gestao.Data.Context;

namespace Gestao.Data.Repository
{
    public class ItemComandaRepository : GenericRepository<ItemComanda>, IItemComandaRepository
    {
        public ItemComandaRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}