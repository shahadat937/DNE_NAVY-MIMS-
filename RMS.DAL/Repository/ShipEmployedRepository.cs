﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class ShipEmployedRepository : Repository<ShipEmployed>, IShipEmployedRepository
    {
        public ShipEmployedRepository(RM_AGBEntities context) : base(context)
        {
        }
    }
}
