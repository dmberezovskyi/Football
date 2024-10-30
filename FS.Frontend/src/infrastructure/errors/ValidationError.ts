import { Error as ErrorType, ErrorValues } from 'src/types';
import { FormikErrors } from 'formik'
import i18n from 'src/utils/i18n'


export default class ValidationError extends Error {
    validationErrors: ErrorType[] = [];

    constructor(errors: ErrorType[]){
        super();
        this.validationErrors = errors;
    }

    getFormikErrors<T>(): FormikErrors<T> {
        let errors = {} as any;
        
        this.validationErrors.forEach(e => {
            let message = this.getValidationMessage(e.errorCode, e.values);
            this.buildErrorObject(e.location, message, errors)
        });

        return errors as FormikErrors<T>;
    }

    buildErrorObject(path: string, value: string, root: any) {
        var segments = path.split('.'),
            cursor = root || window,
            segment,
            i;
      
        for (i = 0; i < segments.length - 1; ++i) {
           segment = segments[i];
           cursor = cursor[segment] = cursor[segment] || {};
        }
      
        return cursor[segments[i]] = value;
      };

    getValidationMessage(errorCode: string, errorValues: ErrorValues): string{
        return i18n.t(`validation.${errorCode}`, { ...errorValues })
    }
}