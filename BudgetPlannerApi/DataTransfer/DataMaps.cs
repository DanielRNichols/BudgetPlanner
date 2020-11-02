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

            CreateMap<MemorizedTransaction, MemorizedTransactionDTO>().ReverseMap();
            CreateMap<MemorizedTransaction, MemorizedTransactionCreateDTO>().ReverseMap();

            CreateMap<Register, RegisterDTO>().ReverseMap();
            CreateMap<Register, RegisterCreateDTO>().ReverseMap();

            CreateMap<RegisterEntry, RegisterEntryDTO>().ReverseMap();
            CreateMap<RegisterEntry, RegisterEntryCreateDTO>().ReverseMap();

            CreateMap<BudgetCycle, BudgetCycleDTO>().ReverseMap();
            CreateMap<BudgetCycle, BudgetCycleCreateDTO>().ReverseMap();

            CreateMap<BudgetCycleItem, BudgetCycleItemDTO>().ReverseMap();
            CreateMap<BudgetCycleItem, BudgetCycleItemCreateDTO>().ReverseMap();
        }
    }
}
