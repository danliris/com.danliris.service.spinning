using Com.Danliris.Service.Spinning.Lib.Helpers;
using Com.Danliris.Service.Spinning.Lib.Interfaces;
using Com.Danliris.Service.Spinning.Lib.Models;
using Com.Danliris.Service.Spinning.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Com.Danliris.Service.Spinning.Lib.Services
{
    public class SpinningInputProduction_InputDetailsService : BasicService<SpinningDbContext, SpinningInputProduction_InputDetails>, IMap<SpinningInputProduction_InputDetails, SpinningInputProductionViewModel.InputModel>
    {
        public SpinningInputProduction_InputDetailsService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void OnCreating(SpinningInputProduction_InputDetails model)
        {
            do
            {
                model.Code = CodeGenerator.GenerateCode();
            }
            while (this.DbSet.Any(d => d.Code.Equals(model.Code)));

            base.OnCreating(model);
        }

        public override void OnUpdating(int id, SpinningInputProduction_InputDetails model)
        {

            base.OnUpdating(id, model);
        }

        public override void OnDeleting(SpinningInputProduction_InputDetails model)
        {
            base.OnDeleting(model);
        }


        public SpinningInputProduction_InputDetails MapToModel(SpinningInputProductionViewModel.InputModel viewModel)
        {
            SpinningInputProduction_InputDetails model = new SpinningInputProduction_InputDetails();

            PropertyCopier<SpinningInputProductionViewModel.InputModel, SpinningInputProduction_InputDetails>.Copy(viewModel, model);


            return model;
        }

        public SpinningInputProductionViewModel.InputModel MapToViewModel(SpinningInputProduction_InputDetails model)
        {
            SpinningInputProductionViewModel.InputModel viewModel = new SpinningInputProductionViewModel.InputModel();
            PropertyCopier<SpinningInputProduction_InputDetails, SpinningInputProductionViewModel.InputModel>.Copy(model, viewModel);
            return viewModel;
        }

        public override Tuple<List<SpinningInputProduction_InputDetails>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<SpinningInputProduction_InputDetails> Query = this.DbContext.SpinningInputProduction_InputDetails;

            List<string> SearchAttributes = new List<string>()
                {
                    "Code"
                };
            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
                {
                    "Id", "Code"
                };
            Query = Query
                .Select(b => new SpinningInputProduction_InputDetails
                {
                    Id = b.Id,
                    Code = b.Code
                });

            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            Pageable<SpinningInputProduction_InputDetails> pageable = new Pageable<SpinningInputProduction_InputDetails>(Query, Page - 1, Size);
            List<SpinningInputProduction_InputDetails> Data = pageable.Data.ToList<SpinningInputProduction_InputDetails>();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        //public override async Task<SpinningInputProduction_InputDetails> ReadModelById(int id)
        //{
        //    return await this.DbSet
        //        .Where(d => d.Id.Equals(id))
        //        .FirstOrDefaultAsync();
        //}
    }
}
