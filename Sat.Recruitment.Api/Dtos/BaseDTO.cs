using Newtonsoft.Json;
using System;

namespace Sat.Recruitment.Api.Dtos
{
    [Serializable]
    public abstract class BaseDTO
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
