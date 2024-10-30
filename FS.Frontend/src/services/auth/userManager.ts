import { UserManager, UserManagerSettings } from "oidc-client"
import { authenticationConfig as authConfig, baseUrl } from "../../config"

const UMSettings: UserManagerSettings = {
    authority: baseUrl,
    client_id: authConfig.clientId,
    redirect_uri: `${baseUrl}/auth/callback`,
    silent_redirect_uri: `${baseUrl}/auth/callback`,
    post_logout_redirect_uri: `${baseUrl}/auth/logout`,
    scope: authConfig.scope,
    response_type: authConfig.responseType,
    filterProtocolClaims: false
};

export default new UserManager(UMSettings)