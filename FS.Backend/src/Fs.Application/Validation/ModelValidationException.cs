using System;

namespace Fs.Application.Validation
{
    public class ModelValidationException : Exception
    {
        public Error[] Errors { get; }
        
        public ModelValidationException(params Error[] errors) 
        {
            Errors = errors;
        }
    }
}
