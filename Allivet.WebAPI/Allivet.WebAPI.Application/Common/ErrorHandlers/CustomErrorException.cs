using System;

namespace Allivet.WebAPI.Application.Common.ErrorHandlers
{

    [Serializable]
    public class CustomErrorException : Exception
    {
        public CustomErrorException()
        {

        }

        public CustomErrorException(string errorMessage) : base(string.Format("{0}", errorMessage))
        {

        }
    }
}
