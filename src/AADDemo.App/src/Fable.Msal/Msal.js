export var initUserAgentApplication = (config) => {
    const msalInstance = new Msal.UserAgentApplication(config);
    msalInstance.handleRedirectCallback((error, response) => {
        // if error is not null, something went wrong
        // if not, response is a successful login response
    });

    return msalInstance;
};
