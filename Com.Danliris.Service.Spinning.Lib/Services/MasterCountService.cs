using Com.Danliris.Service.Spinning.Lib.Helpers;
using Com.Danliris.Service.Spinning.Lib.Interfaces;
using Com.Danliris.Service.Spinning.Lib.Models;
using Com.Danliris.Service.Spinning.Lib.ViewModels;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Spinning.Lib.Services
{
    public class MasterCountService : BasicService<SpinningDbContext, MasterCount>, IMap<MasterCount, MasterCountViewModel>
    {
        public MasterCountService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override Tuple<List<MasterCount>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<MasterCount> Query = this.DbContext.MasterCounts;

            List<string> SearchAttributes = new List<string>()
            {
                "Count", "Remark"
            };

            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "Count", "Remark"
            };

            Query = Query.Select(o => new MasterCount
            {
                Id = o.Id,
                Count = o.Count,
                Remark = o.Remark,
            });

            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            /* Pagination */
            Pageable<MasterCount> pageable = new Pageable<MasterCount>(Query, Page - 1, Size);
            List<MasterCount> Data = pageable.Data.ToList<MasterCount>();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public override void OnCreating(MasterCount model)
        {
            base.OnCreating(model);
        }

        public async Task<int> CreateModel(List<MasterCount> models)
        {
            int created = 0;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach(MasterCount model in models)
                    {
                        created = await this.CreateAsync(model);
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            return created;
        }

        public MasterCountViewModel MapToViewModel(MasterCount model)
        {
            MasterCountViewModel viewModel = new MasterCountViewModel();
            PropertyCopier<MasterCount, MasterCountViewModel>.Copy(model, viewModel);
            return viewModel;
        }

        public MasterCount MapToModel(MasterCountViewModel viewModel)
        {
            MasterCount model = new MasterCount();
            PropertyCopier<MasterCountViewModel, MasterCount>.Copy(viewModel, model);
            return model;
        }
    }
}
