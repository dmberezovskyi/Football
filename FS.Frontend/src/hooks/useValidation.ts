import { useTranslation } from 'react-i18next'
import * as Yup from 'yup';

const useValidation = () => {
    const { t } = useTranslation();

    const requiredMessage = (fieldToken: string): string => t("validation.required", { "field": t(fieldToken) });

    return {
        requiredMessage,
        validationSchemas: {
            firstName: Yup.string()
                .required(t("validation.required")),

            lastName: Yup.string()
                .required(t("validation.required")),

            email: Yup.string()
                .email(t("validation.invalidEmail", "Please, enter a valid email address"))
                .required(t("validation.required")),

            password: Yup.string()
                .required(t("validation.required")),

            phone: Yup.string()
                .required(t("validation.required")),

            birthDate: Yup.date()
            .required(t("validation.required")).nullable(),

            role: Yup.string()
                .required(t("validation.required")),

            about: Yup.string().notRequired()
        }
    }
}

export default useValidation;