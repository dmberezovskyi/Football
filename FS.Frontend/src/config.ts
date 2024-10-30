export const authenticationConfig = {
  clientId: process.env.REACT_APP_CLIENT_ID,
  scope: process.env.REACT_APP_SCOPE,
  responseType: process.env.REACT_APP_RESPONSE_TYPE
};

export const baseUrl = `http://localhost:5001`
export const googleCaptchaKey = process.env.REACT_APP_CAPTCHA_SITEKEY;
export const googleAnalyticsConfig = process.env.REACT_APP_GA_MEASUREMENT_ID;
