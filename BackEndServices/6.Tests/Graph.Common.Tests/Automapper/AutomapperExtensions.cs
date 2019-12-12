using AutoMapper;
using Graph.EntityModels.Core;
using Graph.ViewModels.Core;
 
using System.Collections.Generic;
using System.Data;

namespace Graph.Common.Tests.Automapper
{
    public static class AutomapperExtensions
    {
        public static VM ToViewModel<T, VM>(this IMapper mapper, T model)
        {
            return mapper.Map<T, VM>(model);
        }

        public static T ToEntityModel<T, VM>(this IMapper mapper, VM viewModel)
        {
            return mapper.Map<VM, T>(viewModel);
        }

        public static VM ToViewModel<VM>(this IMapper mapper, BaseEntity entity) where VM : BaseViewModel
        {
            return (VM)mapper.Map(entity, entity.GetType(), typeof(VM));
        }

        public static T ToEntityModel<T>(this IMapper mapper, BaseViewModel viewModel) where T : BaseEntity
        {
            return (T)mapper.Map(viewModel, viewModel.GetType(), typeof(T));
        }

        public static IEnumerable<VM> ToViewModel<T, VM>(this IMapper mapper, IEnumerable<T> models)
        {
            return mapper.Map<IEnumerable<T>, IEnumerable<VM>>(models);
        }

        public static IEnumerable<T> ToEntityModel<T, VM>(this IMapper mapper, IEnumerable<VM> viewModels)
        {
            return mapper.Map<IEnumerable<VM>, IEnumerable<T>>(viewModels);
        }

        public static List<VM> ToViewModel<VM>(this IMapper mapper, IEnumerable<BaseEntity> entity) where VM : BaseViewModel
        {
            return (List<VM>)mapper.Map(entity, entity.GetType(), typeof(VM));
        }

        public static List<T> ToEntityModel<T>(this IMapper mapper, IEnumerable<BaseViewModel> viewModels) where T : BaseEntity
        {
            return (List<T>)mapper.Map(viewModels, viewModels.GetType(), typeof(T));
        }

        public static List<VM> ToViewModel<VM>(this IMapper mapper, List<DataRow> rows)
        {
            return mapper.Map<List<DataRow>, List<VM>>(rows);
        }

    }

}
