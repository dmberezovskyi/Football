import { useTranslation } from 'react-i18next';

// ----------------------------------------------------------------------

export default function useLocales() {
  const { i18n, t } = useTranslation();
  const langStorage = localStorage.getItem('i18nextLng');

  const LANGS = [
    {
      value: 'en',
      label: t("labels.englishLangSelect", "English"),
      icon: '/static/icons/flags/en.svg'
    },
    {
      value: 'ru',
      label: t("labels.russianLangSelect", "Russian"),
      icon: '/static/icons/flags/ru.svg'
    },
    {
      value: 'ua',
      label: t("labels.ukrainianLangSelect", "Українська"),
      icon: '/static/icons/flags/ua.svg'
    }
  ];

  const currentLang = LANGS.find((_lang) => _lang.value === langStorage);

  const handleChangeLanguage = (newlang) => {
    i18n.changeLanguage(newlang);
  };

  return {
    onChangeLang: handleChangeLanguage,
    t,
    currentLang,
    allLang: LANGS
  };
}
