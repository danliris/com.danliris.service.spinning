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

namespace Com.Danliris.Service.Spinning.Lib.Services
{
    public class SpinningInputProductionService : BasicService<SpinningDbContext, SpinningInputProduction>, IMap<SpinningInputProduction, SpinningInputProductionViewModel>
    {
        public SpinningInputProductionService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public SpinningInputProductionViewModel MapToViewModel(SpinningInputProduction model)
        {
            SpinningInputProductionViewModel viewModel = new SpinningInputProductionViewModel();
            PropertyCopier<SpinningInputProduction, SpinningInputProductionViewModel>.Copy(model, viewModel);
            viewModel.NomorInputProduksi = model.NomorInputProduksi;
            viewModel.Date = model.Date;
            viewModel.Shift = model.Shift;
            viewModel.YarnId = model.YarnId;
            viewModel.YarnName = model.YarnName;
            viewModel.MachineId = model.MachineId;
            viewModel.MachineName = model.MachineName;
            viewModel.UnitId = model.UnitId;
            viewModel.UnitName = model.UnitName;
            viewModel.Lot = model.Lot;
            viewModel.Counter = model.Counter;
            viewModel.Hank = model.Hank;

            return viewModel;
        }

        public SpinningInputProduction MapToModel(SpinningInputProductionViewModel viewModel)
        {
            SpinningInputProduction model = new SpinningInputProduction();
            PropertyCopier<SpinningInputProductionViewModel, SpinningInputProduction>.Copy(viewModel, model);
            model.Date = (DateTime)viewModel.Date;
            model.Shift = viewModel.Shift;
            model.YarnId = viewModel.YarnId;
            model.YarnName = viewModel.YarnName;
            model.MachineId = viewModel.MachineId;
            model.MachineName = viewModel.MachineName;
            model.YarnId = viewModel.YarnId;
            model.YarnName = viewModel.YarnName;
            model.Lot = viewModel.Lot;
            model.Counter = (double)viewModel.Counter;
            model.Hank = (double)viewModel.Hank;

            return model;
        }

        public override Tuple<List<SpinningInputProduction>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<SpinningInputProduction> Query = this.DbContext.SpinningInputProductions;
            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);

            if (Keyword != null)
            {
                List<string> SearchAttributes = new List<string>()
                {
                    "NomorInputProduksi","YarnName","UnitName","MachineName","Lot"
                };

                Query = ConfigureSearch(Query, SearchAttributes, Keyword);
            }

            /* Const Select */
            List<string> SelectedFields = new List<string>()
            {
               "Id","NomorInputProduksi","YarnName","UnitName","MachineName","Lot","Shift","Date","Counter","Hank"
            };

            Query = Query
                .Select(o => new SpinningInputProduction
                {
                    Id = o.Id,
                    NomorInputProduksi = o.NomorInputProduksi,
                    YarnName=o.YarnName,
                    UnitName=o.UnitName,
                    MachineName=o.MachineName,
                    Lot=o.Lot,
                    Shift=o.Shift,
                    Date=o.Date,
                    Counter=o.Counter,
                    Hank=o.Hank,
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
            Pageable<SpinningInputProduction> pageable = new Pageable<SpinningInputProduction>(Query, Page - 1, Size);
            List<SpinningInputProduction> Data = pageable.Data.ToList<SpinningInputProduction>();

            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }


        public override void OnCreating(SpinningInputProduction model)
        {
            do
            {
                model.NomorInputProduksi = CodeGenerator.GenerateCode();
            }
            while (this.DbSet.Any(d => d.NomorInputProduksi.Equals(model.NomorInputProduksi)));

            base.OnCreating(model);
        }

        public async Task<int> CreateModels(List<SpinningInputProduction> models)
        {
            int created = 0;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (SpinningInputProduction model in models)
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



    }

}

