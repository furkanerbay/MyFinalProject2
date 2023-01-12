using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            List<IResult> errorResult = new List<IResult>();

            foreach (var logic in logics)
            {
                if(!logic.Success)
                {
                    errorResult.Add(logic);
                }
            }
            return null;
        }
    }
}
