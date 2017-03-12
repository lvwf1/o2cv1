using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using O2CV1EntityDtos;
using O2V1Web.Models.ViewModels;

namespace O2V1Web.Mappers
{
    public static class CriteriaMapper
    {
        public static List<CriteriaGridViewModel> MapCriteriaDtoToCriteriaGridViewModel(List<CriteriaDto> criteriaDto)
        {
            return criteriaDto.Select(criteriaIn => new CriteriaGridViewModel
            {
                CompareOperator = criteriaIn.CompareOperator, CompareValue = criteriaIn.CompareValue, Description = criteriaIn.Description, Disabled = criteriaIn.Disabled,
                Name = criteriaIn.Name + " " + criteriaIn.CompareOperator + " " + criteriaIn.CompareValue,
                Sequence = criteriaIn.Sequence, TableName = criteriaIn.TableName, TableColumn = criteriaIn.TableColumn
            }).ToList();
        }
    }
}