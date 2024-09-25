﻿using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
   public class ShipInactiveOrgRepository:Repository<ShipInactiveOrg>,IShipInactiveOrgRepository
    {
       public ShipInactiveOrgRepository(RM_AGBEntities context)
           : base(context)
        {
        }
    }
}
