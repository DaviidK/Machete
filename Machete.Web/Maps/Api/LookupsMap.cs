﻿using AutoMapper;
using Machete.Domain;
using Machete.Web.ViewModel.Api;

namespace Machete.Web.Maps.Api
{
    public class LookupsMap : Profile
    {
        public LookupsMap()
        {
            CreateMap<Lookup, LookupVM>()
                .ForMember(v => v.id, opt => opt.MapFrom(d => d.ID))
                ;
        }
    }
}