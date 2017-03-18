using System.Collections.Generic;
using System.Linq;
using O2CV1EntityDtos;
using O2V1DataAccess.models;
using O2V1Web.Models.ViewModels;

namespace O2V1Web.Mappers
{
    public static class CriteriaMapper
    {
        public static List<CriteriaGridViewModel> MapCriteriaDtoToCriteriaGridViewModel(List<CriteriaDto> criteriaDto)
        {
            return criteriaDto.Select(criteriaIn => new CriteriaGridViewModel
            {
                CompareOperator = criteriaIn.CompareOperator,
                CompareValue = criteriaIn.CompareValue,
                Description = criteriaIn.Description,
                Disabled = criteriaIn.Disabled,
                Name = criteriaIn.Name + " " + criteriaIn.CompareOperator + " " + criteriaIn.CompareValue,
                Sequence = criteriaIn.Sequence,
                TableName = criteriaIn.TableName,
                TableColumn = criteriaIn.TableColumn
            }).ToList();
        }


        public static AdoQueryFieldsViewModel MapCriteriaDtoToCriteriaGridViewModel(
            AdoQueryFieldsModel queryFieldsViewModel)
        {
            var returnModel = new AdoQueryFieldsViewModel
            {
                ColumnHeaders = queryFieldsViewModel.ColumnHeaders,
                QueryRows = queryFieldsViewModel.QueryRows,
                TotalRowsCount = queryFieldsViewModel.TotalRowsCount
            };

            if (queryFieldsViewModel.ColumnHeaders.Count > 10)
            {
               returnModel.ColumnHeaders.RemoveRange(10, queryFieldsViewModel.ColumnHeaders.Count - 10);
                returnModel.FieldsToShow = 10;

            }
            else
            {
                returnModel.FieldsToShow = queryFieldsViewModel.ColumnHeaders.Count;
            
            }
            return returnModel;

        }
    }
}