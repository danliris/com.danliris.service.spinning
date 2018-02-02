using Com.Danliris.Service.Spinning.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Com.Danliris.Service.Spinning.Lib.Helpers;
using System.Linq.Dynamic.Core;
using System.Reflection;
using Com.Moonlay.NetCore.Lib;
using Com.Danliris.Service.Spinning.Lib.ViewModels;
using Com.Danliris.Service.Spinning.Lib.Interfaces;
using Com.Danliris.Service.Spinning.Lib;

namespace Com.Danliris.Service.Spinning.Lib.Services
{
    public class YarnService : BasicService<SpinningDbContext, Yarn>, IMap<Yarn, YarnViewModel>
    {
        public YarnService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override Tuple<List<Yarn>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<Yarn> Query = this.DbContext.Yarns;

            List<string> SearchAttributes = new List<string>()
                {
                    "Name"
                };
            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
                {
                    "Id", "Code", "Name"
                };
            Query = Query
                .Select(b => new Yarn
                {
                    Id = b.Id,
                    Code = b.Code,
                    Name = b.Name,
                });

            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            Pageable<Yarn> pageable = new Pageable<Yarn>(Query, Page - 1, Size);
            List<Yarn> Data = pageable.Data.ToList<Yarn>();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public override void OnCreating(Yarn model)
        {
            do
            {
                model.Code = CodeGenerator.GenerateCode();
            }
            while (this.DbSet.Any(d => d.Code.Equals(model.Code)));

            base.OnCreating(model);
        }

        public YarnViewModel MapToViewModel(Yarn model)
        {
            YarnViewModel viewModel = new YarnViewModel();
            PropertyCopier<Yarn, YarnViewModel>.Copy(model, viewModel);
            return viewModel;
        }

        public Yarn MapToModel(YarnViewModel viewModel)
        {
            Yarn model = new Yarn();
            PropertyCopier<YarnViewModel, Yarn>.Copy(viewModel, model);
            return model;
        }
    }
}
