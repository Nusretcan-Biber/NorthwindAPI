using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Utils.AutoMapper
{
    public class MappingProfile<TSource,TDestination> : Profile
    {
        private static readonly Lazy<MappingProfile<TSource, TDestination>> _instance = new Lazy<MappingProfile<TSource, TDestination>>(() => new MappingProfile<TSource, TDestination>());
        public static readonly MappingProfile<TSource,TDestination> Instance = _instance.Value;


        public MappingProfile()
        {
            CreateMap<TSource, TDestination>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            _mapper = config.CreateMapper();
            
        }
        private IMapper _mapper;
        public IMapper Mapper { get { return _mapper; } set { _mapper = value; } }
    }
}

