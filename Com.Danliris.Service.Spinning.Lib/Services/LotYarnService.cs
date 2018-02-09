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
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Com.Danliris.Service.Spinning.Lib.Services
{
    public class LotYarnService : BasicService<SpinningDbContext, LotYarn>, IMap<LotYarn, LotYarnViewModel>
    {
        public LotYarnService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override Tuple<List<LotYarn>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<LotYarn> Query = this.DbContext.LotYarns;
            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);

            if (Keyword != null)
            {
                List<string> SearchAttributes = new List<string>()
                {
                    "Name"
                };

                Query = ConfigureSearch(Query, SearchAttributes, Keyword);
            }

            /* Const Select */
            List<string> SelectedFields = new List<string>()
            {
                "_id","Code","Name" ,"Unit"
            };

            Query = Query
                .Select(o => new LotYarn
                {
                    Id = o.Id,
                    Code = o.Code,
                    Name = o.Name,
                    YarnId = o.YarnId,
                    Yarn = new Yarn
                    {
                        Id = o.Yarn.Id,
                        Code = o.Yarn.Code,
                        Name = o.Yarn.Name,
                    },
     
                });

            /* Order */
            if (OrderDictionary.Count.Equals(0))
            {
                OrderDictionary.Add("_updatedDate", General.DESCENDING);

                Query = Query.OrderByDescending(b => b._LastModifiedUtc); /* Default Order */
            }
            else
            {
                string Key = OrderDictionary.Keys.First();
                string OrderType = OrderDictionary[Key];
                string TransformKey = General.TransformOrderBy(Key);

                BindingFlags IgnoreCase = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;

                Query = OrderType.Equals(General.ASCENDING) ?
                    Query.OrderBy(b => b.GetType().GetProperty(TransformKey, IgnoreCase).GetValue(b)) :
                    Query.OrderByDescending(b => b.GetType().GetProperty(TransformKey, IgnoreCase).GetValue(b));
            }

            /* Pagination */
            Pageable<LotYarn> pageable = new Pageable<LotYarn>(Query, Page - 1, Size);
            List<LotYarn> Data = pageable.Data.ToList<LotYarn>();

            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        //public async Task<List<LotYarn>> ReadModelOnUnit(int Id)
        //{
        //    return await this.DbSet
        //        .Where(m => m.YarnId.Equals(Id) && m._IsDeleted == false)
        //        .ToListAsync();
        //}

        //public override async Task<LotYarn> ReadModelById(int Id)
        //{
        //    return await this.DbSet
        //        .Where(m => m.Id.Equals(Id) && m._IsDeleted == false)
        //        .Include(m => m.YarnId)
        //        .FirstOrDefaultAsync();
        //}

        public override void OnCreating(LotYarn model)
        {
            do
            {
                model.Code = CodeGenerator.GenerateCode();
            }
            while (this.DbSet.Any(d => d.Code.Equals(model.Code)));

            base.OnCreating(model);
        }

        public LotYarnViewModel MapToViewModel(LotYarn model)
        {
            LotYarnViewModel viewModel = new LotYarnViewModel();
            PropertyCopier<LotYarn, LotYarnViewModel>.Copy(model, viewModel);
            return viewModel;
        }

        public LotYarn MapToModel(LotYarnViewModel viewModel)
        {
            LotYarn model = new LotYarn();

            PropertyCopier<LotYarnViewModel, LotYarn>.Copy(viewModel, model);

            model.YarnId = viewModel.Yarn.Id;
            model.UnitId = viewModel.Unit.Id;

            return model;
        }

    }
}
