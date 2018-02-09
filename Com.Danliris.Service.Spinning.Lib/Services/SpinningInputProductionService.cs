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
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Com.Danliris.Service.Spinning.Lib.Services
{
    public class SpinningInputProductionService : BasicService<SpinningDbContext, SpinningInputProduction>, IMap<SpinningInputProduction, SpinningInputProductionViewModel>
    {
        public SpinningInputProductionService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public SpinningInputProduction MapToModel(SpinningInputProductionViewModel viewModel)
        {

            SpinningInputProduction model = new SpinningInputProduction();
            model.Input = new List<SpinningInputProduction_InputDetails>();
            PropertyCopier<SpinningInputProductionViewModel, SpinningInputProduction>.Copy(viewModel, model);

            foreach (SpinningInputProductionViewModel.InputModel inputVM in viewModel.Input)
            {

                SpinningInputProduction_InputDetails input = new SpinningInputProduction_InputDetails();
                PropertyCopier<SpinningInputProductionViewModel.InputModel, SpinningInputProduction_InputDetails>.Copy(inputVM, input);
                model.Input.Add(input);
            }
            return model;

        }

        public SpinningInputProductionViewModel MapToViewModel(SpinningInputProduction model)
        {
            SpinningInputProductionViewModel viewModel = new SpinningInputProductionViewModel();
            viewModel.Input = new List<SpinningInputProductionViewModel.InputModel>();
            PropertyCopier<SpinningInputProduction, SpinningInputProductionViewModel>.Copy(model, viewModel);

            foreach (SpinningInputProductionViewModel.InputModel inputeVM in viewModel.Input)
            {
                SpinningInputProduction_InputDetails input = new SpinningInputProduction_InputDetails();
                PropertyCopier<SpinningInputProductionViewModel.InputModel, SpinningInputProduction_InputDetails>.Copy(inputeVM, input);
                model.Input.Add(input);
            }

            return viewModel;
        }

        public override Tuple<List<SpinningInputProduction>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<SpinningInputProduction> Query = this.DbContext.SpinningInputProductions;
            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);

            if (Keyword != null)
            {
                List<string> SearchAttributes = new List<string>()
                {
                    "NomorInputProduksi"
                };

                Query = ConfigureSearch(Query, SearchAttributes, Keyword);
            }

            /* Const Select */
            List<string> SelectedFields = new List<string>()
            {
                "_id","NomorInputProduksi"
            };

            Query = Query
                .Select(o => new SpinningInputProduction
                {
                    Id = o.Id,
                    NomorInputProduksi = o.NomorInputProduksi,

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

            if (model.Input.Count > 0)
            {
                SpinningInputProduction_InputDetailsService spinningInputProduction_InputDetailsService = this.ServiceProvider.GetService<SpinningInputProduction_InputDetailsService>();
                foreach (SpinningInputProduction_InputDetails input in model.Input)
                {
                    spinningInputProduction_InputDetailsService.OnCreating(input);
                }
            }


            base.OnCreating(model);
        }

        public override async Task<int> UpdateModel(int Id, SpinningInputProduction Model)
        {
            SpinningInputProduction_InputDetailsService spinningInputProduction_InputDetailsService = this.ServiceProvider.GetService<SpinningInputProduction_InputDetailsService>();

            int updated = 0;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    HashSet<int> input = new HashSet<int>(spinningInputProduction_InputDetailsService.DbSet
                        .Where(p => p.SpinningInputProductionId.Equals(Id))
                        .Select(p => p.Id));
                    updated = await this.UpdateAsync(Id, Model);

                    foreach (int data in input)
                    {
                        SpinningInputProduction_InputDetails spinningInput = Model.Input.FirstOrDefault(prop => prop.Id.Equals(data));

                        if (spinningInput == null)
                        {
                            await spinningInputProduction_InputDetailsService.DeleteModel(data);
                        }

                    }

                    foreach (SpinningInputProduction_InputDetails spinningInput in Model.Input)
                    {
                        if (spinningInput.Id != null && spinningInput.Id != 0)
                        {
                            await spinningInputProduction_InputDetailsService.UpdateModel(spinningInput.Id,spinningInput);
                        }
                        else if (spinningInput.Id.Equals(0))
                        {
                            await spinningInputProduction_InputDetailsService.CreateModel(spinningInput);
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                }
            }

            return updated;
        }

        public override async Task<SpinningInputProduction> ReadModelById(int id)
        {
            return await this.DbSet
                .Where(d => d.Id.Equals(id))
                .Include(o => o.Input)
                .FirstOrDefaultAsync();
        }

    }
}
