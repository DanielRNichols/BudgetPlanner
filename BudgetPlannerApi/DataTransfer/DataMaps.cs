using AutoMapper;
using BudgetPlannerApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.DataTransfer
{
    public class DataMaps : Profile
    {
        public DataMaps()
        {
            CreateMap<BudgetItemType, BudgetItemTypeDTO>().ReverseMap();
            CreateMap<BudgetItemType, BudgetItemTypeCreateDTO>().ReverseMap();

            CreateMap<BudgetItemGroup, BudgetItemGroupDTO>().ReverseMap();
            CreateMap<BudgetItemGroup, BudgetItemGroupCreateDTO>().ReverseMap();

            CreateMap<BudgetItem, BudgetItemDTO>().ReverseMap();
            CreateMap<BudgetItem, BudgetItemCreateDTO>().ReverseMap();
        }
    }
}
