import i18n from 'i18next';
import Backend from 'i18next-xhr-backend';
import { initReactI18next } from 'react-i18next';
import LanguageDetector from 'i18next-browser-languagedetector';
import { baseUrl } from '../config'

const isDevelopment = process.env.NODE_ENV === "development";

const options = {
  loadPath: isDevelopment ? 
  '/locales/{{lng}}/{{ns}}.json' : 
  `${baseUrl}/locales/{{lng}}.{{ns}}.json`
};

i18n
  .use(Backend)
  .use(LanguageDetector)
  .use(initReactI18next)
  .init({
    lng: localStorage.getItem('i18nextLng') || 'en',
    fallbackLng: 'en',
    debug: false,
    cleanCode: true,
    backend: options,
    returnEmptyString: false,
    react: {
      wait: true,
      useSuspense: false
    }
  });

export default i18n;
