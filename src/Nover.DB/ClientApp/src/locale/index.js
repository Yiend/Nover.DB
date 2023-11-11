import i18n from 'i18next'
import { initReactI18next } from 'react-i18next'

import Backend from 'i18next-http-backend'
import LanguageDetector from 'i18next-browser-languagedetector'

import zhLocale from './lang/zh'
import enLocale from './lang/en'
const resources = {
  zh: {
    translation: { ...zhLocale }
  },
  en: {
    translation: { ...enLocale }
  }
}

i18n
  .use(Backend)
  .use(LanguageDetector)
  .use(initReactI18next)
  .init({
    fallbackLng: 'zh',
    lng: 'zh',
    debug: false,
    resources,
    interpolation: {
      escapeValue: false
    }
  })

export default i18n
