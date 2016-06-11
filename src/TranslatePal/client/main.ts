import {Aurelia} from 'aurelia-framework';
import {bootstrap} from 'aurelia-bootstrapper-webpack';
import config from './authConfig';
import {AuthService} from 'aurelia-auth';

bootstrap((aurelia: Aurelia): void => {

    aurelia
        .use
        .standardConfiguration()
        .developmentLogging()
        .plugin('aurelia-auth', (baseConfig) => {

            baseConfig.configure(config);
        });

    // After starting the aurelia, we can request the AuthService directly
    // from the DI container on the aurelia object. We can then set the 
    // correct root by querying the AuthService's isAuthenticated method.
    aurelia.start().then(() => {

        let auth: AuthService = aurelia.container.get(AuthService);
        let root = auth.isAuthenticated() ? 'app' : 'login';

        aurelia.setRoot(root, document.body);
    });
});