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

            List<string> SearchAttributes = new List<string>()
            {
                "Lot", "UnitName", "YarnName", "MachineName"
            };

            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            /* Const Select */
            List<string> SelectedFields = new List<string>()
            {
                "Id", "Code", "Lot", "Unit", "Yarn", "Machine"
            };

            Query = Query
                .Select(o => new LotYarn
                {
                    Id = o.Id,
                    Code = o.Code,
                    Lot = o.Lot,
                    UnitId = o.UnitId,
                    UnitCode = o.UnitCode,
                    UnitName = o.UnitName,
                    YarnId = o.YarnId,
                    YarnCode = o.YarnCode,
                    YarnName = o.YarnName,
                    MachineId = o.MachineId,
                    MachineCode = o.MachineCode,
                    MachineName = o.MachineName
                });

            /* Order */
            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            /* Pagination */
            Pageable<LotYarn> pageable = new Pageable<LotYarn>(Query, Page - 1, Size);
            List<LotYarn> Data = pageable.Data.ToList<LotYarn>();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

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
            viewModel.Yarn = new LotYarnViewModel.YarnVM();
            viewModel.Yarn.Id = model.YarnId;
            viewModel.Yarn.Code = model.YarnCode;
            viewModel.Yarn.Name = model.YarnName;
            viewModel.Machine = new LotYarnViewModel.MachineVM();
            viewModel.Machine._id = model.MachineId;
            viewModel.Machine.code = model.MachineCode;
            viewModel.Machine.name = model.MachineName;
            viewModel.Unit = new LotYarnViewModel.UnitVM();
            viewModel.Unit._id = model.UnitId;
            viewModel.Unit.code = model.UnitCode;
            viewModel.Unit.name = model.UnitName;
            return viewModel;
        }

        public LotYarn MapToModel(LotYarnViewModel viewModel)
        {
            LotYarn model = new LotYarn();

            PropertyCopier<LotYarnViewModel, LotYarn>.Copy(viewModel, model);

            model.YarnId = viewModel.Yarn.Id != null ? (int)viewModel.Yarn.Id : 0;
            model.YarnCode = viewModel.Yarn.Code;
            model.YarnName = viewModel.Yarn.Name;
            model.MachineId = viewModel.Machine._id;
            model.MachineCode = viewModel.Machine.code;
            model.MachineName = viewModel.Machine.name;
            model.UnitId = viewModel.Unit._id;
            model.UnitCode = viewModel.Unit.code;
            model.UnitName = viewModel.Unit.name;

            return model;
        }

        public class Keys
        {

        }

        public async Task<LotYarn> ReadModelByQuery(string Spinning, string Machine, string Yarn)
        {
            LotYarn result = new LotYarn();
            result = await this.DbSet.Where(lotYarn => String.Equals(lotYarn.UnitName, Spinning) && String.Equals(Machine, lotYarn.MachineName) && String.Equals(Yarn, lotYarn.YarnName)).OrderByDescending(x => x._LastModifiedUtc).FirstOrDefaultAsync();

            //.FirstOrDefaultAsync(lotYarn => String.Equals(lotYarn.UnitName, Spinning) && String.Equals(Machine, lotYarn.MachineName) && String.Equals(Yarn, lotYarn.YarnName)); &&  && String.Equals(x.MachineName, Machine)
            if (result == null)
            {
                result = new LotYarn();
                return result;
            }
            return result;
        }

    }
}
